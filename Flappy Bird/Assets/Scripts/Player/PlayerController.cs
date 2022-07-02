using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    CircleCollider2D _collider;
    Collider2D[] collidersList = new Collider2D[5];
    ContactFilter2D filter;

    public float gravity;
    public float jumpHeight;
    public float rotationSpeed;
    public float velocityRotationMultiplier;
    float velocityY;

    bool dead;
    bool gamePaused;
    bool gameStarted;

    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        InputManager.instance.playerInput.Player.Jump.performed += Jump;
        GameManger.instance.OnGameStart += OnGameStart;
        GameManger.instance.PauseGame += OnPauseGame;
        GameManger.instance.ResumeGame += OnResumeGame;
    }

    void Update()
    {
        if(gamePaused || !gameStarted) return;

        # region Collisions
        CollisionCheck();
        #endregion

        # region Movement
        // Y movement
        transform.position = new Vector2(transform.position.x, transform.position.y + velocityY);
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -3.2f, 4.74f));

        // Z rotation
        Rotate();

        // Gravity
        velocityY += gravity * Time.deltaTime * Time.deltaTime;
        # endregion
    }

    void Jump(InputAction.CallbackContext context)
    {
        if(dead || gamePaused) return;
        AudioManager.instance.PlayFlapSound();
        velocityY = Mathf.Sqrt(jumpHeight * .02f * -2 * gravity);
    }

    void Rotate()
    {
        if(gamePaused) return;

        // Transform Y velocity to rotation
        float rotationZ = transform.rotation.z + velocityY * velocityRotationMultiplier;
        rotationZ = Mathf.Clamp(rotationZ, -90f, 40f);

        // Lerp to the desired rotation
        Quaternion roation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, rotationZ));
        transform.rotation = Quaternion.Lerp(transform.rotation, roation, Time.deltaTime * rotationSpeed);
    } 

    void CollisionCheck()
    {
        if(gamePaused || dead) return;

        _collider.OverlapCollider(filter, collidersList);
        for (int i = 0; i < collidersList.Length; i++)
        {
            if(collidersList[i] != null)
            {
                GameManger.instance.PlayerDead();
                dead = true;
                collidersList[i] = null;
            }
        }
    }

    void OnPauseGame()
    {
        gamePaused = true;
    }

    void OnResumeGame()
    {
        gamePaused = false;
    }

    void OnGameStart()
    {
        gameStarted = true;
    }
}

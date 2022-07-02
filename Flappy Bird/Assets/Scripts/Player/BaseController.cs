using UnityEngine;

public class BaseController : MonoBehaviour
{
    public PlayerController player;
    bool playerDead;

    public Transform[] baseArray;
    public float speed;

    bool gamePaused;

    void Start()
    {
        GameManger.instance.OnPlayerDeath += OnPlayerDeath;
        GameManger.instance.PauseGame += OnPauseGame;
        GameManger.instance.ResumeGame += OnResumeGame;
    }

    void Update()
    {
        if(playerDead || gamePaused) return;

        for (int i = 0; i < baseArray.Length; i++)
        {
            baseArray[i].transform.position = new Vector2(baseArray[i].transform.position.x - speed * Time.deltaTime, baseArray[i].transform.position.y);

            if(baseArray[i].transform.position.x <= -6.273f)
            {
                baseArray[i].transform.position = new Vector2(6.6f, baseArray[i].transform.position.y);
            }
        }
    }

    void OnPlayerDeath()
    {
        playerDead = true;
    }

    void OnPauseGame()
    {
        gamePaused = true;
    }

    void OnResumeGame()
    {
        gamePaused = false;
    }
}

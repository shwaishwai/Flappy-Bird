using UnityEngine;

public class PipeController : MonoBehaviour
{
    PlayerController player;
    bool playerDead;

    public SpriteRenderer bottomPipe;
    public SpriteRenderer topPipe;

    public float spacing;
    public float height;

    public Vector2 spacingMinMax;
    public Vector2 heightMinMax;

    public float speed;
    bool gamePaused;
    bool addedScore;

    void Start()
    {
        spacing = Random.Range(spacingMinMax.x, spacingMinMax.y);
        height = Random.Range(heightMinMax.x, heightMinMax.y);

        bottomPipe.transform.position = new Vector2(bottomPipe.transform.position.x, height);
        topPipe.transform.position = new Vector2(topPipe.transform.position.x, spacing);

        player = FindObjectOfType<PlayerController>();
        GameManger.instance.OnPlayerDeath += OnPlayerDeath;
        GameManger.instance.PauseGame += OnPauseGame;
        GameManger.instance.ResumeGame += OnResumeGame;
    }

    void Update()
    {
        if(gamePaused) return;

        # region Size
        bottomPipe.transform.position = new Vector2(bottomPipe.transform.position.x, height);
        topPipe.transform.position = new Vector2(topPipe.transform.position.x, spacing + bottomPipe.transform.position.y);
        # endregion

        # region Despawning
        if(transform.position.x < -4) Destroy(gameObject);
        # endregion

        if(playerDead) return;
        # region Movement
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        # endregion
    
        # region Score Increment
        if(transform.position.x < -1.38f && !addedScore)
        {
            addedScore = true;
            GameManger.instance.IncrementScore();
        }
        # endregion
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

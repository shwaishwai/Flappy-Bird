using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public PlayerController player;
    bool playerDead;

    public PipeController pipe;
    PipeController currentPipe;
    public float spawnSpeed;
    float lastSpawn;
    public float pipeSpeed;

    bool gamePaused;
    bool gameStarted;

    void Start()
    {
        GameManger.instance.OnPlayerDeath += OnPlayerDeath;
        GameManger.instance.OnGameStart += OnGameStart;
        GameManger.instance.PauseGame += OnPauseGame;
        GameManger.instance.ResumeGame += OnResumeGame;
    }

    void Update()
    {   
        if(playerDead || gamePaused || !gameStarted) return;

        if(Time.time - spawnSpeed > lastSpawn)
        {
            currentPipe = Instantiate(pipe, new Vector2(4f, -5.037924f), Quaternion.identity);
            currentPipe.transform.parent = transform;

            lastSpawn = Time.time;
            currentPipe.speed = -pipeSpeed;
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

    void OnGameStart()
    {
        gameStarted = true;
    }
}

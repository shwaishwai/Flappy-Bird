/*
SO YOU DARED TAKE A LOOK AT THIS CODE HUH...?
    THEN BEHOLD THE CURE I BESTOW UPON YOU... A CURSE THAT WILL ERASE ALL YOUR CODING ABILITIES AND SQUASH ALL OF YOUR REMAINING BRAINCELLS...
        HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHA .oO(happE WHAT IS HE DOING HERE?)
*/

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManger : MonoBehaviour
{       
    public static GameManger instance;

    public event System.Action OnGameStart;
    public event System.Action PauseGame;
    public event System.Action ResumeGame;
    public event System.Action OnScore;
    public event System.Action OnPlayerDeath;
    public event System.Action OnHighScore;
    bool gameStarted;

    public PauseMenu pauseMenu;

    public int score;
    public int highScore;

    public bool schizoMode;
    Transform mainCam;

    void Awake()
    {
        # region Singleton

        if(instance != null) Destroy(gameObject);
        instance = this;
        // DontDestroyOnLoad(gameObject);

        # endregion
        Application.targetFrameRate = 60;
        // PlayerPrefs.DeleteAll();
        highScore = PlayerPrefs.GetInt("High Score");
        Time.timeScale = 1;
        mainCam = Camera.main.transform;

        if(PlayerPrefs.GetString("Schizo Mode") == "True")
        {
            schizoMode = true;
            StartCoroutine(SCHIZOMODE());
        }
        else if(PlayerPrefs.GetString("Schizo Mode") == "False")
        {
            schizoMode = false;
        }
    }
    
    void Start()
    {
        InputManager.instance.playerInput.Player.Jump.performed += StartGame;
        pauseMenu.OnPauseGame += OnPauseGame;
        pauseMenu.OnResumeGame += OnResumeGame;
    }

    void StartGame(InputAction.CallbackContext context)
    {
        if(gameStarted) return;
        gameStarted = true;
        if(OnGameStart != null) OnGameStart();
    }

    void OnPauseGame()
    {
        if(PauseGame != null) PauseGame();
    }

    void OnResumeGame()
    {
        if(ResumeGame != null) ResumeGame();
    }

    public void IncrementScore()
    {
        if(OnScore != null) OnScore();
        score++;
        AudioManager.instance.PlayScoreSound();
    }

    public void PlayerDead()
    {
        if(OnPlayerDeath != null) OnPlayerDeath();
        AudioManager.instance.PlayCrashSound();
        AudioManager.instance.PlayFallSound();
        if(score > highScore)
        {
            highScore = score;
            if(OnHighScore != null) OnHighScore();
            PlayerPrefs.SetInt("High Score", highScore);
        } 
    }

    public IEnumerator SCHIZOMODE()
    {
        while(schizoMode)
        {
            mainCam.Rotate(0,0,Time.deltaTime * 10f);
            yield return null;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public ScoreBoardAnimationEventBroadcaster scoreBoardAnimationEventBroadcaster;

    public Animator gameOverText;
    public Animator scoreBoard;
    public Animator highScoreTextAnimator;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    int currentScore;

    public GameObject newHighScoreText;
    public GameObject goAganeButton;
    public GameObject coin;

    bool newHighScore;

    public Sprite silverCoin;
    public Sprite bronzeCoin;
    public Sprite audioOn;
    public Sprite audioOff;
    public Sprite schizoOn;
    public Sprite schizoOff;

    public Image audioButton;
    public Image schizoButton;

    void Start()
    {
        GameManger.instance.OnPlayerDeath += OnPlayerDeath;
        GameManger.instance.OnHighScore += OnHighScore;
        scoreBoardAnimationEventBroadcaster.OnScoarBoardAnimationOver += UpdateScore;
        highScoreText.text = GameManger.instance.highScore.ToString();

        if(GameManger.instance.schizoMode) schizoButton.sprite = schizoOn;
        if(AudioManager.instance.source.mute) audioButton.sprite = audioOff;
    }

    void OnPlayerDeath()
    {
        gameOverText.SetTrigger("GameOver");
        scoreBoard.SetTrigger("GameOver");   
    }

    public void GOAGANE()
    {
        SceneManager.LoadScene(0);
    }

    void OnHighScore()
    {
        newHighScore = true;
    }

    void UpdateScore()
    {
        StartCoroutine(UpdateCurrentScore());
    }

    IEnumerator UpdateCurrentScore()
    {
        yield return new WaitForSeconds(.1f);
        while(currentScore != GameManger.instance.score + 1)
        {
            scoreText.text = currentScore.ToString();
            currentScore++;
            yield return new WaitForSeconds(.05f);
        }

        if(newHighScore)
        {
            highScoreText.text = GameManger.instance.highScore.ToString();
            newHighScoreText.SetActive(true);
            highScoreTextAnimator.SetTrigger("NewHS");
            coin.SetActive(true);
        }
        else if(GameManger.instance.highScore - GameManger.instance.score <= 5 && GameManger.instance.score > 2)
        {
            coin.GetComponent<Image>().sprite = silverCoin;
            coin.SetActive(true);
        }
        else if(GameManger.instance.score > 2)
        {
            coin.GetComponent<Image>().sprite = bronzeCoin;
            coin.SetActive(true);
        }

        goAganeButton.SetActive(true);
    }

    public void Mute()
    {
        AudioManager.instance.Mute();
        if(AudioManager.instance.source.mute) 
        {
            audioButton.sprite = audioOff;
        }
        if(!AudioManager.instance.source.mute) 
        {
            audioButton.sprite = audioOn;
        }
        PlayerPrefs.SetString("Audio Mute", AudioManager.instance.source.mute.ToString());
    }

    public void ChangeSchizoMode()
    {
        if(GameManger.instance.schizoMode)
        {
            GameManger.instance.schizoMode = false;
            StopCoroutine(GameManger.instance.SCHIZOMODE());
            Camera.main.transform.rotation = Quaternion.identity;
            schizoButton.sprite = schizoOff;
        }
        else
        {
            GameManger.instance.schizoMode = true;
            StopCoroutine(GameManger.instance.SCHIZOMODE());
            StartCoroutine(GameManger.instance.SCHIZOMODE());
            schizoButton.sprite = schizoOn;
        }
        PlayerPrefs.SetString("Schizo Mode", GameManger.instance.schizoMode.ToString());
    }
}

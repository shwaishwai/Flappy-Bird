using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public event System.Action OnPauseGame;
    public event System.Action OnResumeGame;
    bool gamePaused;

    public Image pauseBg;
    public GameObject menuButton;
    public GameObject logoImage;

    public void Start()
    {
        InputManager.instance.playerInput.Player.Pause.performed += DOTHEPAUSETHINGMEGALUL;
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
        if(gamePaused)
        {
            Time.timeScale = 0;
            if(OnPauseGame != null) OnPauseGame();

            pauseBg.color = new Color32(0,0,0,100);
            menuButton.SetActive(true);
            logoImage.SetActive(true);
        }

        if(!gamePaused)
        {
            Time.timeScale = 1;
            if(OnResumeGame != null) OnResumeGame();
            pauseBg.color = new Color32(0,0,0,0);
            menuButton.SetActive(false);
            logoImage.SetActive(false);
        }
    }

    public void DOTHEPAUSETHINGMEGALUL(InputAction.CallbackContext context)
    {
        PauseGame();
    }
}

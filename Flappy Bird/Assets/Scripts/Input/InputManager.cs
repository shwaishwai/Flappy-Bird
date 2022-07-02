using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInputActions playerInput;
    
    void Awake()
    {
        #region Singleton

        if(instance != null) Destroy(gameObject);
        instance = this;
        // DontDestroyOnLoad(gameObject);

        #endregion
    
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();
    }
}

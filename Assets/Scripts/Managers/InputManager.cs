using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private GameInput gameInput;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameInput = new GameInput();
        gameInput.Player.Enable();
    }

    private void OnDestroy()
    {
        gameInput.Player.Disable();
    }

    //get current input value from input action map (vector 2)
    public Vector2 GetMoveInput()
    {
        return gameInput.Player.Move.ReadValue<Vector2>();
    }

    //jump (bool)
    public bool JumpTriggeredThisFrame()
    {
        return gameInput.Player.Jump.triggered;
    }

    // not plan to use yet but leave it here
    public bool JumpHeld()
    {
        return gameInput.Player.Jump.IsPressed();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

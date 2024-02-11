using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputType inputType;
    private Player player;

    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode shootKey;

    private void Awake()
    {
        player = GetComponent<Player>();
        InitInputKeys();
    }

    private void InitInputKeys()
    {
        if (inputType == InputType.WASD)
        {
            upKey = KeyCode.W;
            downKey = KeyCode.S;
            shootKey = KeyCode.A;
        }
        if (inputType == InputType.ArrowKeys)
        {
            upKey = KeyCode.UpArrow;
            downKey = KeyCode.DownArrow;
            shootKey = KeyCode.RightArrow;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            player.moveDirection = 1;
        }
        if (Input.GetKeyDown(downKey))
        {
            player.moveDirection = -1;
        }
        if (Input.GetKeyUp(upKey) || Input.GetKeyUp(downKey))
        {
            player.moveDirection = 0;
        }

        if (Input.GetKeyDown(shootKey))
        {
            player.Shoot();
        }
    }
}

enum InputType
{
    WASD,
    ArrowKeys,
}
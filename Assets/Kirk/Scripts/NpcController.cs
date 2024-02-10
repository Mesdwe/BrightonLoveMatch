// KHOGDEN
using UnityEngine;

/*
    KH - NOTE: Feel free to read through or edit anything on this script,
    as I can imagine there will be an error or two on the Unity console log.
    I won't be able to see any errors as I'm having to do these scripts
    without Unity open.
*/

[RequireComponent(typeof(Rigidbody2D))]
public class NpcController : MonoBehaviour
{
    [Header("NPC Statistics")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float scoreReward = 100f;
    [SerializeField] float setNewDestinationTime = 5f;
    private float setNewDestinationTimer;

    // KH - Range for movement space the NPC can move around.
    public class MoveSpace
    {
        private float min = -9f;
        private float max = 9f;

        public float Min
        {
            get { return min; }
            set { min = value; }
        }

        public float Max
        {
            get { return max; }
            set { max = value; }
        }
    }
    private MoveSpace xMoveSpace = new MoveSpace();
    private MoveSpace yMoveSpace = new MoveSpace();

    /*private enum Direction {north, east, south, west};
    private Direction facingDirection;*/

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 currentPos;
    private Vector2 destination;

    // KH - Called before 'void Start()'.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // KH - Called upon the first frame.
    void Start()
    {

    }

    // KH - Called upon every frame.
    void Update()
    {
        currentPos = transform.position;
        Move();

        // KH - Decrease timer until it reaches zero.
        if(setNewDestinationTimer > 0f)
            setNewDestinationTimer -= Time.deltaTime;
        else if(setNewDestinationTimer < 0f)
            setNewDestinationTimer = 0f;
        
        // KH - Set a random destination for the NPC.
        if(setNewDestinationTimer == 0f)
        {
            // KH - Reset the timer.
            setNewDestinationTimer = setNewDestinationTime;

            // KH - Set a new randomly made destination.
            float x = Random.Range(xMoveSpace.min, xMoveSpace.max);
            float y = Random.Range(yMoveSpace.min, yMoveSpace.max);
            SetDestination(new Vector2(x, y));
        }
    }

    // KH - Have the NPC move towards the target position.
    void Move()
    {
        // KH - Calculated direction the NPC will move towards.
        float x = 0f;
        float y = 0f;
        float d = Vector3.Distance(currentPos, destination);

        // KH - Calculate where the NPC is going to move.
        if (d > 0.2f)
        {
            if (currentPos.x > destination.x)
                x = -moveSpeed;
            else
                x = moveSpeed;

            if (currentPos.y > destination.y)
                y = -moveSpeed;
            else
                y = moveSpeed;
        }

        // KH - Move the NPC.
        rb.velocity = new Vector2(x, y);
    }

    // KH - Set the destination of the NPC.
    public void SetDestination(Vector2 newDestination)
    {
        destination = newDestination;
    }

    public void SetHorizontalMoveSpace(float min, float max)
    {
        xMoveSpace.min = min;
        xMoveSpace.max = max;
    }

    public void SetVerticalMoveSpace(float min, float max)
    {
        yMoveSpace.min = min;
        yMoveSpace.max = max;
    }
}
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
    [SerializeField] int scoreReward = 100;
    public enum BehaviourState { patrol, dazzled, sad, inLove, honeymoon };
    [SerializeField] BehaviourState behaviourState;

    [Header("Time Settings")]
    [SerializeField][Range(0.001f, 20f)] float setNewDestinationTime = 5f;
    private float setNewDestinationTimer;
    [SerializeField][Range(0.001f, 20f)] float dazzledLastingTime = 5f;
    [SerializeField][Range(0.001f, 20f)] float sadTime = 10f;
    private float sadTimer;
    private float matchSearchTime = 0.01f;
    private float matchSearchTimer;
    private float honeymoonTime = 3f;
    private float honeymoonTimer;
    private bool rewardedScore;
    private NpcController loveMatch;
    private NPCVisual npcVisual;
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

    private Rigidbody2D rb;
    private Animator anim;
    private NPC npcScript;
    private Vector2 currentPos;
    private Vector2 destination;

    // KH - Called before 'void Start()'.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        npcScript = GetComponent<NPC>();
        npcVisual = GetComponent<NPCVisual>();
        sadTimer = sadTime;
        honeymoonTimer = honeymoonTime;
    }

    // KH - Called upon the first frame.
    void Start()
    {
        SetRandomDestination();
    }

    // KH - Called upon every frame.
    void Update()
    {
        currentPos = transform.position;
        Move();

        if (behaviourState == BehaviourState.patrol)
        {
            // KH - Decrease timer until ready to pick new AI destination.
            if (setNewDestinationTimer > 0f)
                setNewDestinationTimer -= Time.deltaTime;
            else if (setNewDestinationTimer < 0f)
                setNewDestinationTimer = 0f;

            // KH - Set a random destination for the NPC.
            if (setNewDestinationTimer == 0f)
            {
                setNewDestinationTimer = setNewDestinationTime;
                SetRandomDestination();
            }
        }

        if (behaviourState == BehaviourState.patrol || behaviourState == BehaviourState.dazzled)
        {
            // KH - Decrease timer until NPC is sad.
            if (sadTimer > 0f)
                sadTimer -= Time.deltaTime;
            else if (sadTimer < 0f)
                sadTimer = 0f;
        }

        if (behaviourState == BehaviourState.dazzled)
        {
            // KH - Decrease timer for next time to search for love partner.
            if (matchSearchTimer > 0f)
                matchSearchTimer -= Time.deltaTime;
            else if (matchSearchTimer < 0f)
                matchSearchTimer = 0f;

            // KH - Attempt to find a love match for this NPC.
            if (matchSearchTimer == 0f)
            {
                matchSearchTimer = matchSearchTime;
                SetLoveMatch(FindLoveMatch());
            }

            // KH - Check to see that this NPC has found a love match.
            if (loveMatch != null)
                loveMatch.SetLoveMatch(this);
        }

        if (behaviourState == BehaviourState.inLove)
        {
            // KH - Reward score for matching couples together.
            if (!rewardedScore)
            {
                rewardedScore = true;
                LevelManager.instance.AddScore(scoreReward);
            }

            // KH - Have the NPC and it's love match meet each other at the center.
            if (OnRightSide())
                SetDestination(new Vector2(2f, loveMatch.GetCurrentPos().y));
            else
                SetDestination(new Vector2(-2f, loveMatch.GetCurrentPos().y));

            // KH - Decrease the honeymoon timer until this NPC and their go on one.
            if (honeymoonTimer > 0f)
                honeymoonTimer -= Time.deltaTime;
            else if (honeymoonTimer < 0f)
                honeymoonTimer = 0f;

            // KH - Once the timer is at zero, have the couples leave for their honeymoon.
            if (honeymoonTimer == 0f)
            {
                NpcBehaviourState = BehaviourState.honeymoon;
                loveMatch.NpcBehaviourState = BehaviourState.honeymoon;
            }
        }

        if (behaviourState == BehaviourState.honeymoon)
            SetDestination(new Vector2(currentPos.x, -100f));

        // KH - Make the NPC become sad and leave if they take too long to find a match.
        if (sadTimer == 0f)
            NpcBehaviourState = BehaviourState.sad;

        if (NpcBehaviourState == BehaviourState.sad)
        {
            npcVisual.SetNpcSprite(BehaviourState.sad);
            if (OnRightSide())
                SetDestination(new Vector2(100f, 0f));
            else
                SetDestination(new Vector2(-100f, 0f));
        }
    }

    // KH - Called upon collision of a trigger collider.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // KH - Make the NPC in a dazzled state after being shot.
        if (collision.gameObject.CompareTag("Arrow") && behaviourState == BehaviourState.patrol)
        {
            // KH - Make the NPC dazzled and freeze in place.
            NpcBehaviourState = BehaviourState.dazzled;
            SetDestination(currentPos);
            sadTimer = dazzledLastingTime;
            npcVisual.SetNpcSprite(NpcBehaviourState);

            // KH - Destroy the cupid arrow after hit.
            Destroy(collision.gameObject);
        }
    }

    // KH - Have the NPC move towards the target position.
    void Move()
    {
        // KH - Calculated direction the NPC will move towards.
        float x = 0f;
        float y = 0f;
        float offset = 0.1f;

        // KH - Calculate where the NPC is going to move.
        if (currentPos.x > destination.x + offset)
            x = -moveSpeed;
        else if (currentPos.x < destination.x - offset)
            x = moveSpeed;

        if (currentPos.y > destination.y + offset)
            y = -moveSpeed;
        else if (currentPos.y < destination.y - offset)
            y = moveSpeed;

        // KH - Move the NPC.
        rb.velocity = new Vector2(x, y);
    }

    // KH - Set the destination of the NPC.
    public void SetDestination(Vector2 newDestination)
    {
        destination = newDestination;
    }

    // KH - Set a random destination based on move space.
    public void SetRandomDestination()
    {
        // KH - Set a new randomly made destination.
        float x = Random.Range(xMoveSpace.Min, xMoveSpace.Max);
        float y = Random.Range(yMoveSpace.Min, yMoveSpace.Max);
        SetDestination(new Vector2(x, y));
    }

    // KH - Method to set this NPC's love match to another NPC.
    public void SetLoveMatch(NpcController otherNpc)
    {
        loveMatch = otherNpc;

        if (otherNpc != null)
        {
            behaviourState = BehaviourState.inLove;
            npcVisual.SetNpcSprite(behaviourState);
        }
    }

    // KH - Method to find a love match for this NPC.
    public NpcController FindLoveMatch()
    {
        NpcController[] npcs = FindObjectsOfType<NpcController>();
        foreach (NpcController npc in npcs)
        {
            NPC otherNpcScript = npc.gameObject.GetComponent<NPC>();

            // KH - Prevent the NPC using itself as a love match.
            if (npc != this)
            {
                // KH - Conditions for this NPC and the referred other NPC to be a match.
                bool isDazzled = npc.behaviourState == BehaviourState.dazzled;
                bool onOppositeSide = npc.OnRightSide() == !OnRightSide();
                bool sameType = otherNpcScript.type == npcScript.type;

                // KH - Find a NPC that's on the opposite side and is in a dazzled state.
                if (isDazzled && onOppositeSide && sameType)
                    return npc;
            }
        }

        return null;
    }

    public void SetHorizontalMoveSpace(float min, float max)
    {
        xMoveSpace.Min = min;
        xMoveSpace.Max = max;
    }

    public void SetVerticalMoveSpace(float min, float max)
    {
        yMoveSpace.Min = min;
        yMoveSpace.Max = max;
    }

    public BehaviourState NpcBehaviourState
    {
        get { return behaviourState; }
        set { behaviourState = value; }
    }

    public Vector2 GetCurrentPos()
    {
        return currentPos;
    }

    public bool OnRightSide()
    {
        return transform.position.x > 0f;
    }

}
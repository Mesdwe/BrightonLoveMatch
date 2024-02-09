// KHOGDEN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    }

    // KH - Have the NPC move towards the target position.
    void Move()
    {
        // KH - Calculated direction the NPC will move towards.
        float x = 0f;
        float y = 0f;
        float d = Vector3.Distance(currentPos, destination);

        // KH - Calculate where the NPC is going to move.
        if(d > 0.2f)
        {
            if(currentPos.x > destination.x)
                x = -moveSpeed;
            else
                x = moveSpeed;

            if(currentPos.y > destination.y)
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
        destination = d;
    }
}
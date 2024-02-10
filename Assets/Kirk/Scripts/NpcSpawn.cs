// KHOGDEN
using UnityEngine;

/*
    KH - NOTE: Feel free to read through or edit anything on this script,
    as I can imagine there will be an error or two on the Unity console log.
    I won't be able to see any errors as I'm having to do these scripts
    without Unity open.
*/

public class NpcSpawn : MonoBehaviour
{
    [SerializeField] GameObject npcPrefab;
    [SerializeField][Range(0.001f, 60f)] float spawnTime = 10f;
    private float spawnTimer;

    // KH - Range for movement space the NPC can move around.
    [System.Serializable]
    public class MoveSpace
    {
        [SerializeField] float min = -9f;
        [SerializeField] float max = 9f;

        public float Min()
        {
            return min;
        }

        public float Max()
        {
            return max;
        }
    }
    [SerializeField] MoveSpace xMoveSpace = new MoveSpace();
    [SerializeField] MoveSpace yMoveSpace = new MoveSpace();

    // KH - Called upon the first frame.
    void Start()
    {

    }

    // KH - Called upon every frame.
    void Update()
    {
        // KH - Continously decrease timer until it reaches zero.
        if (spawnTimer > 0f)
            spawnTimer -= Time.deltaTime;
        else if (spawnTimer < 0f)
            spawnTimer = 0f;

        // KH - Spawn NPC once timer reaches zero.
        if (spawnTimer == 0f)
        {
            spawnTimer = spawnTime;
            Spawn();
        }
    }

    // KH - Instantiate an NPC at the spawn's position.
    void Spawn()
    {
        GameObject n = Instantiate(npcPrefab, transform.position, Quaternion.identity);
        NpcController npcScript = n.GetComponent<NpcController>();

        // KH - Set the movement limit range for where the NPC chooses to move around.
        npcScript.SetHorizontalMoveSpace(xMoveSpace.Min(), xMoveSpace.Max());
        npcScript.SetVerticalMoveSpace(yMoveSpace.Min(), yMoveSpace.Max());
    }
}
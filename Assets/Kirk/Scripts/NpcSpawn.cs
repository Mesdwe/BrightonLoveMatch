// KHOGDEN
using UnityEngine;
using System.Collections.Generic;

/*
    KH - NOTE: Feel free to read through or edit anything on this script,
    as I can imagine there will be an error or two on the Unity console log.
    I won't be able to see any errors as I'm having to do these scripts
    without Unity open.
*/

public class NpcSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] npcPrefabs;
    [SerializeField][Range(0.001f, 60f)] float spawnTime = 10f;
    [SerializeField][Range(0, 12)] int maxSpawnCapacity = 5;
    [SerializeField] bool disableSpawn;
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

    // KH - List used to track the NPCs this spawner has instantiated.
    private List<GameObject> npcs = new List<GameObject>();

    // KH - Called upon every frame.
    void Update()
    {
        if (!disableSpawn)
        {
            // KH - Continously decrease timer until it reaches zero.
            if (spawnTimer > 0f)
                spawnTimer -= Time.deltaTime;
            else if (spawnTimer < 0f)
                spawnTimer = 0f;

            // KH - Spawn NPC once timer reaches zero.
            if (spawnTimer == 0f && npcs.Count < maxSpawnCapacity)
            {
                spawnTimer = spawnTime;
                Spawn();
            }
        }
    }

    // KH - Instantiate an NPC at the spawn's position.
    void Spawn()
    {
        // KH - Spawn a random NPC type from the array of NPC prefabs.
        int r = Random.Range(0, npcPrefabs.Length);
        GameObject n = Instantiate(npcPrefabs[r], transform.position, Quaternion.identity);

        // KH - Set the movement limit range for where the NPC chooses to move around.
        NpcController npcScript = n.GetComponent<NpcController>();
        npcScript.SetHorizontalMoveSpace(xMoveSpace.Min(), xMoveSpace.Max());
        npcScript.SetVerticalMoveSpace(yMoveSpace.Min(), yMoveSpace.Max());

        // KH - Add the NPC gameobject to the list.
        npcs.Add(n);
    }

    // KH - Destroy an NPC gameobject from the scene and remove them from the list.
    public void DestroyNpc(GameObject npc)
    {
        int i = npcs.IndexOf(npc);
        npcs.RemoveAt(i);
        Destroy(npc);
    }

    // KH - Make all NPCs that spawned from this spawner sad.
    public void SaddenNpcs()
    {
        foreach(GameObject npc in npcs)
        {
            NpcController npcScript = npc.GetComponent<NpcController>();

            if (npcScript.NpcBehaviourState == NpcController.BehaviourState.patrol || npcScript.NpcBehaviourState == NpcController.BehaviourState.dazzled)
                npcScript.NpcBehaviourState = NpcController.BehaviourState.sad;
        }
    }

    public void SetDisableSpawn(bool toggle)
    {
        disableSpawn = toggle;
    }

    // KH - Method to see if this spawner contains the NPC specified in the parameter.
    public bool ContainsNpc(GameObject npc)
    {
        return npcs.Contains(npc);
    }
}
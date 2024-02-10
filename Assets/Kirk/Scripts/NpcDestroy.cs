// KHOGDEN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcDestroy : MonoBehaviour
{
    // KH - Called when a collider enters this trigger.
    void OnTriggerEnter2D(Collider2D collision)
    {
        NpcController npcScript = collision.gameObject.GetComponent<NpcController>();
        if(npcScript != null)
        {
            // KH - Check which NPC spawn spawned this NPC.
            NpcSpawn[] npcSpawns = FindObjectsOfType<NpcSpawn>();
            foreach(NpcSpawn npcSpawn in npcSpawns)
            {
                // KH - Destroy the NPC, and remove them from the NPC spawn's NPCs list.
                if(npcSpawn.ContainsNpc(npcScript.gameObject))
                {
                    npcSpawn.DestroyNpc(npcScript.gameObject);
                    break;
                }
            }
        }
    }
}
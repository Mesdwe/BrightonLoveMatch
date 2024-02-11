using System;
using UnityEngine;

// This could be used as a proper npc script 

public class NPC : MonoBehaviour
{
    public NpcType type;
    public event Action<NPC> OnNPCShot;

    private void Awake()
    {
        OnNPCShot += MatchManager.HandleNpcShot;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Arrow"))
        {
            OnNPCShot?.Invoke(this);
        }
    }

    private static void OnNpcShot()
    {
        // set match
        // visual effect
        // destroy it for now to show feedback
        // Destroy(gameObject);
    }

    public void DestroyNpc()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnNPCShot -= MatchManager.HandleNpcShot;

    }
}

public enum NpcType
{
    type1,
    type2,
    type3,
    type4,
    type5,
}
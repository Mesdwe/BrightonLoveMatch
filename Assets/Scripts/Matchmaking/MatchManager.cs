using System.Collections.Generic;

public class MatchManager
{
    private static MatchManager instance = null;

    private MatchManager()
    {
    }

    public static MatchManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MatchManager();
            }
            return instance;
        }
    }

    public static Dictionary<NpcType, NPC> NPCs = new Dictionary<NpcType, NPC>();
    public static int matchesMade = 0; // is it enough?

    public static void HandleNpcShot(NPC npc)
    {
        if (!NPCs.ContainsKey(npc.type))
            NPCs.Add(npc.type, npc);
        else if (NPCs[npc.type] != npc)
        {
            // TODO: match successful event
            NPC other = NPCs[npc.type];
            other.DestroyNpc();
            matchesMade++;

            NPCs.Remove(npc.type);
            npc.DestroyNpc();
        }

    }

    public void HandleMatchTimeOut(NPC npc)
    {
        NPCs.Remove(npc.type);
    }
}

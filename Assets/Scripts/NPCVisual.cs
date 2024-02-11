using UnityEngine;

public class NPCVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] sprites;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        SetNpcSprite(NpcController.BehaviourState.patrol);
    }

    public void SetNpcSprite(NpcController.BehaviourState state)
    {
        switch (state)
        {
            case NpcController.BehaviourState.patrol:
                spriteRenderer.color = Color.white;
                spriteRenderer.sprite = sprites[0];
                break;
            case NpcController.BehaviourState.dazzled:
                spriteRenderer.sprite = sprites[1];
                break;
            case NpcController.BehaviourState.inLove:
                spriteRenderer.sprite = sprites[2];
                break;
            case NpcController.BehaviourState.sad:
                spriteRenderer.color = Color.blue;
                break;
            case NpcController.BehaviourState.honeymoon:
                break;
            default:
                break;
        }
    }
}

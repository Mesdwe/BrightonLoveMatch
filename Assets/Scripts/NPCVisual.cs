using UnityEngine;

public class NPCVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    Sprite[] sprites;

    // KH
    [SerializeField] FollowTransform markedVisualsObj;
    private GameObject markedVisualsHolder;
    private bool instantiatedMarkedVisuals;

    [SerializeField] FollowTransform loveVisualsObj;
    private GameObject loveVisualsHolder;
    private bool instantiatedLoveVisuals;

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

                if (!instantiatedMarkedVisuals)
                {
                    instantiatedMarkedVisuals = true;
                    FollowTransform v = Instantiate(markedVisualsObj, transform.position, Quaternion.identity);
                    v.SetFollow(transform);
                    markedVisualsHolder = v.gameObject;
                }
                break;
            case NpcController.BehaviourState.inLove:
                //spriteRenderer.sprite = sprites[2];
                if (markedVisualsHolder != null)
                    Destroy(markedVisualsHolder);

                if (!instantiatedLoveVisuals)
                {
                    instantiatedLoveVisuals = true;
                    FollowTransform v = Instantiate(loveVisualsObj, transform.position, Quaternion.identity);
                    v.SetFollow(transform);
                    loveVisualsHolder = v.gameObject;
                }
                break;
            case NpcController.BehaviourState.sad:
                spriteRenderer.color = Color.blue;
                if (markedVisualsHolder != null)
                    Destroy(markedVisualsHolder);
                break;
            case NpcController.BehaviourState.honeymoon:
                break;
            default:
                break;
        }
    }
}

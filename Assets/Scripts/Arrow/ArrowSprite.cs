using UnityEngine;

public class ArrowSprite : MonoBehaviour
{
    private Arrow arrow;

    private void Awake()
    {
        arrow = transform.parent.gameObject.GetComponent<Arrow>();
    }
    private void OnBecameInvisible()
    {
        if (arrow != null)
        {
            arrow.ArrowMiss?.Invoke();
        }
    }
}

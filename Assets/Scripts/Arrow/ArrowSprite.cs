using UnityEngine;

public class ArrowSprite : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(transform.parent.gameObject);
    }
}

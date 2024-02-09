using UnityEngine;

public class Player : MonoBehaviour
{
    // controller
    // sprite
    // score

    [HideInInspector] public int velocity;
    public float speed;

    private void Update()
    {
        gameObject.transform.position += speed * velocity * Vector3.up * Time.deltaTime;
    }
}

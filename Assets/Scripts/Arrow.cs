using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int direction = 0;
    [SerializeField] float speed = 10f;

    public void ShootArrow(int dir)
    {
        direction = dir;
    }

    private void Update()
    {
        gameObject.transform.position += 10 * direction * Vector3.right * Time.deltaTime;
    }
}

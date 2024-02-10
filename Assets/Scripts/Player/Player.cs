using UnityEngine;

public class Player : MonoBehaviour
{
    // controller
    // sprite
    // score
    public int arrowDirection;
    [HideInInspector] public int moveDirection;
    public float speed;
    [Header("Arrow related")]
    public bool shoot;
    private float arrowCooldown;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnTransform;

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        gameObject.transform.position += speed * moveDirection * Vector3.up * Time.deltaTime;
        // TODO: prevent going off screen
    }

    public void Shoot()
    {
        if (!shoot) return;
        Arrow newArrow = Instantiate(arrow, arrowSpawnTransform);
        newArrow.transform.parent = null;
        newArrow.ShootArrow(arrowDirection);

        shoot = false;
    }
}

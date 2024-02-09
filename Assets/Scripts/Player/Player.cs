using UnityEngine;

public class Player : MonoBehaviour
{
    // controller
    // sprite
    // score
    public int direction;
    [HideInInspector] public int velocity;
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
        gameObject.transform.position += speed * velocity * Vector3.up * Time.deltaTime;
    }
    public void Shoot()
    {
        if (!shoot) return;
        // instantiate arrow
        Arrow newArrow = Instantiate(arrow, arrowSpawnTransform);
        newArrow.transform.parent = null;
        newArrow.ShootArrow(direction);
        Destroy(newArrow.gameObject, 3f);
        shoot = false;
        // shoot in a direction
    }
}

using System;
using System.Collections;
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
    private bool canFire = true;
    [SerializeField] private float arrowCooldown = 2f;
    [SerializeField] private Arrow arrow;
    [SerializeField] private Transform arrowSpawnTransform;
    public event Action PlayerShoot;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        gameObject.transform.position += speed * moveDirection * Vector3.up * Time.deltaTime;
        // TODO: prevent going off screen
    }

    IEnumerator ArrowCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(arrowCooldown);
        canFire = true;
    }

    public void Shoot()
    {
        if (!canFire) return;
        PlayerShoot?.Invoke();
        Arrow newArrow = Instantiate(arrow, arrowSpawnTransform);
        newArrow.transform.parent = null;
        newArrow.ShootArrow(arrowDirection);
        StartCoroutine(ArrowCooldown());

    }
}

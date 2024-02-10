using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Player player = transform.parent.GetComponent<Player>();
        player.PlayerShoot += OnPlayerShoot;
    }
    public void OnPlayerShoot()
    {
        animator.SetTrigger("startFire");
    }
}

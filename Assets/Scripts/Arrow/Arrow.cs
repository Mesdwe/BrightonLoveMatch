using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int direction = 0;
    [SerializeField] float speed = 10f;
    public Action ArrowMiss;
    public Action ArrowHit;
    // not enough time for proper callback set up, do it here instead :(
    public AudioSource[] audioSource;
    private bool hit = false;

    private void Awake()
    {
        ArrowMiss += DestroyArrow;
    }

    public void ShootArrow(int dir)
    {
        direction = dir;
        audioSource[0].Play();
    }

    public void DestroyArrow()
    {
        audioSource[1].Play();
        Destroy(gameObject, 1);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            hit = true;
            // hacky
            transform.localScale = Vector3.zero;
            Destroy(gameObject, 0.8f);
        }

    }

    private void Update()
    {
        if (!hit)
            gameObject.transform.position += speed * direction * Vector3.right * Time.deltaTime;
    }
}

// KHOGDEN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] Transform follow;

    [Header("Offset Position")]
    [SerializeField] float x = 0f;
    [SerializeField] float y = 0.5f;

    // KH - Called upon every frame.
    void Update()
    {
        if(follow != null)
            transform.position = new Vector2(x, y);
        else
            Destroy(gameObject);
    }

    // KH - Make sure to call this when instantiating a follow transform.
    public void SetFollow(Transform followTransform)
    {
        follow = followTransform;
    }
}
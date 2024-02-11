// KHOGDEN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destroy : MonoBehaviour
{
    [SerializeField] float gameObjectLifetime = 3f;

    // KH - Called upon the first frame.
    void Start()
    {
        Destroy(gameObject, gameObjectLifetime);
    }
}
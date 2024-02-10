// KHOGDEN
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Text scoreDisplay;
    [SerializeField] Text timeDisplay;

    // KH - Called upon the first frame.
    void Start()
    {

    }

    // KH - Called upon every frame.
    void Update()
    {
        //scoreDisplay = 
        timeDisplay = Timer.instance.GetTime();
    }
}
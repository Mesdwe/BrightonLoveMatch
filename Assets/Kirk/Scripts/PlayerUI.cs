// KHOGDEN
using TMPro;
using UnityEngine;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI timeDisplay;

    // KH - Called upon the first frame.
    void Start()
    {

    }

    // KH - Called upon every frame.
    void Update()
    {
        scoreDisplay.text = MatchManager.matchesMade.ToString();
        timeDisplay.text = Timer.instance.GetTime().ToString();
    }
}
// KHOGDEN
using TMPro;
using UnityEngine;
public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI timeDisplay;

    // KH - Called upon every frame.
    void Update()
    {
        //scoreDisplay.text = MatchManager.matchesMade.ToString();
        scoreDisplay.text = LevelManager.instance.GetScore().ToString();
        timeDisplay.text = Timer.instance.TimeDisplay();
    }
}
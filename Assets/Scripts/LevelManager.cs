using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] GameObject[] players = new GameObject[2];
    [SerializeField] int score;
    private bool levelFinish;

    // KH - Called before 'void Start()'
    void Awake()
    {
        instance = this;
    }

    public void StartLevel()
    {
        // KH - Activate player gameobjects.
        foreach (GameObject player in players)
            player.SetActive(true);

        // KH - Set the timer and make it go down.
        Timer.instance.ResetTime();
        Timer.instance.PauseTime(false);

        ResetScore();

    }

    public void FinishLevel()
    {
        levelFinish = true;
        Timer.instance.PauseTime(true);
        LevelFinishUI.instance.Display(true);
    }

    private void Update()
    {
        // KH - Finish level session when timer reaches zero.
        if (Timer.instance.GetTime() == 0f && !IsLevelFinish())
            FinishLevel();
    }

    // KH - Increase the player score.
    public void AddScore(int increase)
    {
        score += increase;
    }

    // KH - Reset the player score back to zero.
    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public bool IsLevelFinish()
    {
        return levelFinish;
    }
}

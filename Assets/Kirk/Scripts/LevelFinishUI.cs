// KHOGDEN
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFinishUI : MonoBehaviour
{
    public static LevelFinishUI instance;

    [SerializeField] Text scoreDisplay;
    private GameObject container;

    // KH - Called before 'void Start()'.
    void Awake()
    {
        instance = this;
        container = transform.GetChild(0).gameObject;
    }

    // KH - Toggle whether the finish UI is displayed or not.
    public void Display(bool toggle)
    {
        container.SetActive(toggle);

        if (toggle)
            scoreDisplay.text = LevelManager.instance.GetScore().ToString();
    }

    // KH - Load the user into a different scene.
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
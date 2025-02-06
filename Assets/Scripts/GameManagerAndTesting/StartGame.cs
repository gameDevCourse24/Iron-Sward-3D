using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGameScene);
    }

    void StartGameScene()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("InstructionsScene"); // שם הסצנה השנייה (ההוראות)
    }
}

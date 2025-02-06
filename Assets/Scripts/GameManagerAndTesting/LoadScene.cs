using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField, Tooltip("The scene you will be transported to when you win and press the /NextLevel/ button")] private string nextSceneName;

    public void GoToNextLevel()//This function is public so I can call it from the victory panel button.
    {
        pprint.p("nextLevelJustClicked", this);
        SceneManager.LoadScene(nextSceneName);
    }
}

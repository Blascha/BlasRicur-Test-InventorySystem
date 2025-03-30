using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static bool PlayingSave = false;
    [SerializeField] Button loadButton;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Wave") == 0)
            loadButton.interactable = false;
    }

    public void NewGame()
    {
        PlayingSave = false;
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        PlayingSave = true;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

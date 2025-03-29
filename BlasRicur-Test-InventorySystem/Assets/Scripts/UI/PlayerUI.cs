using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text life;
    [SerializeField] CanvasGroup lostGroup;
    [SerializeField] CanvasGroup wonGroup;
    public static PlayerUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLife(int newLife)
    {
        life.text = "" + newLife;
    }

    public void Lost()
    {
        StartCoroutine(OnLoose());
    }

    public void Won()
    {
        StartCoroutine(OnWon());
    }

    IEnumerator OnLoose()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        for(float i = 0; i< 1; i += 0.02f)
        {
            lostGroup.alpha = i;
            yield return wait;
        }

        lostGroup.blocksRaycasts = true;
    }

    IEnumerator OnWon()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        for (float i = 1; i > 0; i -= 0.02f)
        {
            wonGroup.alpha = i;
            yield return wait;
        }

        wonGroup.blocksRaycasts = true;
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        SpawnManager.NextWave = () => { SpawnManager.Wave++; };
    }
}

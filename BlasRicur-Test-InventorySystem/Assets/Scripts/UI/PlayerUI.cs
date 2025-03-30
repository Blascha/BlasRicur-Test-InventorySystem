using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text wave;
    [SerializeField] TMP_Text life;
    [SerializeField] CanvasGroup lostGroup;
    [SerializeField] CanvasGroup wonGroup;
    public static PlayerUI Instance;

    private void Awake()
    {
        SpawnManager.Wave = MainMenu.PlayingSave ? Mathf.Max(PlayerPrefs.GetInt("Wave"), 1) : 1;

        Instance = this;
        UpdateWave();
    }

    public void UpdateLife(int newLife)
    {
        life.text = "" + newLife;
    }

    public static void UpdateWave()
    {
        Instance.wave.text = "Wave: " + SpawnManager.Wave;

        if (SpawnManager.Wave > 1)
            Instance.GetComponent<AudioSource>().Play();
    }

    public void Lost()
    {
        StartCoroutine(OnLoose());
    }

    public void Won()
    {
        ModelPlayer.Instance.OnWin();
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
        for (float i = 0; i < 1; i += 0.02f)
        {
            wonGroup.alpha = i;
            yield return wait;
        }

        wonGroup.blocksRaycasts = true;
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        SpawnManager.NextWave = () => 
        {
            SpawnManager.Wave++;
            UpdateWave();
        };
    }
}

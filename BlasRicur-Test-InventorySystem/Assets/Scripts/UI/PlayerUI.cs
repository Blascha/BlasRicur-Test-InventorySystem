using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text life;
    public static PlayerUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLife(int newLife)
    {
        life.text = "" + newLife;
    }
}

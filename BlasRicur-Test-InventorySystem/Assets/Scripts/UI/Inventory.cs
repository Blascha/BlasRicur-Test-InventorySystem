using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class Inventory : MonoBehaviour , IScreenObject
{
    public static Inventory Instance;
    CanvasGroup group;

    public void OnTurnOffScreen()
    {
        StopAllCoroutines();
        StartCoroutine(TurnOff());
    }

    public void OnTurnOnScreen()
    {
        StopAllCoroutines();
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);

        for(float i = 0; i <= 1; i+= 0.2f)
        {
            group.alpha = i;
            yield return wait;
        }

        group.blocksRaycasts = true;
        group.interactable = true;
    }

    IEnumerator TurnOff()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);

        for (float i = 1; i >= 0; i -= 0.2f)
        {
            group.alpha = i;
            yield return wait;
        }

        group.blocksRaycasts = false;
        group.interactable = false;
    }


    private void Awake()
    {
        Instance = this;
        group = GetComponent<CanvasGroup>();
        ScreenManager.AddObjectToScreen(ScreenManager.Screens.Menus, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

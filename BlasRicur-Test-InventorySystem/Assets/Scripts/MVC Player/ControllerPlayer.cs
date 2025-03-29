using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour, IScreenObject
{
    [SerializeField] ModelPlayer model;
    [SerializeField] Transform cursor;

    float lastShot;
    public float ROF;
    public float ROFMultiplyier = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (model == null)
            model = GetComponent<ModelPlayer>();

        ScreenManager.AddObjectToScreen(ScreenManager.Screens.Game, this);
        model.Controler = this;
    }

    bool canFire = true;
    public void OnTurnOnScreen()
    {
        canFire = true;
    }

    public void OnTurnOffScreen()
    {
        canFire = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        model.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Update()
    {
        cursor.localPosition = 2 * ((Input.mousePosition / (Screen.width)) - Vector3.one * .5f) + Vector3.forward * 10;

        if (canFire)
        {
            lastShot += Time.deltaTime * ROFMultiplyier;

            if (Input.GetButtonDown("Fire1") && lastShot >= ROF)
            {
                lastShot = 0;
                model.Fire((cursor.position - transform.position).normalized);
            }
        }

        if (Input.GetButtonDown("Inventory"))
        {
            if (ScreenManager.activeScreen == ScreenManager.Screens.Menus)
                ScreenManager.ChangeScreen(ScreenManager.Screens.Game);
            else
                ScreenManager.ChangeScreen(ScreenManager.Screens.Menus);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Inventory.Instance.MakeNewItem(SymbolManager.GetType("No Way this is a Symbol"));
        }
    }
}

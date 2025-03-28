using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    [SerializeField] ModelPlayer model;
    [SerializeField] Transform cursor;

    // Start is called before the first frame update
    void Start()
    {
        if (model == null)
            model = GetComponent<ModelPlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        model.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Update()
    {
        cursor.localPosition = 2 * ((Input.mousePosition/(Screen.width)) - Vector3.one * .5f) + Vector3.forward * 10;

        if (Input.GetButtonDown("Fire1"))
        {
            model.Fire((cursor.position - transform.position).normalized);
        }

        if (Input.GetButtonDown("Inventory"))
        {
            if(ScreenManager.activeScreen == ScreenManager.Screens.Menus)
            {
                ScreenManager.ChangeScreen(ScreenManager.Screens.Game);
                return;
            }

            ScreenManager.ChangeScreen(ScreenManager.Screens.Menus);
        }
    }
}

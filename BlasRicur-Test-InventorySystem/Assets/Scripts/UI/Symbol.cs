using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Symbol : MonoBehaviour
{
    public SymbolType MySymbolType;
    public static Symbol ActualDraggedSymbol;
    public Slot mySlot;
    GameObject showSelection;

    // Start is called before the first frame update
    public void SetSymbolType(SymbolType type)
    {
        MySymbolType = type;
        GetComponent<Image>().sprite = MySymbolType.sprite;

        GameObject circles = Resources.Load<GameObject>("Area of Effect");
        showSelection = new GameObject("What Will I Affect");
        showSelection.transform.SetParent(transform);

        foreach(var i in type.effectArea)
        {
            GameObject actualCircle = Instantiate(circles);
            actualCircle.transform.SetParent(showSelection.transform);
            actualCircle.GetComponent<RectTransform>().anchoredPosition = new Vector2(i.x * 250, i.y* -250);
        }

        showSelection.SetActive(false);
    }

    public void StartDrag()
    {
        //Well, sometimes, I want to change the item on a slot for the one being used. So I use this.
        if (ActualDraggedSymbol != null)
        {
            mySlot.OnPressed();
            return;
        }
        
        //I actually set everyting to be dragged
        ActualDraggedSymbol = this;
        GetComponent<Image>().raycastTarget = false;
        mySlot.assignedSymbol = null;
        showSelection.SetActive(true);

        //This is so that is the one on top of everything
        transform.SetSiblingIndex(transform.parent.childCount - 1);

        //This will start the drag. Why use this instead of a delegate on Update? ´cause it is easyier to stop, and I don´t waste cycles calling an empty function
        StartCoroutine(BeingDragged());
    }

    public void StopDrag()
    {
        //I will recalculate all symbol effects after this
        Inventory.Instance.OnChangeSymbol();
        showSelection.SetActive(false);

        //Make it pressable + not moving any more
        GetComponent<Image>().raycastTarget = true;
        StopAllCoroutines();
    }

    IEnumerator BeingDragged()
    {
        //This will mostly just set the position to where the cursor is on the UI.
        RectTransform rect = GetComponent<RectTransform>();

        while (true)
        {
            rect.anchoredPosition =  (Vector2)(Input.mousePosition) - new Vector2(Screen.width,Screen.height) * .5f ;
            yield return null;
        }
    }
}

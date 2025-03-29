using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Symbol assignedSymbol;
    public float actualMultiplyier = 1;

    public void OnPressed()
    {
        //This is to keep track of the selected symbol on press time and to say "you are home in this slot"
        Symbol originalSymbol = Symbol.ActualDraggedSymbol;
        Symbol.ActualDraggedSymbol = null;

        //If I pressed it while having one, I will start carrying the one I had
        if (assignedSymbol != null)
            assignedSymbol.StartDrag();

        //Early return if I press the Slot while not having anything
        if (originalSymbol == null) return;

        //I set the stuff to the new symbol
        assignedSymbol = originalSymbol;
        assignedSymbol.StopDrag();
        assignedSymbol.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        assignedSymbol.mySlot = this;
    }

    public void Destroy()
    {
        assignedSymbol = Symbol.ActualDraggedSymbol;
        assignedSymbol.StopDrag();
        Destroy(assignedSymbol.gameObject);
        assignedSymbol = null;
    }
}

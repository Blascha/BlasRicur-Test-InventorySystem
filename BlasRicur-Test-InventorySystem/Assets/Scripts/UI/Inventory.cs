using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent (typeof(CanvasGroup))]
public class Inventory : MonoBehaviour , IScreenObject
{
    public static Inventory Instance;
    CanvasGroup group;
    [SerializeField] GameObject[] slots;
    Slot[,] actualSlots;
    [SerializeField] Slot[] nonEffectingSlots;
    [SerializeField] GameObject symbol;
    [SerializeField] TMP_Text statsInfo;

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

        //Overcomplicated as in my design I wanted to use just a 3x3 grid. But you never know (maybe an upgrade to a 4x4, 5x5)
        int slotsSide = (int)Mathf.Sqrt(slots.Length);
        actualSlots = new Slot[slotsSide, slotsSide];

        for (int x = 0; x < slotsSide; x++)
        {
            for (int y = 0; y < slotsSide; y++)
            {
                actualSlots[x, y] = slots[y * 3 + x].GetComponent<Slot>();
            }
        }

        //Should be like this:
        /* |---| |---| |---|
         * | 1 | | 2 | | 3 |
         * |---| |---| |---|
         * 
         * |---| |---| |---|
         * | 4 | | 5 | | 6 |
         * |---| |---| |---|
         * 
         * |---| |---| |---|
         * | 7 | | 8 | | 9 |
         * |---| |---| |---|
         */
    }

    public void OnChangeSymbol()
    {
        float newDamageFire = 1;
        float newShotResilienceRock = 1;
        float newRateOfFireElectricity = 1;
        float newPlayerLifePlants = 1;
        float newPlayerKnockbackEarth = 1;
        float newEnemyKnockBackWater = 1;

        foreach (Slot s in actualSlots)
            s.actualMultiplyier = 1;

        //I will check all the slots
        for (int x = 0; x < actualSlots.GetLength(0); x++)
        {
            for (int y = 0; y < actualSlots.GetLength(1); y++)
            {
                //There are empty slots, so I will skip over those
                if (actualSlots[x, y].assignedSymbol == null) continue;

                //I save the symbol here
                SymbolType actualSymbol = actualSlots[x, y].assignedSymbol.MySymbolType;

                //I will affect each offset here
                foreach (var offset in actualSymbol.effectArea)
                {
                    //I check the offset is inside the slots
                    if (x + offset.x >= actualSlots.GetLength(0) || x + offset.x < 0 || y + offset.y >= actualSlots.GetLength(1) || y + offset.y < 0) continue;
                    if (actualSlots[x + offset.x, y + offset.y].assignedSymbol == null) continue;

                    //With this, I basically go to the offset of the area and affect the symbol there
                    float finalMultiplyier = 1;

                    var elements = (actualSymbol.elements, actualSlots[x + offset.x, y + offset.y].assignedSymbol.MySymbolType.elements);

                    if (SymbolType.ElementToElementEffect.ContainsKey(elements))
                    {
                        finalMultiplyier = SymbolType.ElementToElementEffect[elements];
                    }

                    actualSlots[x + offset.x, y + offset.y].actualMultiplyier *= finalMultiplyier;
                }
            }
        }

        //I will set the new stats
        for (int x = 0; x < actualSlots.GetLength(0); x++)
        {
            for (int y = 0; y < actualSlots.GetLength(1); y++)
            {
                //There are empty slots, so I will skip over those
                if (actualSlots[x, y].assignedSymbol == null) continue;

                SymbolType actualSymbol = actualSlots[x, y].assignedSymbol.MySymbolType;

                float effect = actualSlots[x, y].actualMultiplyier;

                switch (actualSymbol.elements)
                {
                    case Elements.Fire:
                        newDamageFire *= effect;
                        break;

                    case Elements.Electricity:
                        newRateOfFireElectricity *= effect;
                        break;

                    case Elements.Water:
                        newEnemyKnockBackWater *= effect;
                        break;

                    case Elements.Rock:
                        newShotResilienceRock *= effect;
                        break;

                    case Elements.Plant:
                        newPlayerLifePlants *= effect;
                        break;

                    case Elements.Earth:
                        newPlayerKnockbackEarth *= effect;
                        break;
                }
            }
        }

        string inventoryStats = $"HP: {GetStrengthColor(newPlayerLifePlants)}\nDamage: {GetStrengthColor(newDamageFire)}\nROF:{GetStrengthColor(newRateOfFireElectricity)} \n Player Knockback: {GetStrengthColor(newPlayerKnockbackEarth)}\n Enemy Knockback: {GetStrengthColor(newEnemyKnockBackWater)}\n Shot Resilience: {GetStrengthColor(newShotResilienceRock)}";
        statsInfo.text = inventoryStats;

        //I actually set the Stats
        ModelPlayer.Instance.SetNewROFMultiplyier(newRateOfFireElectricity);

    }
    string GetStrengthColor(float strength)
    {
        switch(strength)
        {
            case < 1:
                return "<color=red>" + strength +" X</color>";

            case > 1:
                return "<color=green>" + strength + "X</color>";

            default:
                return "" + strength;
        }
    }

    public void MakeNewItem(SymbolType type)
    {
        foreach(var i in nonEffectingSlots)
        {
            if(i.assignedSymbol == null)
            {
                GameObject newSymbol = Instantiate(symbol);
                Symbol newSymbolComponent = newSymbol.GetComponent<Symbol>();

                newSymbol.transform.SetParent(transform);
                i.assignedSymbol = newSymbolComponent;

                newSymbolComponent.SetSymbolType(type);
                newSymbolComponent.GetComponent<RectTransform>().anchoredPosition = i.GetComponent<RectTransform>().anchoredPosition;
                newSymbolComponent.mySlot = i;
                break;
            }
        }
    }
}

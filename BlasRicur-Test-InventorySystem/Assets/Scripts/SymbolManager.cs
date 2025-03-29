using System;
using UnityEngine;
using System.Linq;

public class SymbolManager : MonoBehaviour
{
    public static void FindAllSymbolTypes()
    {
        Debug.Log("All SymbolTypes are");

        var allSymbols = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsSubclassOf(typeof(SymbolType))).ToArray();

        foreach(var i in allSymbols)
        {
            Debug.Log(i.Name);
        }
    }
}

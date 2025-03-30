using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class SymbolManager
{
    public static void FindAllSymbolTypes()
    {
        SymbolsAndNames = new Dictionary<string, SymbolType>();
        var allSymbolsPaths = Resources.LoadAll<SymbolType>("Symbols");

        foreach (var i in allSymbolsPaths)
        {
            SymbolsAndNames.Add(i.name,i);
        }
    }

    static Dictionary<string, SymbolType> SymbolsAndNames;

    public static SymbolType GetType(string name, bool nullOrRandom = false)
    {
        if (SymbolsAndNames == null)
            FindAllSymbolTypes();

        if (SymbolsAndNames.ContainsKey(name))
            return SymbolsAndNames[name];

        //Maybe I want to get an exact ot maybe I made a Typo. Maybe I´m looking for one that doesn´t exist or I am looking for something
        if (nullOrRandom)
            return null;

        //I will return a random Symbol
        var RandomKey = SymbolsAndNames.Keys.ToArray()[Random.Range(0, SymbolsAndNames.Keys.Count - 1)];
        return SymbolsAndNames[RandomKey];
    }
}

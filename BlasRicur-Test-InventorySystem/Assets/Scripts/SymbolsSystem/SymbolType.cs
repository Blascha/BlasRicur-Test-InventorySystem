using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymbolType", menuName = "New Symbol", order = 1)]
public class SymbolType : ScriptableObject
{
    public Elements elements;
    public List<Vector2Int> effectArea;
    public Sprite sprite;

    //These is the type chart from Pokemon basically
    public static Dictionary<(Elements, Elements), float> ElementToElementEffect = new Dictionary<(Elements, Elements), float>
    {
        //Fire to others
        { (Elements.Fire,Elements.Plant),0.5f },

        //Water to others
        { (Elements.Water,Elements.Fire),0.5f },
        { (Elements.Water,Elements.Electricity),2 },
        { (Elements.Water,Elements.Plant),2 },
        { (Elements.Water,Elements.Rock),0.5f },

        //Electricity to others
        { (Elements.Electricity,Elements.Plant),.5f },
        { (Elements.Electricity,Elements.Fire),2 },

        //Earth to others
        { (Elements.Earth,Elements.Plant),3 },
        { (Elements.Earth,Elements.Electricity),.5f },
        { (Elements.Earth,Elements.Fire),.5f },
        { (Elements.Earth,Elements.Rock),.5f },

        //Plant to others
        { (Elements.Plant,Elements.Fire),2 },
        { (Elements.Plant,Elements.Water),.5f },
        { (Elements.Plant,Elements.Rock),.5f },
    };
}

public enum Elements
{
    Fire,
    Water,
    Earth,
    Rock,
    Electricity,
    Plant
}

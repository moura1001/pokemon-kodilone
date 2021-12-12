using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new Move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string moveName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] PokemonType type;
    [SerializeField] MoveCategory category;

    [SerializeField] ushort power;
    [SerializeField] ushort accuracy;
    [SerializeField] ushort pp;
    [SerializeField] ushort maxPP;

    public string Name
    {
        get { return moveName; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonType Type
    {
        get { return type; }
    }

    public MoveCategory Category
    {
        get { return category; }
    }

    public ushort Power
    {
        get { return power; }
    }

    public ushort Accuracy
    {
        get { return accuracy; }
    }

    public ushort PP
    {
        get { return pp; }
    }

    public ushort MaxPP
    {
        get { return maxPP; }
    }
}

public enum MoveCategory
{
    Physical,
    Special,
    Status
}

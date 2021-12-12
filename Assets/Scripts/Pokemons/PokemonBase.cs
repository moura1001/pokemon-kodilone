using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject
{
    [SerializeField] string pokeName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    //Base Stats
    [SerializeField] ushort hp;
    [SerializeField] ushort attack;
    [SerializeField] ushort defense;
    [SerializeField] ushort speed;
    [SerializeField] ushort special;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name
    {
        get { return pokeName; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public ushort Hp
    {
        get { return hp; }
    }

    public ushort Attack
    {
        get { return attack; }
    }

    public ushort Defense
    {
        get { return defense; }
    }

    public ushort Speed
    {
        get { return speed; }
    }

    public ushort Special
    {
        get { return special; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] ushort level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public ushort Level
    {
        get { return level; }
    }
}
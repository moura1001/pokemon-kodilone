using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    PokemonBase _base;
    ushort level;

    ushort hpIV;
    ushort attackIV;
    ushort defenseIV;
    ushort speedIV;
    ushort specialIV;

    ushort hpEV;
    ushort attackEV;
    ushort defenseEV;
    ushort speedEV;
    ushort specialEV;

    public Pokemon(PokemonBase _base, ushort level,
                    ushort attackIV, ushort defenseIV, ushort speedIV, ushort specialIV)
    {
        this._base = _base;
        this.level = level;

        this.attackIV = attackIV;
        this.defenseIV = defenseIV;
        this.speedIV = speedIV;
        this.specialIV = specialIV;

        int bit0 = (short) (specialIV & 1);
        int bit1 = (short)((speedIV & 1) << 1);
        int bit2 = (short)((defenseIV & 1) << 2);
        int bit3 = (short)((attackIV & 1) << 3);

        hpIV = (ushort) (bit0 | bit1 | bit2 | bit3);
    }

    public ushort Hp
    {
        get {
            return (ushort)
            (
                Mathf.Floor(
                    ( ((_base.Hp + hpIV)*2 + Mathf.Floor(Mathf.Ceil(Mathf.Sqrt(hpEV)) / 4)) * level) / 100
                ) + level + 10
            );
        }
    }

    public ushort Attack
    {
        get
        {
            return (ushort)
            (
                Mathf.Floor(
                    ( ((_base.Attack + attackIV) * 2 + Mathf.Floor(Mathf.Ceil(Mathf.Sqrt(attackEV)) / 4)) * level) / 100
                ) + 5
            );
        }
    }

    public ushort Defense
    {
        get
        {
            return (ushort)
            (
                Mathf.Floor(
                    ( ((_base.Defense + defenseIV) * 2 + Mathf.Floor(Mathf.Ceil(Mathf.Sqrt(defenseEV)) / 4)) * level) / 100
                ) + 5
            );
        }
    }

    public ushort Speed
    {
        get
        {
            return (ushort)
            (
                Mathf.Floor(
                    ( ((_base.Speed + speedIV) * 2 + Mathf.Floor(Mathf.Ceil(Mathf.Sqrt(speedEV)) / 4)) * level) / 100
                ) + 5
            );
        }
    }

    public ushort Special
    {
        get
        {
            return (ushort)
            (
                Mathf.Floor(
                    ( ((_base.Special + specialIV) * 2 + Mathf.Floor(Mathf.Ceil(Mathf.Sqrt(specialEV)) / 4)) * level) / 100
                ) + 5
            );
        }
    }
}

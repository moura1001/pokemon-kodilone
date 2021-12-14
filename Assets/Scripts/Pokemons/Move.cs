using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveBase Base { get; set; }
    public ushort PP { get; set; }

    public Move(MoveBase _base)
    {
        Base = _base;
        PP = _base.PP;
    }
}

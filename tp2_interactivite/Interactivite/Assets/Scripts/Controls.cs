using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlInput
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    BOMB
}

public class Controls {

    Dictionary<ControlInput, string>[] BaseControl = new Dictionary<ControlInput, string>[]
    {
    new Dictionary<ControlInput, string> {
        { ControlInput.LEFT, "left"  },
        { ControlInput.RIGHT, "right" },
        { ControlInput.UP, "up" },
        { ControlInput.DOWN, "down" },
        { ControlInput.BOMB, "space" }
    },
    new Dictionary<ControlInput, string> {
        { ControlInput.UP, "w" },
        { ControlInput.DOWN, "s" },
        { ControlInput.LEFT, "a" },
        { ControlInput.RIGHT, "d" },
        { ControlInput.BOMB, "f" }
    }
    };
    

    public Dictionary<ControlInput, string> GetControl(int nb)
    {
       return BaseControl[nb];
    }
}

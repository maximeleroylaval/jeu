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

public static class Controls {

    private static Dictionary<ControlInput, string> control1 = new Dictionary<ControlInput, string> {
        { ControlInput.LEFT, "left"  },
        { ControlInput.RIGHT, "right" },
        { ControlInput.UP, "up" },
        { ControlInput.DOWN, "down" },
        { ControlInput.BOMB, "space" }
    };
    private static Dictionary<ControlInput, string> control2 = new Dictionary<ControlInput, string> {
        { ControlInput.UP, "w" },
        { ControlInput.DOWN, "s" },
        { ControlInput.LEFT, "a" },
        { ControlInput.RIGHT, "d" },
        { ControlInput.BOMB, "f" }
    };

    public static void SetCommand(int nbPlayer, KeyValuePair<ControlInput, string> command)
    {
        Dictionary<ControlInput, string> New = new Dictionary<ControlInput, string>();
        if (nbPlayer == 0)
        {
            New = Controls.Control1;
        }
        if (nbPlayer == 1)
        {
            New = Controls.Control2;
        }

        New[command.Key] = command.Value;

    }


    public static Dictionary<ControlInput, string> Control1
    {
        get
        {
            return control1;
        }
        set
        {
            control1 = value;
        }
    }

    public static Dictionary<ControlInput, string> Control2
    {
        get
        {
            return control2;
        }
        set
        {
            control2 = value;
        }
    }
}

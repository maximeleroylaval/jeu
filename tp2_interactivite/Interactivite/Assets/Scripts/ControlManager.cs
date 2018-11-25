using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour
{

    //Used for singleton
    public static ControlManager CM;

    public Control[] Controls;

    void Awake()
    {
        Controls = new Control[] { new Control(), new Control() };


        //Singleton pattern
        if (CM == null)
        {
            DontDestroyOnLoad(gameObject);
            CM = this;
        }
        else if (CM != this)
        {
            Destroy(gameObject);
        }


        Controls[0].bomb = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Space");
        Controls[0].forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), "UpArrow");
        Controls[0].backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), "DownArrow");
        Controls[0].left = (KeyCode)System.Enum.Parse(typeof(KeyCode), "LeftArrow");
        Controls[0].right = (KeyCode)System.Enum.Parse(typeof(KeyCode), "RightArrow");


        Controls[1].bomb = (KeyCode)System.Enum.Parse(typeof(KeyCode), "F");
        Controls[1].forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), "W");
        Controls[1].backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), "S");
        Controls[1].left = (KeyCode)System.Enum.Parse(typeof(KeyCode), "A");
        Controls[1].right = (KeyCode)System.Enum.Parse(typeof(KeyCode), "D");

    }

    void Start()
    {
        
        
        
    }

    void Update()
    {

    }
}
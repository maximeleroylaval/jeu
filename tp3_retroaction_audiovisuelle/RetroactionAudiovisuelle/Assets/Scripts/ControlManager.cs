using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager CM;

    public Control[] Controls;

    void Awake()
    {
        Controls = new Control[] { new Control(), new Control() };

        if (CM == null)
        {
            //DontDestroyOnLoad(gameObject);
            CM = this;
        }
        else if (CM != this)
            Destroy(gameObject);

        Controls[0].bomb = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("bombKey0", "F"));
        Controls[0].forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey0", "W"));
        Controls[0].backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey0", "S")); 
        Controls[0].left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey0", "A"));
        Controls[0].right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey0", "D"));

        Controls[1].bomb = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("bombKey1", "Space"));
        Controls[1].forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey1", "UpArrow"));
        Controls[1].backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey1", "DownArrow"));
        Controls[1].left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey1", "LeftArrow"));
        Controls[1].right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey1", "RightArrow"));
    }
}
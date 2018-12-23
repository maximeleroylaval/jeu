using UnityEngine;
using UnityEngine.UI;

public class TextAnnouncer : MonoBehaviour {

    public float displayTime = 2f;
    public Text canvasText;


    private bool displaying = false;
    private bool displaying2 = false;

    public void Display(string toDisplay)
    {
        this.DisplayWithTime(toDisplay, this.displayTime);
    }

    public void DisplayWithTime(string toDisplay, float myDisplayTime)
    {
        this.ClearText();
        canvasText.text = toDisplay;
        if (this.displaying)
            this.displaying2 = true;
        else
            displaying = true;
        Invoke("ClearText", myDisplayTime);
    }

    public void ClearText()
    {
        if ((this.displaying && !this.displaying2) || (!this.displaying && this.displaying2) || (!this.displaying && !this.displaying2))
        {
            canvasText.text = string.Empty;
        }
        else
        {
            this.displaying = false;
        }
    }
}

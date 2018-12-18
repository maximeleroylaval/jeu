using UnityEngine;
using UnityEngine.UI;

public class TextAnnouncer : MonoBehaviour {

    public float displayTime = 2f;
    public Text canvasText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Display(string toDisplay)
    {
        this.ClearText();
        canvasText.text = toDisplay;
        Invoke("ClearText", this.displayTime);
    }

    public void ClearText()
    {
        canvasText.text = string.Empty;
    }
}

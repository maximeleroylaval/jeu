using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceGameMenu : MonoBehaviour {

    public GameObject SoloMenu;
    public GameObject OnlineMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Solo()
    {
        this.gameObject.SetActive(false);
        this.SoloMenu.SetActive(true);
    }

    public void Online()
    {
        this.gameObject.SetActive(false);
        this.OnlineMenu.SetActive(true);
    }

    public void BackToGameMenu()
    {
        SceneManager.LoadScene("Scenes/Game");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

    public GameObject MenuPanel;
    public GameObject OptionsPanel;
    public GameObject CommandsPanel;
    public GameObject NewCommandPanel;

    // Use this for initialization
    void Start () {
        MenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        CommandsPanel.SetActive(false);
        NewCommandPanel.SetActive(false);
    }

    public void ShowOptionsPanel()
    {
        MenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }

    public void ShowCommandPanel()
    {
        OptionsPanel.SetActive(false);
        CommandsPanel.SetActive(true);
    }

    public void SetNewCommand(string command)
    {
        NewCommandPanel.SetActive(true);

        if (Input.anyKey)
            {
                Debug.Log(Input.inputString);
            }

        NewCommandPanel.SetActive(false);

    }

    public void Exit()
    {
        Application.Quit();
    }
    public void LaunchGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}

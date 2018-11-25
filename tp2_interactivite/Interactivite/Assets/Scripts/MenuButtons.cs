using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    public GameObject MenuPanel;
    public GameObject OptionsPanel;
    public GameObject CommandsPanel;
    public GameObject NewCommandPanel;
    public GameObject prefabCommandElement;


    
    public void SetNewCommand(ElementCommand el)
    {
        NewCommandPanel.SetActive(true);
        NewCommandPanel.AddComponent<KeyListener>();



        //Controls.SetCommand(0, el)
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

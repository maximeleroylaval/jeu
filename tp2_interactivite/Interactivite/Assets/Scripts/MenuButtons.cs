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

    public Controls ControlManager = new Controls();

    public int nbPlayers = 0;
    public int nbPlayersIA = 0;

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
        CommandsPanel.SetActive(false);
        NewCommandPanel.SetActive(false);
    }

    public void ShowCommandPanel(int nb)
    {
       
        Dictionary<ControlInput, string> control = ControlManager.GetControl(0);

        foreach (KeyValuePair<ControlInput, string> el in control)
        {
            GameObject commandElement = (GameObject)Instantiate(prefabCommandElement);
            commandElement.transform.SetParent(CommandsPanel.transform, false);

            ElementCommand element = commandElement.GetComponent<ElementCommand>();
            element.NameCommand.text = el.Key.ToString();
            element.AtributionCommand.text = el.Value;
        }

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

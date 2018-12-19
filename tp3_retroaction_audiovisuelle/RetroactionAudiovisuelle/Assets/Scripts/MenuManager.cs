using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject OptionsPanel;
    public GameObject CommandsPanel;
    public GameObject SetNewCommandPanel;
    private ParticleSystem particle;

    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    void Start()
    {
        ShowMenuPanel();
        InitKeys();

        particle = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = Input.mousePosition;
            emitParams.velocity = new Vector3(0.0f, 0.0f, -2.0f);
            particle.Emit(emitParams, 10);
            //particle.Stop();
            //particle.Play();
        }
    }

    void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void InitKeys()
    {
        waitingForKey = false;

        int i = 0;
        foreach (Transform child in CommandsPanel.transform)
        {
            if (child.name.Contains("Player"))
            {
                foreach (Transform cc in child)
                {
                    if (cc.name == "ForwardKey")
                        cc.GetComponentInChildren<Text>().text = ControlManager.CM.Controls[i].forward.ToString();
                    else if (cc.name == "BackwardKey")
                        cc.GetComponentInChildren<Text>().text = ControlManager.CM.Controls[i].backward.ToString();
                    else if (cc.name == "LeftKey")
                        cc.GetComponentInChildren<Text>().text = ControlManager.CM.Controls[i].left.ToString();
                    else if (cc.name == "RightKey")
                        cc.GetComponentInChildren<Text>().text = ControlManager.CM.Controls[i].right.ToString();
                    else if (cc.name == "BombKey")
                        cc.GetComponentInChildren<Text>().text = ControlManager.CM.Controls[i].bomb.ToString();
                }
                i++;
            }
        }
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    public void StartAssignment(string key)
    {
        int nb = Int32.Parse(key.Substring(0, 1));
        key = key.Substring(1, key.Length - 1);

        SetNewCommandPanel.SetActive(true);

        if (!waitingForKey)
            StartCoroutine(AssignKey(nb, key));
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    public bool AlreadyAssigned(KeyCode newKey)
    {
        foreach (Control el in ControlManager.CM.Controls)
        {
            if (el.backward == newKey || el.forward == newKey || el.left == newKey || el.right == newKey || el.bomb == newKey)
                return true;
        }
        return false;
    }

    public IEnumerator AssignKey(int i, string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();

        if (AlreadyAssigned(newKey))
        {
            SetNewCommandPanel.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = "This key is already assigned";

            waitingForKey = false;
            StartAssignment(i.ToString() + keyName.ToString().ToLower());
        }
        else
        {
            switch (keyName)
            {
                case "forward":
                    ControlManager.CM.Controls[i].forward = newKey;
                    buttonText.text = ControlManager.CM.Controls[i].forward.ToString();
                    PlayerPrefs.SetString("forwardKey" + i, ControlManager.CM.Controls[i].forward.ToString());
                    break;

                case "backward":
                    ControlManager.CM.Controls[i].backward = newKey;
                    buttonText.text = ControlManager.CM.Controls[i].backward.ToString();
                    PlayerPrefs.SetString("backwardKey" + i, ControlManager.CM.Controls[i].backward.ToString());
                    break;

                case "left":
                    ControlManager.CM.Controls[i].left = newKey;
                    buttonText.text = ControlManager.CM.Controls[i].left.ToString();
                    PlayerPrefs.SetString("leftKey" + i, ControlManager.CM.Controls[i].left.ToString());
                    break;

                case "right":
                    ControlManager.CM.Controls[i].right = newKey;
                    buttonText.text = ControlManager.CM.Controls[i].right.ToString();
                    PlayerPrefs.SetString("rightKey" + i, ControlManager.CM.Controls[i].right.ToString());
                    break;

                case "bomb":
                    ControlManager.CM.Controls[i].bomb = newKey;
                    buttonText.text = ControlManager.CM.Controls[i].bomb.ToString();
                    PlayerPrefs.SetString("bombKey" + i, ControlManager.CM.Controls[i].bomb.ToString());
                    break;
            }
            SetNewCommandPanel.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
            SetNewCommandPanel.SetActive(false);
        }

        yield return null;
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        CommandsPanel.SetActive(false);
        SetNewCommandPanel.SetActive(false);
    }

    public void ShowOptionsPanel()
    {
        MenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        CommandsPanel.SetActive(false);
        SetNewCommandPanel.SetActive(false);
    }

    public void ShowCommandPanel()
    {
        MenuPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        CommandsPanel.SetActive(true);
        SetNewCommandPanel.SetActive(false);
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
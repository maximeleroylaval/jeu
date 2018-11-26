using UnityEngine;
using UnityEngine.UI;

public class MyNetworkManagerHUD : MonoBehaviour {

    public GameObject InGameMenu;
    public GameObject OnlineMenu;
    public GameObject SoloMenu;
    public Text IP;
    public int Port = 1337;

    public bool solo;

    // Use this for initialization
    void Start () {

    }

    public void Display(bool ingame)
    {
        if (this.solo)
            this.SoloMenu.SetActive(!ingame);
        else
            this.OnlineMenu.SetActive(!ingame);
        this.InGameMenu.SetActive(ingame);
    }

    public void StartHost(bool solo)
    {
        this.solo = solo;
        this.Display(true);
        MyNetworkManager Manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
        Manager.networkPort = Port;
        Manager.StartHost();
    }

    public void JoinHost()
    {
        this.Display(true);
        MyNetworkManager Manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
        Manager.networkAddress = IP.text;
        Manager.networkPort = Port;
        Manager.StartClient();
    }

    public void DisconnectHost()
    {
        this.Display(false);
        MyNetworkManager Manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
        Manager.StopHost();
    }

    // Update is called once per frame
    void Update () {
		
	}
}

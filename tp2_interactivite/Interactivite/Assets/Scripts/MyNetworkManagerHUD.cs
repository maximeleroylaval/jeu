using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MyNetworkManagerHUD : MonoBehaviour {

    public GameObject InGameMenu;
    public GameObject OnlineMenu;
    public GameObject SoloMenu;
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
        MyNetworkManager Manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
        Manager.networkPort = Port;
        Manager.StartHost();
        this.solo = solo;
        this.Display(true);
    }

    public void JoinHost()
    {
        MyNetworkManager Manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
        string ip = GameObject.FindWithTag("IP").GetComponent<Text>().text;
        Manager.networkAddress = ip;
        Manager.networkPort = Port;
        Manager.StartClient();
        this.Display(true);
    }

    public void DisconnectHost()
    {
        MyNetworkManager Manager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
        Manager.StopHost();
        this.Display(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

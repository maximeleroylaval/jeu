using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MyNetworkManager : NetworkManager {

    List<NetworkConnection> activeSpawns = new List<NetworkConnection>();

    public class MyMsgType
    {
        public static short Win = MsgType.Highest + 1;
    };

    public class WinMessage : MessageBase
    {
        public bool win;
        public string toDisplay;
    }

    // Use this for initialization
    void Start () {

    }

    public override void OnStartServer()
    {
        int length = GameObject.FindObjectsOfType<NetworkStartPosition>().Length;
        for (int i = 0; i < length; i++)
        {
            activeSpawns.Add(null);
        }
        NetworkServer.RegisterHandler(MyMsgType.Win, OnWin);
        base.OnStartServer();
    }

    public override void OnStartClient(NetworkClient client)
    {
        client.RegisterHandler(MyMsgType.Win, OnWin);
        base.OnStartClient(client);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        GameObject.FindWithTag("GameManager").GetComponent<MyGameManager>().CmdGameStart(true);
    }

    public override void OnClientDisconnect(NetworkConnection connection)
    {
        Destroy(connection.playerControllers[0].gameObject, 1);
        NetworkServer.Destroy(connection.playerControllers[0].gameObject);
        SceneManager.LoadScene("Game");
    }

    //Detect when a client disconnects from the Server
    public override void OnServerDisconnect(NetworkConnection connection)
    {
        for (int i = 0; i < activeSpawns.Count; i++)
        {
            if (activeSpawns[i] == connection)
                activeSpawns[i] = null;
        }
        Destroy(connection.playerControllers[0].gameObject, 1);
        NetworkServer.Destroy(connection.playerControllers[0].gameObject);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        int i = -1;
        for (; i < activeSpawns.Count; i++)
        {
            if (activeSpawns[i + 1] == null)
            {
                i = i + 1;
                break;
            }
        }

        Debug.Log(i);

        if (i != -1)
        {
            NetworkStartPosition[] spawns = GameObject.FindObjectsOfType<NetworkStartPosition>();
            GameObject player = Instantiate(playerPrefab, spawns[i].transform.position, spawns[i].transform.rotation);
            player.GetComponent<Player>().SetPseudo("Player " + (i + 1));
            player.GetComponent<Player>().NumberPlayer = 0;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            activeSpawns[i] = conn;
        }
    }

    public override void OnStopServer()
    {
        for (int i = 0; i < activeSpawns.Count; i++)
        {
            activeSpawns[i] = null;
        }
        SceneManager.LoadScene("Game");
    }

    void Restart()
    {
        GameObject.FindWithTag("GameManager").GetComponent<MyGameManager>().Restart();
    }

    void OnWin(NetworkMessage netMsg)
    {
        WinMessage msg = netMsg.ReadMessage<WinMessage>();
        if (msg.win)
        {
            TextAnnouncer announcer = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<TextAnnouncer>();
            Invoke("Restart", announcer.displayTime);
            announcer.Display(msg.toDisplay);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

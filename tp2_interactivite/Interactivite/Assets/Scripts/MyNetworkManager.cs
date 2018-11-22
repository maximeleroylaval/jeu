using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MyNetworkManager : NetworkManager {

    List<NetworkConnection> activeSpawns = new List<NetworkConnection>();

	// Use this for initialization
	void Start () {
        int length = GameObject.FindObjectsOfType<NetworkStartPosition>().Length;
        for (int i = 0; i < length; i++)
        {
            activeSpawns.Add(null);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnClientDisconnect(NetworkConnection connection)
    {
        Destroy(connection.playerControllers[0].gameObject, 1);
        NetworkServer.Destroy(connection.playerControllers[0].gameObject);
        SceneManager.LoadScene("Game");
    }

    public override void OnStopServer()
    {
        for (int i = 0; i < activeSpawns.Count; i++)
        {
            activeSpawns[i] = null;
        }
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

        if (i != -1)
        {
            NetworkStartPosition[] spawns = GameObject.FindObjectsOfType<NetworkStartPosition>();
            GameObject player = (GameObject)Instantiate(playerPrefab, spawns[i].transform.position, spawns[i].transform.rotation);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            activeSpawns[i] = conn;
        }
    }
}

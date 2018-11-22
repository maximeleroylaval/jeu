using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MyGameManager : NetworkBehaviour
{
    public Transform SpawnPoint;
    public GameObject PlayerIAPrefab;
    public int AmountIA = 0;

    private List<GameObject> playerIAs = new List<GameObject>();
    private bool started = false;
    private bool won = false;

    // Use this for initialization
    void Start()
    {

    }

    public void ToggleIA()
    {
        this.AmountIA = this.AmountIA == 0 ? 1 : 0;
    }

    [Command]
    void CmdSpawnIAs()
    {
        for (int i = 0; i < this.AmountIA; i++)
        {
            GameObject IA = Instantiate(PlayerIAPrefab, SpawnPoint.transform.position, Quaternion.identity);
            this.playerIAs.Add(IA);
            NetworkServer.Spawn(IA);
        }
    }

    [Command]
    public void CmdGameStart(bool isServer)
    {
        if (isServer)
            this.CmdSpawnIAs();
        this.started = true;
    }

    List<GameObject> GetPlayersAlive()
    {
        GameObject[] realPlayers = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] iaPlayers = GameObject.FindGameObjectsWithTag("PlayerIA");

        List<GameObject> playersAlive = new List<GameObject>();

        playersAlive.AddRange(realPlayers);
        playersAlive.AddRange(iaPlayers);

        for (int i = playersAlive.Count - 1; i >= 0; i--)
        {
            if (playersAlive[i].GetComponent<Player>().Dead())
                playersAlive.RemoveAt(i);
        }

        return playersAlive;
    }

    bool Won()
    {
        if (this.GetPlayersAlive().Count == 1)
            return true;

        return false;
    }

    GameObject GetWinner()
    {
        return this.GetPlayersAlive()[0];
    }

    public void Restart()
    {
        NetworkManager manager = GameObject.FindObjectOfType<NetworkManager>();
        manager.StopServer();
        manager.StopHost();
        this.started = false;
        this.won = false;
    }

    void Win()
    {
        this.won = true;
        MyNetworkManager.WinMessage msg = new MyNetworkManager.WinMessage
        {
            win = true,
            toDisplay = "Congratulations, " + this.GetWinner().GetComponent<Player>().GetPseudo() + " won the game !"
        };
        NetworkServer.SendToAll(MyNetworkManager.MyMsgType.Win, msg);
    }

    bool Ready()
    {
        if ((NetworkServer.connections.Count > 1 && this.AmountIA <= 0) ||
            (NetworkServer.connections.Count > 0 && this.AmountIA > 0))
            return true;
        return false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.started && this.Ready())
        {
            if (this.Won() && this.won == false)
            {
                this.Win();
            }
        }
    }
}

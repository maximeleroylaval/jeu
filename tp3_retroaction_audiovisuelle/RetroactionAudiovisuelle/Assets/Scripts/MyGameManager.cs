using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyGameManager : NetworkBehaviour
{
    public Transform SpawnPoint;
    public GameObject PlayerIAPrefab;
    public int AmountIA = 0;

    private List<GameObject> playerIAs = new List<GameObject>();
    private bool started = false;
    private bool won = false;
    private bool ready = false;

    public void GameStartLocal(int players)
    {
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().Play("ingame_ambient");
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManagerHUD>().StartHost(true);
        ClientScene.AddPlayer(NetworkClient.allClients[0].connection, 1);
        this.started = true;
        this.CmdSpawnIAs();
        this.CmdSpawnEnv();
    }

    public void ToggleIA()
    {
        this.AmountIA = this.AmountIA == 0 ? 1 : 0;
    }

    [Command]
    public void CmdGameStart(bool isServer)
    {
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().Play("ingame_ambient");
        if (isServer)
        {
            this.CmdSpawnIAs();
            this.CmdSpawnEnv();
        }
        this.started = true;
    }

    [Command]
    void CmdSpawnEnv()
    {
        List<GameObject> map = GameObject.FindGameObjectWithTag("EnvironmentManager").GetComponent<GenerationEnvironment>().GenerateEnvironment();
        foreach (GameObject gameobject in map)
        {
            if (gameobject != null)
                NetworkServer.Spawn(gameobject);
        }
    }

    [Command]
    void CmdSpawnIAs()
    {
        for (int i = 0; i < this.AmountIA; i++)
        {
            GameObject IA = Instantiate(PlayerIAPrefab, SpawnPoint.transform.position, Quaternion.identity);
            NetworkServer.Spawn(IA);
            this.playerIAs.Add(IA);
        }
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
        GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().Play("game_ambient");
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

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetPlayersAlive().Count > 1)
            this.ready = true;
        if (this.started && this.ready)
        {
            if (this.GetPlayersAlive().Count == 1 && this.won == false)
            {
                this.Win();
            }
        }
    }
}

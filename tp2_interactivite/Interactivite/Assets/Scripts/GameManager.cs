using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

    public Transform SpawnPoint;
    public GameObject PlayerIAPrefab;
    public int AmountIA = 1;

    private List<GameObject> playerIAs = new List<GameObject>();
    private bool started = false;
    private bool won = false;

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

    void SpawnIAs()
    {
        for (int i = 0; i < this.AmountIA; i++)
        {
            GameObject IA = Instantiate(PlayerIAPrefab, SpawnPoint.transform.position, Quaternion.identity);
            this.playerIAs.Add(IA);
            NetworkServer.Spawn(IA);
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

    bool Ready()
    {
        if (GameObject.FindWithTag("Player") != null && (NetworkServer.connections.Count > 0 && this.AmountIA > 0)
            || (NetworkServer.connections.Count > 1 && this.AmountIA <= 0))
            return true;
        return false;
    }
    void Restart()
    {
        NetworkManager manager = GameObject.FindObjectOfType<NetworkManager>();
        manager.StopServer();
        manager.StopHost();
        this.started = false;
        this.won = false;
    }

    void SoftRestart()
    {
        this.started = false;
        SceneManager.LoadScene("Game");
    }
    
    void Win()
    {
        this.won = true;
        WinMessage msg = new WinMessage();
        msg.win = true;
        msg.toDisplay = "Congratulations, " + this.GetWinner().gameObject.name + " won the game !";
        NetworkServer.SendToAll(MyMsgType.Win, msg);
        TextAnnouncer announcer = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<TextAnnouncer>();
        Invoke("Restart", announcer.displayTime);
        announcer.Display(msg.toDisplay);
    }

    void OnWin(NetworkMessage netMsg)
    {
        Debug.Log("SALUT");
        WinMessage msg = netMsg.ReadMessage<WinMessage>();
        if (msg.win)
        {
            TextAnnouncer announcer = GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<TextAnnouncer>();
            Invoke("Restart", announcer.displayTime);
            announcer.Display(msg.toDisplay);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!this.started && this.Ready())
        {
            this.SpawnIAs();
            this.started = true;
        } else if (this.started && !this.Ready())
        {
            this.SoftRestart();
        }

        if (this.started)
        {
            if (this.Won() && this.won == false)
            {
                this.Win();
            }
        }
    }
}

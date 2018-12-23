using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class PlayerIA : Player
{
    public GameObject player;

    private Vector3 lastPos;
    private float lastTime;

    // Use this for initialization
    void Start()
    {
        this.lastPos = transform.position;
        this.lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;

        if (Time.time - this.lastTime > 0.6f)
        {
            if (Vector3.Distance(this.transform.position, this.lastPos) < 0.1f)
            {
                this.CmdBomb();
            }
            lastPos = currentPos;
            this.lastTime = Time.time;
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
            GetComponent<NavMeshAgent>().SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}

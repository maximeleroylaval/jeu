using UnityEngine;

public class PlayerIA : Player {

    public GameObject player;

    private Vector3 lastPos;

    // Use this for initialization
    void Start() {
        this.lastPos = transform.position;
    }

    Vector3 ChasePlayer()
    {
        if (this.player == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length <= 0)
                return Vector3.zero;
            else
                this.player = players[0];
        }

        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (direction.x < 0)
            return Vector3.left;
        else if (direction.x <= 0)
            return Vector3.right;
        else if (direction.z >= 0)
            return Vector3.forward;
        else if (direction.z < 0)
            return Vector3.back;
        else
            return Vector3.zero;
    }

    Vector3 HideFromBomb()
    {
        bool check = false;
        RaycastHit objectHit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out objectHit, 50))
        {
            Debug.Log(objectHit.collider.name);
            if (objectHit.collider.name.Contains("boxdestroy"))
            {
                Vector3 direction = objectHit.collider.transform.position;
                if (Vector3.Distance(transform.position, direction) > 1)
                {
                    if (direction.x < 0)
                        return Vector3.left;
                    else if (direction.x <= 0)
                        return Vector3.right;
                    else if (direction.z >= 0)
                        return Vector3.forward;
                    else if (direction.z < 0)
                        return Vector3.back;
                }
                check = true;
            }
        }

        if (!check)
        {
            transform.Rotate(0, 90, 0);

            Vector3 direction = ((transform.rotation * Quaternion.Euler(0, 90, 0)) * transform.rotation) * Vector3.forward;
            return direction;
        }
        


        return Vector3.zero;
    
    }

    public override Vector3 GetDirection()
    {
        // BOMB TOO CLOSE
        //if (this.bombs.Count > 0 && this.bombs[0] != null && Vector3.Distance(transform.position, this.bombs[0].transform.position) > 0.35)
            return HideFromBomb();
        //return ChasePlayer();
    }

    // Update is called once per frame
    void Update () {
        Vector3 currentPos = transform.position;

        if (Vector3.Distance(currentPos, lastPos) < 1)
            CmdBomb();

        Move();

        lastPos = currentPos;
    }
}

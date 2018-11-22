using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIA : MonoBehaviour {
    public float speed = 2.0f;
    public int bombLimit = 1;
    public GameObject bombPrefab;
    public GameObject player;
    public List<GameObject> bombs;
    
    Vector3 lastPos;

    bool CanBomb()
    {
        if (this.bombs.Count < this.bombLimit)
            return true;
        return false;
    }

    Vector3 ChasePlayer()
    {
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
        // TODO
        /*RaycastHit objectHit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.Log("HERE");
        Debug.DrawRay(transform.position, fwd * 50, Color.green, 20, true);
        if (Physics.Raycast(transform.position, fwd, out objectHit, 50))
        {
            Debug.Log(objectHit.collider.name);
            if (objectHit.collider.name == "Enemy")
            {
                Debug.Log("Close to enemy");
            }
        }*/

        return Vector3.left;
    }


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction;
        Vector3 currentPos = transform.position;




        if (Vector3.Distance(currentPos, lastPos) < 1)
        {
            if (this.CanBomb())
            {
                GameObject bomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x - 1.14f, 0.087f, this.transform.position.z), Quaternion.identity);
                bomb.transform.parent = transform.parent;
                bomb.GetComponent<BoxCollider>().isTrigger = true;
                this.bombs.Add(bomb);
            }
        }
        
        if (this.bombs.Count > 0 && this.bombs[0] != null && Vector3.Distance(transform.position, this.bombs[0].transform.position) > 0.7) // BOMB TOO CLOSE
        {
            direction = HideFromBomb();
        }
        else
        {
            direction = ChasePlayer();
        }

        transform.rotation = Quaternion.LookRotation(direction);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

















        for (int i = this.bombs.Count - 1; i >= 0; i--)
        {
            if (this.bombs[i] == null)
                this.bombs.RemoveAt(i);
        }
        lastPos = currentPos;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Explosive")
            this.Die();
    }
    public void Die()
    {
        string toDisplay = this.gameObject.name + " just died like a noob";
        GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<TextAnnouncer>().Display(toDisplay);
        Destroy(this.gameObject);
    }

}

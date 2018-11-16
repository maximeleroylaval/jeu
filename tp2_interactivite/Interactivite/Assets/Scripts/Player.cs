using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 2.0f;
    public int bombLimit = 1;
    public GameObject bombPrefab;
    public List<GameObject> bombs;

	// Use this for initialization
	void Start () {

	}

    Vector3 GetDirection()
    {
        if (Input.GetKey("right"))
            return Vector3.right;
        else if (Input.GetKey("left"))
            return Vector3.left;
        else if (Input.GetKey("up"))
            return Vector3.forward;
        else if (Input.GetKey("down"))
            return -Vector3.forward;
        return Vector3.zero;
    }

    bool CanBomb()
    {
        if (Input.GetKey("space") && this.bombs.Count < this.bombLimit)
            return true;
        return false;
    }

    // Update is called once per frame
    void Update () {
        Vector3 direction = this.GetDirection();
        if (direction != Vector3.zero)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
        if (this.CanBomb())
        {
            GameObject bomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x - 1.14f, 0.087f, this.transform.position.z), Quaternion.identity);
            bomb.transform.parent = transform.parent;
            bomb.GetComponent<BoxCollider>().isTrigger = true;
            this.bombs.Add(bomb);
        }

        for (int i = this.bombs.Count - 1; i >= 0; i--)
        {
            if (this.bombs[i] == null)
                this.bombs.RemoveAt(i);
        }
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
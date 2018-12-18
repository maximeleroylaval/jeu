using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bomb : NetworkBehaviour {

    public int range = 1;
    public float waitExplode = 2.5f;
    public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
        Invoke("CmdExplode", this.waitExplode);
    }
	
	// Update is called once per frame
	void Update () {

    }

    [Command]
    void CmdExplode()
    {
        for (int i = 0; i < this.range; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab, new Vector3(this.transform.position.x + 1.2f, this.transform.position.y + 0.3f, this.transform.position.z), Quaternion.identity);
            explosion.transform.parent = transform.parent;
        }
        Destroy(this.gameObject);
    }

    void Activate()
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.Activate();
        }
    }
}
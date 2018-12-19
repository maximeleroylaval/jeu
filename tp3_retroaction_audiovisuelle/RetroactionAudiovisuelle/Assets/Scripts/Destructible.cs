using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Destructible : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    void CmdExplode()
    {
        Destroy(this.gameObject);
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Explosive")
            this.CmdExplode();
    }
}

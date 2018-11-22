using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Explosion : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        var psys = this.GetComponent<ParticleSystem>();
        Invoke("CmdDestroy", psys.main.duration);
    }

    // Update is called once per frame
    void Update () {

    }

    [Command]
    void CmdDestroy()
    {
        Destroy(this.gameObject);
    }
}

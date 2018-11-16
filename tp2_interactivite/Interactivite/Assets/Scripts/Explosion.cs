using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var psys = this.GetComponent<ParticleSystem>();
        Destroy(this.gameObject, psys.main.duration);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderCustom : ColliderCustom
{
    public Vector3 center;
    public Vector3 size = new Vector3(1, 1, 1);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void GenerateExtrusion()
    {

    }

    public override void CheckCollision(ColliderCustom c)
    {

    }
}

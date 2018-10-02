using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderCustom : MonoBehaviour {

    private Vector3 velocity;
    private Vector3 acceleration;
    private Bounds bounds;

    public bool rigid;
    public Vector3 center;
    public Vector3 size;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (rigid == true)
        {
            Vector3 forces;
            forces = GameObject.FindObjectOfType<Environment>().GetGravitation(this.transform.position);

            this.SetAcceleration(forces);
            this.SetVelocity();
            this.SetPosition(this.transform.position);
        }
	}

    void SetPosition(Vector3 position)
    {
        this.transform.position = new Vector3(position.x, position.y + (this.velocity.y * Time.deltaTime) + (0.5f * this.acceleration.y * Mathf.Pow(Time.deltaTime, 2)), position.z);
    }

    void SetAcceleration(Vector3 forces)
    {
        this.acceleration = new Vector3(this.acceleration.x, forces.y, this.acceleration.z);
    }

    void SetVelocity()
    {
        this.velocity = new Vector3(this.velocity.x, this.velocity.y + (this.acceleration.y * Time.deltaTime), this.velocity.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCustom : MonoBehaviour {

    private Vector3 velocity;
    private Vector3 acceleration;
    private Bounds bounds;
    private Vector3 lastPosition;

    public float Mass = 1;
    public bool UseGravity = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (this.UseGravity)
        {
            Vector3 forces;
            forces = GameObject.FindObjectOfType<Environment>().GetGravitation(this.transform.position);

            this.SetAcceleration(forces);
            this.SetVelocity();
            this.SetPosition(this.transform.position);
        }
    }

    public void SetPosition(Vector3 position)
    {
        this.lastPosition = this.transform.position;
        this.transform.position = position + (this.velocity * Time.deltaTime) + (0.5f * this.acceleration * Mathf.Pow(Time.deltaTime, 2));
    }

    public void SetAcceleration(Vector3 forces)
    {
        this.acceleration = forces;
    }

    void SetVelocity()
    {
        this.velocity = velocity + (this.acceleration * Time.deltaTime);
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void StopMovement()
    {
        this.transform.position = this.lastPosition;
        UseGravity = false;
    }
    public void BounceMovement(float BounceCoef, Vector3 normale)
    {
        this.transform.position = this.lastPosition;

        Vector3 result = BounceCoef * (this.velocity - (2f * Vector3.Dot(normale, this.velocity) * normale));

        this.velocity = result;
    }
}

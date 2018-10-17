using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColliderCustom : ColliderCustom {

    public Vector3 Center;
    public float Radius = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void GenerateExtrusion()
    {

    }

    public bool CheckPoint(Vector3 point)
    {
        Vector3 resDist = (this.Center + this.transform.position) - point;
        float dist = Mathf.Sqrt(Mathf.Pow(resDist.x, 2) + Mathf.Pow(resDist.y, 2) + Mathf.Pow(resDist.z, 2));

        if (dist <= this.Radius)
        {
            return true;
        }

        return false;
    }

    public bool CheckPoints(List<Vector3> points)
    {
        foreach (Vector3 point in points)
        {
            if (this.CheckPoint(point) == true)
            {
                return true;
            }
        }
        return false;
    }

    public override void CheckCollision(ColliderCustom c)
    {
        if (c is BoxColliderCustom && this.CheckPoints(((BoxColliderCustom)c).GetAllPoints()))
        {
            this.GetComponent<RigidbodyCustom>().stopMovement();
        }
    }
}

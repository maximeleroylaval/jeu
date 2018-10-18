using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColliderCustom : ColliderCustom {

    public Vector3 Center;
    public float Radius = 0.5f;

    public bool Bounce = false;
    public float BounceCoefficient = 0.7f;

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

    public List<Vector3> GetCollisionPoints(List<Vector3> points)
    {
        List<Vector3> collision = new List<Vector3>();

        foreach (Vector3 point in points)
        {
            if (this.CheckPoint(point) == true)
            {
                collision.Add(point);
            }
        }
        return collision;
    }

    public override void CheckCollision(ColliderCustom c)
    {
        if (c is BoxColliderCustom)
        {
            List<Vector3> collisions = this.GetCollisionPoints(((BoxColliderCustom)c).GetAllPoints());
    
            if (collisions.Count > 0)
            {
                if (this.Bounce)
                {
                    Vector3[] face = ((BoxColliderCustom)c).GetFace(collisions);

                    Vector3 normale = ((BoxColliderCustom)c).GetNormal(face);

                    if (normale == Vector3.zero)
                    {
                        normale = new Vector3(0f, 1f, 0f);
                    }

                    this.GetComponent<RigidbodyCustom>().BounceMovement(this.BounceCoefficient, normale);
                }
            }
        }
    }
}

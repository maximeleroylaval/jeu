using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderCustom : ColliderCustom
{
    public Vector3 Center;
    public Vector3 Size = new Vector3(1, 1, 1);

    public bool stop = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public override void GenerateExtrusion()
    {

    }

    public Vector3[][] GetFaces()
    {
        Vector3[] verticiesUp = new Vector3[4];

        verticiesUp[0] = new Vector3(-0.5f * Size.x, 0.5f * Size.y, 0.5f * Size.z) + this.transform.position; //UP LEFT
        verticiesUp[1] = new Vector3(0.5f * Size.x, 0.5f * Size.y, 0.5f * Size.z) + this.transform.position; //UP RIGHT
        verticiesUp[2] = new Vector3(0.5f * Size.x, 0.5f * Size.y, -0.5f * Size.z) + this.transform.position; //DOWN RIGHT
        verticiesUp[3] = new Vector3(-0.5f * Size.x, 0.5f * Size.y, -0.5f * Size.z) + this.transform.position; //DOWN LEFT

        Vector3[] verticiesUnder = new Vector3[4];

        verticiesUnder[0] = new Vector3(-0.5f * Size.x, -0.5f * Size.y, -0.5f * Size.z) + this.transform.position;
        verticiesUnder[1] = new Vector3(0.5f * Size.x, -0.5f * Size.y, -0.5f * Size.z) + this.transform.position;
        verticiesUnder[2] = new Vector3(0.5f * Size.x, -0.5f * Size.y, 0.5f * Size.z) + this.transform.position;
        verticiesUnder[3] = new Vector3(-0.5f * Size.x, -0.5f * Size.y, 0.5f * Size.z) + this.transform.position;

        Vector3[] verticiesLeft = new Vector3[4];

        verticiesLeft[0] = verticiesUp[0];
        verticiesLeft[1] = verticiesUp[3];
        verticiesLeft[2] = verticiesUnder[0];
        verticiesLeft[3] = verticiesUnder[3];

        Vector3[] verticiesRight = new Vector3[4];

        verticiesRight[0] = verticiesUp[2];
        verticiesRight[1] = verticiesUp[1];
        verticiesRight[2] = verticiesUnder[2];
        verticiesRight[3] = verticiesUnder[1];

        Vector3[] verticiesFront = new Vector3[4];

        verticiesFront[0] = verticiesUp[1];
        verticiesFront[1] = verticiesUp[0];
        verticiesFront[2] = verticiesUnder[3];
        verticiesFront[3] = verticiesUnder[2];

        Vector3[] verticiesBehind = new Vector3[4];

        verticiesBehind[0] = verticiesUp[3];
        verticiesBehind[1] = verticiesUp[2];
        verticiesBehind[2] = verticiesUnder[1];
        verticiesBehind[3] = verticiesUnder[0];

        Vector3[][] faces = new Vector3[6][];

        faces[0] = verticiesUp;
        faces[1] = verticiesUnder;
        faces[2] = verticiesLeft;
        faces[3] = verticiesRight;
        faces[4] = verticiesFront;
        faces[5] = verticiesBehind;

        return faces;
    }

    public Vector3 CrossProduct(Vector3 ab, Vector3 ac)
    {
        return new Vector3(ab.y * ac.z - ac.y * ab.z, ab.z * ac.x - ab.x * ac.z, ab.x * ac.y - ac.x * ab.y);
    }

    public bool CheckPointContact(Vector3 res)
    {
        if (this.stop == true)
        {
            return false;
        }
        Vector3 boundsPos = this.Center + this.transform.position + 0.5f * this.Size;
        Vector3 boundsNeg = this.Center + this.transform.position + -0.5f * this.Size;

        Debug.Log("Position: " + this.transform.position);
        Debug.Log("Pos:" + boundsPos);
        Debug.Log("Neg:" + boundsNeg);

        this.stop = true;

        return res.x <= boundsPos.x && res.x >= boundsNeg.x && res.y <= boundsPos.y && res.y >= boundsNeg.y && res.z <= boundsPos.z && res.z >= boundsNeg.z;
    }

    public bool CheckFaces(Vector3[] tops)
    {
        bool collide = false;

        Vector3[][] faces = this.GetFaces();

        foreach(Vector3[] face in faces)
        {
            Vector3 ab = face[1] - face[0];
            Vector3 ac = face[2] - face[1];
            Vector3 normalUp = this.CrossProduct(ab, ac);
            float normalDown = Mathf.Sqrt(Mathf.Pow(normalUp.x, 2) + Mathf.Pow(normalUp.y, 2) + Mathf.Pow(normalUp.z, 2));
            Vector3 normal = normalUp / normalDown;

            Vector3 segmentV = tops[1] - tops[0];
            Vector3 point = tops[0];

            for (float increment = 0f; increment < 1.0f; increment += 0.01f)
            {
                point = tops[0] + increment * segmentV;
                Vector3 res = this.CrossProduct(point, normal);

                if (this.CheckPointContact(res) == true)
                {
                    Debug.Log("Contact ?");
                }
            }
        }

        return collide;
    }

    public override void CheckCollision(ColliderCustom c)
    {
        Vector3[][] faces = this.GetFaces();

        foreach(Vector3[] face in faces)
        {
            if (c is BoxColliderCustom && ((BoxColliderCustom)c).CheckFaces(face))
            {
                Debug.Log("Collide");
            }
        }
    }
}

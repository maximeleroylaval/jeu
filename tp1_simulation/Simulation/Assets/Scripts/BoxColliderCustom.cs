using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderCustom : ColliderCustom
{
    public Vector3 Center;
    public Vector3 Size = new Vector3(1, 1, 1);
    public float Precision = 0.1f;

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

    public void TimePrint(Vector3 value)
    {
        if (this.stop == true)
        {
            return;
        }
        Debug.Log("Vector3 = " + value);
        this.stop = true;
    }

    public bool CheckPlane(Vector3[] tops, Vector3 segmentVV, Vector3 segmentVH, Vector3 inPlane, Vector3 normal)
    {
        Vector3 pointH, pointV;
        Vector3 zero = new Vector3(0, 0, 0);

        pointV = tops[0];
        for (float alphaV = 0f; alphaV < 1.0f; alphaV += this.Precision)
        {
            pointV = tops[0] + alphaV * segmentVV;

            for (float alphaH = 0f; alphaH < 1.0f; alphaH += this.Precision)
            {
                pointH = pointV + alphaH * segmentVH;

                Vector3 toCheck = this.CrossProduct(pointH, normal);
                Vector3 res = inPlane - toCheck;

                if (res == zero)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Vector3 GetNormal(Vector3[] face)
    {
        Vector3 ab = face[1] - face[0];
        Vector3 ac = face[2] - face[1];

        Vector3 normalUp = this.CrossProduct(ab, ac);

        float normalDown = Mathf.Sqrt(Mathf.Pow(normalUp.x, 2) + Mathf.Pow(normalUp.y, 2) + Mathf.Pow(normalUp.z, 2));

        Vector3 normal = normalUp / normalDown;

        return normal;
    }

    public bool CheckFaces(Vector3[] tops)
    {
        Vector3 segmentVH = tops[1] - tops[0];
        Vector3 segmentVV = tops[3] - tops[0];

        Vector3[][] faces = this.GetFaces();

        Vector3 normal = this.GetNormal(faces[0]);

        Vector3 segmentVD = faces[1][0] - faces[0][0];
        Vector3 pointD = faces[0][0];

        for (float alphaD = 0f; alphaD < 1.0f; alphaD += this.Precision)
        {
            pointD = faces[0][0] + alphaD * segmentVD;
            Vector3 inPlane = this.CrossProduct(pointD, normal);

            if (this.CheckPlane(tops, segmentVV, segmentVH, inPlane, normal) == true)
            {
                return true;
            }
        }

        return false;
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

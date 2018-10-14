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

        verticiesUp[0] = Vector3.Scale(new Vector3(-0.5f * Size.x, 0.5f * Size.y, 0.5f * Size.z), this.transform.localScale) + this.transform.position; //UP LEFT
        verticiesUp[1] = Vector3.Scale(new Vector3(0.5f * Size.x, 0.5f * Size.y, 0.5f * Size.z), this.transform.localScale) + this.transform.position; //UP RIGHT
        verticiesUp[2] = Vector3.Scale(new Vector3(0.5f * Size.x, 0.5f * Size.y, -0.5f * Size.z), this.transform.localScale) + this.transform.position; //DOWN RIGHT
        verticiesUp[3] = Vector3.Scale(new Vector3(-0.5f * Size.x, 0.5f * Size.y, -0.5f * Size.z), this.transform.localScale) + this.transform.position; //DOWN LEFT

        Vector3[] verticiesUnder = new Vector3[4];

        verticiesUnder[0] = Vector3.Scale(new Vector3(-0.5f * Size.x, -0.5f * Size.y, -0.5f * Size.z), this.transform.localScale) + this.transform.position;
        verticiesUnder[1] = Vector3.Scale(new Vector3(0.5f * Size.x, -0.5f * Size.y, -0.5f * Size.z), this.transform.localScale) + this.transform.position;
        verticiesUnder[2] = Vector3.Scale(new Vector3(0.5f * Size.x, -0.5f * Size.y, 0.5f * Size.z), this.transform.localScale) + this.transform.position;
        verticiesUnder[3] = Vector3.Scale(new Vector3(-0.5f * Size.x, -0.5f * Size.y, 0.5f * Size.z), this.transform.localScale) + this.transform.position;

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

    public List<Vector3> GetFacePoints(Vector3[] tops)
    {
        Vector3 segmentVH = tops[1] - tops[0];
        Vector3 segmentVV = tops[3] - tops[0];

        List<Vector3> points = new List<Vector3>();

        Vector3 pointH, pointV;

        for (float alphaV = 0f; alphaV < 1.0f + this.Precision; alphaV += this.Precision)
        {
            pointV = tops[0] + alphaV * segmentVV;

            for (float alphaH = 0f; alphaH < 1.0f + this.Precision; alphaH += this.Precision)
            {
                pointH = pointV + alphaH * segmentVH;

                points.Add(pointH);
            }
        }

        return points;
    }

    public List<Vector3> GetAllPoints()
    {
        Vector3[][] faces = this.GetFaces();

        List<Vector3> points = new List<Vector3>();

        foreach(Vector3[] face in faces)
        {
            List<Vector3> facePoints = this.GetFacePoints(face);

            foreach(Vector3 mypoint in facePoints)
            {
                points.Add(mypoint);
            }
        }

        return points;
    }

    public bool CheckPoint(Vector3 pointH)
    {
        Vector3[][] faces = this.GetFaces();

        if (pointH.x >= faces[1][0].x && pointH.y >= faces[1][0].y && pointH.z >= faces[1][0].z
            && pointH.x <= faces[0][1].x && pointH.y <= faces[0][1].y && pointH.z <= faces[0][1].z)
        {
            return true;
        }
        return false;
    }

    public bool CheckFace(Vector3[] tops)
    {
        List<Vector3> points = this.GetFacePoints(tops);

        foreach (Vector3 point in points)
        {
            if (this.CheckPoint(point))
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
            if (c is BoxColliderCustom && ((BoxColliderCustom)c).CheckFace(face))
            {
                Debug.Log("Collide");
                break;
            }
        }
    }
}

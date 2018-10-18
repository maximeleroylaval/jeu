using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public GameObject SpherePrefab;
    public GameObject CubePrefab;

    private GameObject cube = null;
    private GameObject sphere = null;

    private bool wait = false;

    public void SpawnEach()
    {
        if (sphere == null)
        {
            if (cube != null) Object.Destroy(cube);
            cube = null;
            sphere = Object.Instantiate(SpherePrefab, new Vector3(0, 5, -8), new Quaternion(0, 0, 0, 0));
            sphere.GetComponent<RigidbodyCustom>().SetVelocity(new Vector3(0f, -8f, 6f));
        }
        else if (cube == null)
        {
            if (sphere != null) Object.Destroy(sphere);
            sphere = null;
            cube = Object.Instantiate(CubePrefab, new Vector3(0, 4, 5), new Quaternion(0, 0, 0, 0));
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Round(Time.time) % 5 == 0)
        {
            if (this.wait == false)
            {
                this.SpawnEach();
                this.wait = true;
            }
        } else
        {
            this.wait = false;
        }
    }
}

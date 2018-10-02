using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

    public float gravity;
    
    public Vector3 GetGravitation(Vector3 position)
    {
        Vector3 gravitation;

        gravitation.x = 0;
        gravitation.y = this.gravity;
        gravitation.z = 0;

        return gravitation;
    }
}

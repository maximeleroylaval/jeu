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

    void Start()
    {

    }

    void Update()
    {
        ColliderCustom[] colliders;

        colliders = Object.FindObjectsOfType<ColliderCustom>();

        foreach (ColliderCustom c in colliders)
        {
            c.GenerateExtrusion();
        }

        foreach (ColliderCustom c in colliders)
        {
            foreach (ColliderCustom cc in colliders)
            {
                if (c != cc)
                {
                    c.CheckCollision(cc);
                }
            }
        }
    }
}

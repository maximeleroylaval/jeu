using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColliderCustom : MonoBehaviour
{
    public abstract void GenerateExtrusion();
    public abstract void CheckCollision(ColliderCustom c);
}

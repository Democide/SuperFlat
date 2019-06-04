using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public float fireRate = 1f;

    public virtual void Fire(Vector3 projectileOrigin, Vector3 targetDirection) {
        Debug.Log("Pew (" + name + ")");
    }

}

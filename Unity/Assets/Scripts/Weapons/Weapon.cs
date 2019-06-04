using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{

    public virtual void Fire(Vector3 projectileOrigin, Vector2 direction) {
        Debug.Log("Pew (" + name + ")");
    }

}

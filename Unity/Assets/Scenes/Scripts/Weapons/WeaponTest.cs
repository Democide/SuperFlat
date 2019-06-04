using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Test", fileName = "New Weapon Test")]
public class WeaponTest : Weapon
{
    public override void Fire(Vector3 projectileOrigin, Vector2 direction) {
        base.Fire(projectileOrigin, direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Test", fileName = "New Weapon Test")]
public class WeaponTest : Weapon
{
    public GameObject projectile;

    public override void Fire(Vector3 projectileOrigin, Vector3 targetDirection) {
        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        var proj = Instantiate(projectile, projectileOrigin + targetDirection.normalized * 0.2f, Quaternion.Euler(0, 0, angle));
    }
}

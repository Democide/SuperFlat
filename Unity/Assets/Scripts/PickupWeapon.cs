using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : Pickup {

    [SerializeField] Weapon weapon;

    protected override void Collect(Character character) {
        character.EquipNewWeapon(weapon);
    }

}



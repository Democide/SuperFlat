using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public override void Kill() {
        base.Kill();

        if (!alive) {
            Destroy(gameObject);
        }
    }

}

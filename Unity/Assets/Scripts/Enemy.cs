using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    Vector2 target;
    Rigidbody2D rigid;
    public float VelocityMax = 5f;

    private void Awake () {
        rigid = GetComponent<Rigidbody2D>();
    }

    public override void Kill() {
        base.Kill();

        if (!alive) {
            Destroy(gameObject);
        }
    }

    public void SetTarget (Vector2 t) {
        target = t;
    }

    public void AddForceVelocityCapped (Vector2 force) {
        if (rigid.velocity.magnitude < VelocityMax) rigid.AddForce(force);
    }

    private void FixedUpdate () {
        AddForceVelocityCapped(target * speedMoveGround);
    }
}

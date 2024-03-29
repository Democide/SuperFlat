﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    Vector2 target;
    Rigidbody2D rigid;
    public float VelocityMax = 5f;
    public GameController gc;
    public bool move = false;
    public GameObject PrefPFXEnemyDeath;

    private void Awake () {
        rigid = GetComponent<Rigidbody2D>();
        gc = GameObject.Find("GameController").GetComponent<GameController>(); // sorry 

    }

    public override void Kill() {
        base.Kill();

        gc.Score++;

        if (!alive) {
            GameObject.Instantiate(PrefPFXEnemyDeath, transform.position, Quaternion.Euler(90f, 0f, 0f));
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
        if (move) AddForceVelocityCapped(target * speedMoveGround);
    }
}

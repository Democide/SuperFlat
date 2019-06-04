﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] protected float speedMoveGround = 100f;
    [SerializeField] protected float speedMoveAir = 100f;
    [SerializeField] protected float mass = 100f;
    [SerializeField] protected bool hasGravity = true;
    [SerializeField] protected bool canJump = true;
    [SerializeField] protected bool isGrounded = false;
    [SerializeField] protected float forceJump = 100f;
    [SerializeField] protected bool alive = true;
    [SerializeField] protected Weapon weapon;

    const float groundYTolerance = 0.1f;
    Vector3 groundCheckRight;
    Vector3 groundCheckLeft;
    float groundCheckHeight;

    Rigidbody2D rigid;
    Collider2D col;
    Vector2 moveDirection;

    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = hasGravity ? 1f : 0f;
        rigid.mass = mass;
        col = GetComponent<Collider2D>();

        groundCheckLeft = new Vector3(-col.bounds.extents.x, -col.bounds.extents.y);
        groundCheckRight = new Vector3(col.bounds.extents.x, -col.bounds.extents.y);
        groundCheckHeight = groundYTolerance;
    }

    public bool IsAlive() {
        return alive;
    }

    public Weapon GetWeapon() {
        return weapon;
    }

    public void Move(Vector2 direction) {
        moveDirection = direction;
    }

    public void AddForce(Vector2 force) {
        rigid.AddForce(force);
    }

    private void FixedUpdate() {
        if (isGrounded)
            rigid.AddForce(moveDirection * speedMoveGround);
        else
            rigid.AddForce(moveDirection * speedMoveAir);

        if (canJump) {
            isGrounded = Physics2D.Raycast(transform.position + groundCheckLeft, Vector3.down, groundCheckHeight)
                || Physics2D.Raycast(transform.position + groundCheckRight, Vector3.down, groundCheckHeight);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawLine(transform.position + groundCheckLeft, transform.position + groundCheckLeft + Vector3.down * groundCheckHeight);
        Gizmos.DrawLine(transform.position + groundCheckRight, transform.position + groundCheckRight + Vector3.down * groundCheckHeight);
    }

    public void Fire(Vector2 direction) {
        if (weapon != null)
            weapon.Fire(transform.position, direction);
    }

}
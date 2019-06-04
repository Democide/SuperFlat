using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBasic : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.right * speed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var character = collision.collider.GetComponent<Character>();

        if (character != null) { 
            character.Kill();
        }

        Destroy(gameObject);
    }

}

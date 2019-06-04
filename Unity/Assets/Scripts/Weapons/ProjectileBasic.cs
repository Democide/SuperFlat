using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBasic : MonoBehaviour
{
    private float speed;
    [SerializeField] float lifeTime = 5f;

    private Rigidbody2D rigid;

    public void SetSpeed(float s) {
        speed = s;
        rigid.AddForce(transform.right * speed, ForceMode2D.Force);
    }

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Expire", lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var character = collision.collider.GetComponent<Character>();

        if (character != null) { 
            character.Kill();
        }

        Expire();
    }

    private void Expire() {
        Destroy(gameObject);
    }

}

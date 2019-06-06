using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBasic : MonoBehaviour
{
    private float speed;
    [SerializeField] float lifeTime = 5f;
    float startTime;

    private Rigidbody2D rigid;

    int bouncesCurrent;
    public int bouncesMax;
    public float velocityMin = 2f;

    public void SetSpeed(float s) {
        speed = s;
        rigid.AddForce(transform.right * speed, ForceMode2D.Force);
    }

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Expire", lifeTime);
        startTime = Time.time;
    }

    float getAge() {
        return Time.time - startTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var character = collision.collider.GetComponent<Character>();
        if (character != null) { 
            character.Kill();
            Expire();
        }

        var player = collision.collider.GetComponent<Player>();
        if (player != null) {
            player.Kill();
            Expire();
        }

        bouncesCurrent++;
    }

    private void Update () {
        if (bouncesCurrent >= bouncesMax) Expire();
        if (rigid.velocity.magnitude < velocityMin && getAge() > 2f) Expire(); // WHY IS THIS NOT WORKING?!?!?
    }

    private void Expire() {
        Destroy(gameObject);
    }

}

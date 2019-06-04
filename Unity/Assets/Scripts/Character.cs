using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] public float speedMoveGround = 6f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] public bool canStomp = true;
    [SerializeField] public float timeoutStomp = 1f;
    [SerializeField] public bool canDash = true;
    [SerializeField] public float timeoutDash = 2f;
    [SerializeField] public float distanceDash = 7f;

    [SerializeField] protected bool alive = true;
    [SerializeField] protected Weapon weapon;

    float lastShotTime = -1f;

    private void Start() {

    }

    public bool IsAlive() {
        return alive;
    }

    public Weapon GetWeapon() {
        return weapon;
    }

    public void EquipNewWeapon(Weapon newWeapon) {
        weapon = newWeapon;
        lastShotTime = Time.time;
    }

    public virtual void Kill() {
        if (alive) {
            Debug.Log("Killed " + name);
            alive = false;
        }
    }

    public void ResetStomp () {
        StartCoroutine("CoResetStomp");
    }

    public void ResetDash () {
        StartCoroutine("CoResetDash");
    }

    private IEnumerator CoResetStomp () {
        yield return new WaitForSeconds(timeoutStomp);
        canStomp = true;
    }

    private IEnumerator CoResetDash() {
        yield return new WaitForSeconds(timeoutDash);
        canDash = true;
    }

    public bool CanFire() {
        return weapon != null && (lastShotTime < 0f || Time.time >= lastShotTime + weapon.fireRate);
    }

    public void Fire(Vector3 direction) {
        if (CanFire()) {
            Vector3 v = new Vector3();
            v = direction.normalized;
            weapon.Fire(transform.position +v, direction);
            lastShotTime = Time.time;
        }
    }
}

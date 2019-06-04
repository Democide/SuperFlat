using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    Player player;
    Vector2 directionMove;

    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        bool rightHeld = Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0;
        bool leftHeld = Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0;
        bool upHeld = Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0;
        bool downHeld = Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") < 0;

        if (rightHeld) directionMove = Vector2.right;
        if (leftHeld) directionMove = Vector2.left;
        //if (upHeld) directionMove = Vector2.up;
        //if (downHeld) directionMove = Vector2.down;

        //if (upHeld && rightHeld) directionMove = Vector2.ClampMagnitude(Vector2.up + Vector2.right, 1f);
        //if (upHeld && leftHeld) directionMove = Vector2.ClampMagnitude(Vector2.up + Vector2.left, 1f);
        //if (downHeld && leftHeld) directionMove = Vector2.ClampMagnitude(Vector2.down + Vector2.left, 1f);
        //if (downHeld && rightHeld) directionMove = Vector2.ClampMagnitude(Vector2.down + Vector2.right, 1f);

        player.Move(directionMove);

        if (Input.GetKeyDown(KeyCode.Space)) {
            player.Jump(Vector3.up);
        }

        if (ShouldFire()) {
            player.Fire(player.reticule.transform.position - player.transform.position);
        }
    }

    bool ShouldFire() {
        var weapon = player.GetWeapon();

        if (weapon == null)
            return false;

        switch (weapon.fireMode) {
            case Weapon.FireMode.Semi:
                return Input.GetMouseButtonDown(0);
            case Weapon.FireMode.Auto:
                return Input.GetMouseButton(0);
            case Weapon.FireMode.Charge:
                return Input.GetMouseButtonUp(0);
            default:
                return false;
        }
    }
}

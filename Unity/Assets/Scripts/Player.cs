using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : Character
{
    public void Jump (Vector2 direction) {
        if (isGrounded) AddForce(direction * forceJump);
    }
}

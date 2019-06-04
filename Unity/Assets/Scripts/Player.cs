using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Reticule reticule;

    public void StartGame() {
        alive = true;
    }

    public void Jump (Vector2 direction) {
        if (isGrounded) AddForce(direction * forceJump);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Reticule reticule;
    PlayerInput input;

    public void Init() {
        input = GetComponent<PlayerInput>();
    }

    public void StartGame() {
        alive = true;
    }

    public bool HasActed() {
        return input.hasActed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public Reticule reticule;
    PlayerInput input;
    Text textTimeouts;

    public void Init() {
        input = GetComponent<PlayerInput>();
        textTimeouts = GetComponentInChildren<Text>();
    }

    public void StartGame() {
        alive = true;
    }

    public bool HasActed() {
        return input.hasActed;
    }

    private void Update () {
        if (CanFire() && canDash) textTimeouts.text = "S D";
        if (!CanFire() && canDash) textTimeouts.text = "- D";
        if (CanFire() && !canDash) textTimeouts.text = "S -";
        if (!CanFire() && !canDash) textTimeouts.text = "- -";
    }
}

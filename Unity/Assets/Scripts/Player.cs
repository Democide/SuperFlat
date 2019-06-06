using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public Reticule reticule;
    PlayerInput input;
    Text textTimeouts;
    Vector3 positionStart;
    public GameObject cam;
    TimeController tc;

    public void Init() {
        input = GetComponent<PlayerInput>();
        textTimeouts = GetComponentInChildren<Text>();
        positionStart = transform.position;
        tc = GameObject.Find("GameController").GetComponent<TimeController>(); // sorry Martin =D
    }

    public void StartGame() {
        alive = true;
        transform.position = positionStart;
        cam.transform.position = positionStart;
    }

    public bool HasActed() {
        return input.hasActed;
    }

    private void Update () {
        if (CanFire() && canDash) textTimeouts.text = "S D";
        if (!CanFire() && canDash) textTimeouts.text = "- D";
        if (CanFire() && !canDash) textTimeouts.text = "S -";
        if (!CanFire() && !canDash) textTimeouts.text = "- -";

        textTimeouts.transform.localScale = transform.localScale / 2f; // since player is scaled to 2
    }

     public override void Fire (Vector3 direction) {
        base.Fire(direction);
        tc.TimeBoost(weapon.timeBoostDuration);
    }
}

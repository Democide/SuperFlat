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

    public void Init() {
        input = GetComponent<PlayerInput>();
        textTimeouts = GetComponentInChildren<Text>();
        positionStart = transform.position;
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
    }
}

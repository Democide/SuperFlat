using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            player.Jump(Vector3.up);
        }

        if (Input.GetMouseButtonDown(0)) {
            var weapon = player.GetWeapon();
            weapon.Fire(player.transform.position, player.reticule.transform.position - player.transform.position);
        }
    }
}

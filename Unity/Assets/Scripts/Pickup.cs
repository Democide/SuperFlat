using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        var player = other.GetComponent<Player>();

        if (player != null) {
            Collect(player);
            Destroy(gameObject);
        }
    }

    abstract protected void Collect(Character character);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chargeDistance;

    Enemy enemy;
    Player player;

    private void Awake() {
        enemy = GetComponent<Enemy>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > chargeDistance)
            return;

        // Try to shoot
        enemy.Fire(player.transform.position - transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chargeDistance = 5;
    [SerializeField] float shootDistance = 10;

    Enemy enemy;
    Player player;

    Vector2 playerVector;
    float distance;

    private void Awake() {
        enemy = GetComponent<Enemy>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        playerVector = player.transform.position - transform.position;
        distance = Vector3.SqrMagnitude(playerVector);

        // Try to shoot
        if (distance < shootDistance)
            enemy.Fire(playerVector);

        // Try to move
        if (distance < chargeDistance)
            enemy.Move(playerVector);
    }
}

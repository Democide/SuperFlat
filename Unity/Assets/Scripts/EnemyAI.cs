using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] public float rangeSight = 50;
    [SerializeField] public float distanceDesired = 15;
    [SerializeField] public float rangeShoot = 20;
    [SerializeField] public float ReactionTime = 0.5f;

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
        if (distance < rangeShoot)
            StartCoroutine(CoFire());

        // Try to move
        if (distance < rangeSight && distance > distanceDesired)
            enemy.SetTarget(playerVector);
    }

    IEnumerator CoFire() {
        yield return new WaitForSeconds(ReactionTime);
        enemy.Fire(playerVector);
    }
}

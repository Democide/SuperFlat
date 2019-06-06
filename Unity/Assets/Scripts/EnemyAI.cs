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
        distance = Vector3.Magnitude(playerVector);

        bool hasLineOfSight = true;

        float LoSDistanceReduction = 2f;

        LayerMask mask = LayerMask.GetMask("BlockingPlatforms");
        if (Physics2D.Raycast(transform.position, playerVector, distance - LoSDistanceReduction, mask)) hasLineOfSight = false;

        Debug.DrawRay(transform.position, playerVector.normalized * (distance - LoSDistanceReduction), Color.cyan);

        // Try to shoot
        if (distance < rangeShoot && hasLineOfSight)
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

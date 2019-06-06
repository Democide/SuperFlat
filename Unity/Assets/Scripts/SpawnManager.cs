using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] SpawnPointEnemy[] enemies;
    [SerializeField] SpawnPointWeapon[] weapons;

    GameObject player;

    bool canSpawn;

    int numEnemiesSpawned;
    float nextEnemySpawn;

    public float enemySpawnDelay = 5f;
    public float enemySpawnDelayDecayRate = 0.1f;
    public float enemySpawnDelayMin = 0.1f;
    public float distanceToPlayer = 10f;
    public int minimumNumberOfEnemiesAlive = 3;

    int numWeaponsSpawned;
    float nextWeaponSpawn;

    public void Init() {
        player = GameObject.Find("Player");
    }

    public void StartGame() {
        numEnemiesSpawned = 0;
        numWeaponsSpawned = 0;
        nextEnemySpawn = GetNextEnemySpawn();
        nextWeaponSpawn = GetNextWeaponSpawn();
        canSpawn = true;
    }

    float GetNextEnemySpawn() {
        return Time.time + enemySpawnDelay;
    }

    float GetNextWeaponSpawn() {
        return Time.time + 1f;
    }

    public void EndGame() {
        canSpawn = false;
    }

    int CountEnemies() {
        return FindObjectsOfType<Enemy>().Length;
    }

    private void Update() {
        if (!canSpawn)
            return;

        if (Time.time >= nextEnemySpawn || CountEnemies() <= minimumNumberOfEnemiesAlive)
            SpawnEnemy();

        if (Time.time >= nextWeaponSpawn)
            SpawnWeapon();
    }

    void SpawnEnemy() {
        numEnemiesSpawned++;
        enemySpawnDelay = Mathf.Max(enemySpawnDelayMin, enemySpawnDelay - (enemySpawnDelay * enemySpawnDelayDecayRate));
        nextEnemySpawn = GetNextEnemySpawn();
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(0f, 1f); // they always spawn above the player
        Vector2 dir = new Vector2(x, y).normalized;
        Vector2 playerPos = player.transform.position;
        Vector2 pos = dir * distanceToPlayer + playerPos;
        Instantiate(Resources.Load("Enemies/TestEnemy"), pos, Quaternion.identity);
    }

    void SpawnWeapon() {
        numWeaponsSpawned++;
        nextWeaponSpawn = GetNextWeaponSpawn();
        Instantiate(Resources.Load("Pickups/TestWeaponPickup"), weapons[Random.Range(0, weapons.Length)].transform.position, Quaternion.identity);
    }

#if UNITY_EDITOR
    void Reset() {
        enemies = FindObjectsOfType<SpawnPointEnemy>();
        weapons = FindObjectsOfType<SpawnPointWeapon>();
    }

    private static float gizmoSize = .2f;

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        for (int i = 0; i < weapons.Length; i++) {
            Gizmos.DrawWireSphere(weapons[i].transform.position, gizmoSize);
        }
        Gizmos.color = Color.red;

        for (int i = 0; i < enemies.Length; i++) {
            Gizmos.DrawWireSphere(enemies[i].transform.position, gizmoSize);
        }
    }

#endif
}

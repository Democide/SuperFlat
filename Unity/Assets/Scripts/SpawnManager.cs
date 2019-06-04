using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] SpawnPointEnemy[] enemies;
    [SerializeField] SpawnPointWeapon[] weapons;

    bool canSpawn;

    int numEnemiesSpawned;
    float nextEnemySpawn;

    int numWeaponsSpawned;
    float nextWeaponSpawn;

    public void Init() {

    }

    public void StartGame() {
        numEnemiesSpawned = 0;
        numWeaponsSpawned = 0;
        nextEnemySpawn = GetNextEnemySpawn();
        nextWeaponSpawn = GetNextWeaponSpawn();
        canSpawn = true;
    }

    float GetNextEnemySpawn() {
        return Time.time + 10f;
    }

    float GetNextWeaponSpawn() {
        return Time.time + 1f;
    }

    public void EndGame() {
        canSpawn = false;
    }

    private void Update() {
        if (!canSpawn)
            return;

        if (Time.time >= nextEnemySpawn)
            SpawnEnemy();

        if (Time.time >= nextWeaponSpawn)
            SpawnWeapon();
    }

    void SpawnEnemy() {
        numEnemiesSpawned++;
        nextEnemySpawn = GetNextEnemySpawn();
        Instantiate(Resources.Load("Enemies/TestEnemy"), enemies[Random.Range(0, enemies.Length)].transform.position, Quaternion.identity);
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

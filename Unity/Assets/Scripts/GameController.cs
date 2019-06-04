using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    bool isRunning;

    public TimeController timeController;
    public SpawnManager spawnmanager;
    public GUIManager guiManager;
    public Player player;

    private void Start() {
        timeController.Init();
        spawnmanager.Init();
        guiManager.Init();
    }

    void StartGame() {
        isRunning = true;
        player.StartGame();
        guiManager.StartGame();
        spawnmanager.StartGame();
        timeController.StartGame();
        Debug.Log("GAME STARTED!");
    }

    void EndGame() {
        isRunning = false;
        guiManager.EndGame();
        spawnmanager.EndGame();
        timeController.EndGame();
        Debug.Log("GAME ENDED!");
    }

    private void Update() {
        guiManager.UpdateTime(timeController.timeElapsed);
        guiManager.UpdateScore(0);
        guiManager.UpdateWeapon(player.GetWeapon());

        if (isRunning) {
            if (!player.IsAlive()) { 
                EndGame();
                return;
            }

            DoPlayerTimeCheck();
        }
        else {
            if (Input.GetKeyDown(KeyCode.R))
                StartGame();
        }
    }

    bool isPlayerMoving;

    void DoPlayerTimeCheck() {

    }

}

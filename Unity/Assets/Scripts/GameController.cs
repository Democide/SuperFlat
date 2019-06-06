using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool isRunning;

    public TimeController timeController;
    public SpawnManager spawnmanager;
    public GUIManager guiManager;
    public Player player;
    bool dirty;

    public float timeBoostOnStart = 0.5f;

    public int Score;

    private void Start() {
        timeController.Init();
        spawnmanager.Init();
        guiManager.Init();
        player.Init();
    }

    void StartGame() {
        isRunning = true;
        player.StartGame();
        guiManager.StartGame();
        spawnmanager.StartGame();
        timeController.StartGame();
        Debug.Log("GAME STARTED!");
        timeController.TimeBoost(timeBoostOnStart);
    }

    void EndGame() {
        isRunning = false;
        guiManager.EndGame();
        spawnmanager.EndGame();
        timeController.EndGame();
        Debug.Log("GAME ENDED!");
        dirty = true;
    }

    private void Update() {
        guiManager.UpdateTime(timeController.timeElapsed);
        guiManager.UpdateScore(Score);
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
                if (dirty) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
                }
            else {
                StartGame();
                }
        }
    }

    bool isPlayerMoving;

    void DoPlayerTimeCheck() {
        if (player.HasActed())
            timeController.SetTimeScale();
    }

}

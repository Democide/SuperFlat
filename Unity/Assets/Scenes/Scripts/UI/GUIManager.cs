using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour {

    public TextMeshProUGUI time;
    public TextMeshProUGUI score;
    public TextMeshProUGUI weapon;

    public TextMeshProUGUI title;
    public TextMeshProUGUI pressToStart;

    bool gameOver;

    public void Init() {
        ShowTitle(true, true);
    }

    public void StartGame() {
        gameOver = false;
        ShowTitle(false);
    }

    public void EndGame() {
        gameOver = true;
        ShowTitle(true);
    }

    void ShowTitle(bool show, bool isFirst = false) {
        title.gameObject.SetActive(show);
        pressToStart.gameObject.SetActive(show);

        if (show) {
            if (!isFirst) { 
                title.text = "Game Over";
            }
            else {
                title.text = "Super Flat";
                time.gameObject.SetActive(false);
                score.gameObject.SetActive(false);
                weapon.gameObject.SetActive(false);
            }
        }
        else {
            time.gameObject.SetActive(true);
            score.gameObject.SetActive(true);
            weapon.gameObject.SetActive(true);
        }
    }

    public void UpdateTime(float time) {
        this.time.text = time.ToString();// string.Format("mm:ss:fff", time);
    }

    public void UpdateScore(float score) {
        this.score.text = score.ToString();// string.Format("N0", score);
    }

    public void UpdateWeapon(Weapon newWeapon) {
        this.weapon.text = newWeapon != null ? newWeapon.name : "NONE";
    }
}

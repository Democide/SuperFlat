using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public string Scene = "Level0";

    void Start()
    {
        SceneManager.LoadScene(Scene, LoadSceneMode.Additive);
    }   
}

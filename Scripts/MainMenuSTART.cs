using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSTART : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player") {
            Time.timeScale = 0.4f;
            //start game:
            GameObject.Find("info").GetComponent<DontDestroyThis>().totalCollected = 0;
            GameObject.Find("info").GetComponent<DontDestroyThis>().victory = false;

            //this will make that script invoke the next level (level1)
            GameObject.Find("LevelScriptHolder").GetComponent<LevelScript>().score = GameObject.Find("LevelScriptHolder").GetComponent<LevelScript>().targetScore;
        }
    }
}

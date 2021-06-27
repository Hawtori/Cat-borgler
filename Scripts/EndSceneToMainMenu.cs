using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneToMainMenu : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player") {
            //start game:
            //GameObject.Find("info").GetComponent<DontDestroyThis>().totalCollected = 0;
            //GameObject.Find("info").GetComponent<DontDestroyThis>().victory = false;

            //this will make that script invoke the next level (level1)
            GameObject.Find("LevelScriptHolder").GetComponent<LevelScript>().score = GameObject.Find("LevelScriptHolder").GetComponent<LevelScript>().targetScore;
        }
    }
}

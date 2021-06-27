using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public int targetScore;         // # of collectables in the scene.
    public int score = 0;           // # of collectables collected so far.


    void Update()
    {
        if(score >= targetScore && SceneManager.GetActiveScene().buildIndex == 0) {
            Debug.Log("Going to first scene");
            StartCoroutine(waitSomeTime());
        } else if(score >= targetScore && SceneManager.GetActiveScene().buildIndex == 4) {
            Debug.Log("hi");
            Debug.Log("Going to main menu scene");
            StartCoroutine(waitSomeTime2());
        } else if(score >= targetScore && SceneManager.GetActiveScene().buildIndex == 3) {
            Debug.Log("Game completed!!! Going to gameover scene");
            SoundManagerScript.PlaySound("win");
            Time.timeScale = 0;
            StartCoroutine(waitSomeTime());
        }else if(score >= targetScore) {
            //SoundManagerScript.PlaySound("next_level");
            Time.timeScale = 0;
            StartCoroutine(waitSomeTime());
        }
    }
    public static IEnumerator waitSomeTime() {
            Debug.Log("Going to next level");
        yield return new WaitForSecondsRealtime(0.5f);
        GameObject.Find("LevelChanger").GetComponent<LeverChangerScript>().fadeToLevel(1 + SceneManager.GetActiveScene().buildIndex);
    }
    public static IEnumerator waitSomeTime2() {
        yield return new WaitForSecondsRealtime(0.5f);
        GameObject.Find("LevelChanger").GetComponent<LeverChangerScript>().fadeToLevel(0);
    }
}

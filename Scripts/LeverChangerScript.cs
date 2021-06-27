using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeverChangerScript : MonoBehaviour
{
    public Animator anim;

    private int levelToLoad;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void fadeToLevel(int levelIndex) {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }

    public void fadeToEnd()
    {
        levelToLoad = 4;
        anim.SetTrigger("FadeOut");
    }

    public void onFadeComplete() {
        //Debug.Log("FADE DONEEE");
        SceneManager.LoadScene(levelToLoad);
    }

}

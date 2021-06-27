using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyThis : MonoBehaviour
{

    // This gameobject holds information we want to keep as we change scenes.

    public int totalCollected; // Total stuff collected throughout the game.
    public bool victory;

    private static bool aayush;
    void Start ()
    {
        totalCollected = 0;
        victory = true;
        if(!aayush) 
        {
            aayush = true;
            DontDestroyOnLoad(transform.gameObject);
        } else Destroy(gameObject);
    }
}

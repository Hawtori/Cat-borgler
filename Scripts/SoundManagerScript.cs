using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip item_pickup, spotted, win, next_level;
    public static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        item_pickup = Resources.Load<AudioClip>("Sounds/item_pickup");
        spotted = Resources.Load<AudioClip>("Sounds/spotted");
        win = Resources.Load<AudioClip>("Sounds/win");
        next_level = Resources.Load<AudioClip>("Sounds/next_level");
        
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip) {
        if(clip == "item_pickup") audioSrc.PlayOneShot(item_pickup);
        if(clip == "spotted") audioSrc.PlayOneShot(spotted);
        if(clip == "win") audioSrc.PlayOneShot(win);
        if(clip == "next_level") audioSrc.PlayOneShot(next_level);

        //if(clip == "trigger") {
        //    audioSrc.volume = 0.5f;
        //    audioSrc.PlayOneShot(trigger);
        //    audioSrc.volume = 1;
        //}
        //if(clip == "gameMusic") audioSrc.PlayOneShot(gameMusic);
    }
}

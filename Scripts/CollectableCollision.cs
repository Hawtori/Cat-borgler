using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCollision : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col) {
        // If player collides with this, it means they've "collected" it, so we increment score in LevelScript and destroy this collectable.
        if(col.gameObject.tag == "Player") {
            
            SoundManagerScript.PlaySound("item_pickup");
            GameObject.Find("LevelScriptHolder").GetComponent<LevelScript>().score += 1;        // Increase score.
            //GameObject.Find("info").GetComponent<DontDestroyThis>().totalCollected += 1;      // Uncomment this later
            Destroy(gameObject);                                                                // Destroy the collectable 
        }
    }

}

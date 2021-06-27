using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseGridShower : MonoBehaviour
{
    void Start()
    {
        Debug.Log("HI!!!!!!!!!!");
        bool status = GameObject.Find("info").GetComponent<DontDestroyThis>().victory;
        Debug.Log(status);
        if(!status) {
            GameObject.Find("LoseGrid").transform.position = new Vector3(0, 0, -1);
            GameObject.Find("WinGrid").transform.position = new Vector3(999, 999, -1);
        }else{
            GameObject.Find("LoseGrid").transform.position = new Vector3(999, 999, -1);
            GameObject.Find("WinGrid").transform.position = new Vector3(0, 0, -1);
        }
    }
}

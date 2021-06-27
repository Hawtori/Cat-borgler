using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAlert : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}

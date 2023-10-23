using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGate : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject SceneManagerObject = GameObject.Find("ScenesManager");
            ScenesManager ScenesManagerScript = SceneManagerObject.GetComponent<ScenesManager>();

            ScenesManagerScript.LoadNextLevel();
        }
    }
}

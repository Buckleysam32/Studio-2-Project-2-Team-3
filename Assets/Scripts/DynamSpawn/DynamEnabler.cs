using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamEnabler : MonoBehaviour
{
    /// <summary>
    /// Enable spawners if they have the spawner script
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DynamPrefabSpawn enableBool;
        if (collision.GetComponent<DynamPrefabSpawn>())
        {
            enableBool = collision.GetComponent<DynamPrefabSpawn>();
            enableBool.eligable = true;
        }
    }
    /// <summary>
    /// Disable spawners if the have the spawner script
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        DynamPrefabSpawn enableBool;
        if (collision.GetComponent<DynamPrefabSpawn>())
        {
            enableBool = collision.GetComponent<DynamPrefabSpawn>();
            enableBool.eligable = false;
        }
    }
}

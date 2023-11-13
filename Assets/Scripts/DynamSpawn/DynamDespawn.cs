using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamDespawn : MonoBehaviour
{
    /// <summary>
    /// Despawn objects it comes into contact with if that object has the tag "Dynamic"
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Dynamic"))
        {
            Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpill : MonoBehaviour
{
    /// <summary>
    /// If something with a playercontroller enters, turn on the slippery bool
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController enableBool;
        if (collision.GetComponent<PlayerController>())
        {
            enableBool = collision.GetComponent<PlayerController>();
            enableBool.slippery = true;
        }
    }
    /// <summary>
    /// If something with a playercontroller exits, turn off the slippery bool
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController enableBool;
        if (collision.GetComponent<PlayerController>())
        {
            enableBool = collision.GetComponent<PlayerController>();
            enableBool.slippery = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] GameObject followThis;
    [SerializeField] bool position;
    [SerializeField] bool rotation;

    void Update()
    {
        if (position)
        {
            this.transform.position = followThis.transform.position;
        }
        if (rotation)
        {
            this.transform .rotation = followThis.transform.rotation;
        }
    }
}

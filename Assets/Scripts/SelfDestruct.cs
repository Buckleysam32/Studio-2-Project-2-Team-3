using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public bool selfDestruct = true; //While true, timer ticks. If not true, timer freezes
    [SerializeField] float timer; //How long until this object self destructs
    private float timePass;
    void Update()
    {
        if (selfDestruct)
        {
            timePass += Time.deltaTime;
            if (timePass >= timer)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

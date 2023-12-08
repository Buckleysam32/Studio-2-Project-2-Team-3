using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireMarks : MonoBehaviour
{
    public PlayerController playerController;
    TrailRenderer trailRenderer;

    private void Awake()
    {


        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;
    }

    private void Update()
    {
        if (playerController.IsLeavingMark(out float lateralVelocity, out bool isBraking))
        {
            trailRenderer.emitting = true;
            GameEventsManager.instance.audioEvents.PlayOneShot("CarScreechHD");
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
}

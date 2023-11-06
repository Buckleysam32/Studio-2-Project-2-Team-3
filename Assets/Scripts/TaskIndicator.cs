using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class TaskIndicator : MonoBehaviour
{
    public GameObject target;
    public float speed = 1.0f;

    private void OnEnable()
    {
        GameEventsManager.instance.uiEvents.onActivateIndicator += SetIndicatorActive;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.uiEvents.onActivateIndicator -= SetIndicatorActive;
    }

    private void Update()
    {
        if(target != null)
        {
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        }
    }

    private void SetIndicatorActive(GameObject targetGO)
    {
        target = targetGO;
    }

}

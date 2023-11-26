using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCarAnim : MonoBehaviour
{
    [SerializeField] Vector3 startPosition = new Vector3(0, 0, 0);
    [SerializeField] Vector3 maxPosition = new Vector3(0, 0, 0); //Maximum position
    [SerializeField] Vector3 minPosition = new Vector3(0, 0, 0); //Minimum position
    private Vector3 selectedPosition;
    [SerializeField] float speed = 1; //Speed multiplier; 1 = default speed, 3 = triple speed, 0.5 = half speed
    private bool position = false;
    private float movement;

    // Update is called once per frame
    void Update()
    {
        if (position)
        {
            GoToPosition();
        }
        else
        {
            FindNewPosition();
        }
    }

    /// <summary>
    /// Set selected position to a random vector3 that's in between the max and min positions, also redefine start position using current position
    /// </summary>
    private void FindNewPosition()
    {
        startPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 v = maxPosition - minPosition;
        selectedPosition = minPosition + Random.value * v;
        position = true;
    }

    /// <summary>
    /// Smoothly move this transform to the selected position from the starting position
    /// </summary>
    private void GoToPosition()
    {
        if (this.transform.position == selectedPosition)
        {
            movement = 0;
            position = false;
        }
        else
        {
            this.transform.SetPositionAndRotation(Vector3.MoveTowards(startPosition, selectedPosition, movement), Quaternion.Euler(0, 0, 0));
            movement += (Time.deltaTime * speed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStreetAnim : MonoBehaviour
{
    [SerializeField] Vector3 startPosition = new Vector3(27, -13.5f, 0); //Starting position
    [SerializeField] Vector3 endPosition = new Vector3(49, 24.5f, 0); //Ending position
    [SerializeField] float speed = 1; //Speed multiplier; 1 = default speed, 3 = triple speed, 0.5 = half speed
    private float movement;

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position == endPosition)
        {
            movement = 0;
            this.transform.SetPositionAndRotation(startPosition, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            this.transform.SetPositionAndRotation(Vector3.MoveTowards(startPosition, endPosition, movement), Quaternion.Euler(0, 0, 0));
            movement += (Time.deltaTime * speed);
        }
    }
}

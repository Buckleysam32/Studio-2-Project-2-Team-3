using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public float bopSpeed = 1.0f; // Adjust the bop speed as needed
    public float bopHeight = 0.5f; // Adjust the bop height as needed

    private float initialYPosition;

    void Start()
    {
        // Store the initial Y position of the object
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        // Calculate the bop motion using a sine wave
        float bopOffset = Mathf.Sin(Time.time * bopSpeed) * bopHeight;

        // Apply the bop motion to the object's Y position
        Vector3 newPosition = transform.position;
        newPosition.y = initialYPosition + bopOffset;
        transform.position = newPosition;
    }

}

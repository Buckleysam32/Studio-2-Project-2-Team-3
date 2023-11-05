using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraSystem : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        Vector3 movePosition = playerTransform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}

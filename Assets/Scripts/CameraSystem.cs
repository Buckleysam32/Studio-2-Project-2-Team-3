using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraSystem : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float damping;

    public Rigidbody2D playerRB;

    public float playerZoom;

    public float zoomFactor;

    public Camera OrthographicCamera;

    private Vector3 velocity = Vector3.zero;

    public void Awake()
    {
        OrthographicCamera = GetComponent<Camera>();
        playerZoom = 5f;
    }

    private void Update()
    {
        Vector3 movePosition = playerTransform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        playerZoom = playerRB.velocity.magnitude / 10 + 8f;
        OrthographicCamera.orthographicSize = playerZoom;
    }
}
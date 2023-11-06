using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.95f; // How much the car will drift
    public float accelerationFactor = 30.0f; // How fast the car will accelerate
    public float turnFactor = 3.5f; // How fast the car will turn
    public float maxSpeed = 20; // Max speed of the car

    // Local Variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;
    float rotationBuffer = 0;
    int spriteDirection = 8;
    [SerializeField] Animator spriteAnim;

    float velocityVsUp = 0;

    // Components
    Rigidbody2D carRigidbody2D;

    private void Awake()
    {
        // Assign the car's rigidbody
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyAcceleration();

        KillOrthogonalVelocity();

        ApplySteering();
    }
    
    /// <summary>
    /// Creates and applies acceleration force to the player
    /// </summary>
    void ApplyAcceleration()
    {
        // Calculate how much force is present in terms of direction of velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        // Prevent player from going faster when max foward speed is reached
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        // Prevent player from going faster when max reverse speed is reached
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        // Prevent player from going faster when max sideways speed is reached
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        // Slow acceleration when there is no input
        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRigidbody2D.drag = 0;
        }

        // Create acceleration force
        Vector2 accelerationForceVector = transform.up * accelerationInput * accelerationFactor;

        // Apply the acceleration force
        carRigidbody2D.AddForce(accelerationForceVector, ForceMode2D.Force);
    }

    /// <summary>
    /// Applies the steering to the player rigidbody
    /// </summary>
    void ApplySteering()
    {
        // Only let car turn when moving
        float minSpeedBeforeTurn = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeTurn = Mathf.Clamp01(minSpeedBeforeTurn);

        // Update car's rotation angle
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeTurn;

        // Apply steering
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    /// <summary>
    /// Handles the animator's use of the sprites
    /// </summary>
    void DirectionSpriteAnimation()
    {
        if (spriteDirection == 1)
        {
            rotationBuffer = 135;
        }
        else if (spriteDirection == 2)
        {
            rotationBuffer = 180;
        }
        else if (spriteDirection == 3)
        {
            rotationBuffer = -135;
        }
        else if (spriteDirection == 4)
        {
            rotationBuffer = 90;
        }
        else if (spriteDirection == 6)
        {
            rotationBuffer = -90;
        }
        else if (spriteDirection == 7)
        {
            rotationBuffer = 45;
        }
        else if (spriteDirection == 8)
        {
            rotationBuffer = 0;
        }
        else if (spriteDirection == 9)
        {
            rotationBuffer = -45;
        }
        rotationBuffer = (rotationAngle - rotationBuffer);
    }



    /// <summary>
    /// Adds friction to the car
    /// </summary>
    public void KillOrthogonalVelocity()
    {
        Vector2 fowardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up); // The car's foward velocity
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right); // The car's right velocity

        carRigidbody2D.velocity = fowardVelocity + rightVelocity * driftFactor;
    }

    /// <summary>
    /// Applies the player input
    /// </summary>
    /// <param name="inputVector"></param>
    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }


}

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

    //Sprite animation related variables
    float rotationAngleAnim = 0;
    float rotationBuffer = 0;
    public int spriteDirection = 8;
    [SerializeField] Animator spriteAnim;


    [SerializeField] GameObject impactParticle;

    float velocityVsUp = 0;

    // Components
    Rigidbody2D carRigidbody2D;

    // Special condition
    public bool slippery;
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

        DirectionSpriteAnimation();
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

        // Slippery System
        if (slippery)
        {
            int rndm = Random.Range(0, 2);
            if (rndm == 1)
            {
                rndm = Random.Range(0, 2);
                if (rndm == 1)
                {
                    steeringInput = 2f;
                }
                else
                {
                    steeringInput = -2f;
                }
            }
        }

        // Update car's rotation angle
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeTurn;

        // Apply steering
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    /// <summary>
    /// Handles the animator's use of the sprites' rotation direction
    /// </summary>
    void DirectionSpriteAnimation()
    {
        //First, set axis buffer for current direction
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
            rotationBuffer = 225;
        }
        else if (spriteDirection == 4)
        {
            rotationBuffer = 90;
        }
        else if (spriteDirection == 6)
        {
            rotationBuffer = 270;
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
            rotationBuffer = 315;
        }
        rotationAngleAnim = (this.transform.localEulerAngles.z - rotationBuffer); //Check angle
        if(rotationAngleAnim > 22.5f && rotationAngleAnim < 95) //Turn left
        {
            if (spriteDirection == 1)
            {
                spriteDirection = 2;
            }
            else if (spriteDirection == 2)
            {
                spriteDirection = 3;
            }
            else if (spriteDirection == 3)
            {
                spriteDirection = 6;
            }
            else if (spriteDirection == 6)
            {
                spriteDirection = 9;
            }
            else if (spriteDirection == 9)
            {
                spriteDirection = 8;
            }
            else if (spriteDirection == 8)
            {
                spriteDirection = 7;
            }
            else if(spriteDirection == 7)
            {
                spriteDirection = 4;
            }
            else if(spriteDirection == 4)
            {
                spriteDirection = 1;
            }
        }
        else if(rotationAngleAnim < 337.5f && rotationAngleAnim > 235 || rotationAngleAnim < -22.5f && rotationAngleAnim > -95) //Turn right
        {
            if (spriteDirection == 1)
            {
                spriteDirection = 4;
            }
            else if (spriteDirection == 4)
            {
                spriteDirection = 7;
            }
            else if (spriteDirection == 7)
            {
                spriteDirection = 8;
            }
            else if (spriteDirection == 8)
            {
                spriteDirection = 9;
            }
            else if (spriteDirection == 9)
            {
                spriteDirection = 6;
            }
            else if (spriteDirection == 6)
            {
                spriteDirection = 3;
            }
            else if (spriteDirection == 3)
            {
                spriteDirection = 2;
            }
            else if (spriteDirection == 2)
            {
                spriteDirection = 1;
            }
        }
        spriteAnim.SetInteger("RotationDirection", spriteDirection);

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

    float GetLateralVeolcity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsLeavingMark(out float lateralVeolcity, out bool isBraking)
    {
        lateralVeolcity = GetLateralVeolcity();
        isBraking = false;

        if (accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        else if (Mathf.Abs(GetLateralVeolcity()) > 0.93f || slippery && Mathf.Abs(GetLateralVeolcity()) > 0.45f)
        {
            return true;
        }

        else
        {
            return false;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {;
        Instantiate<GameObject>(impactParticle, this.transform);

        // apply some damage to the current package
        GameEventsManager.instance.rewardEvents.PlayerCrashed(10f);

        GameEventsManager.instance.audioEvents.PlayOneShot("CrashCollision");
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

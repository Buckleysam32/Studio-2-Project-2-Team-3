using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteAngler : MonoBehaviour
{
    Animator animSprite;
    [SerializeField] int spriteDirection;
    [SerializeField] float rotationBuffer;
    [SerializeField] float curRotation;
    
    void Awake()
    {
        animSprite = GetComponent<Animator>(); //Get animator
    }

    // Update is called once per frame
    void Update()
    {
        FixRotation();
    }

    /// <summary>
    /// Rotates the object in accordance with the sprite displayed
    /// </summary>
    void FixRotation()
    {
        spriteDirection = animSprite.GetInteger("RotationDirection"); //Figure out what sprite is displayed
        curRotation = this.transform.parent.gameObject.transform.localEulerAngles.z + 180; //Find current rotation (and flip it because it displays upside down)

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

        this.transform.rotation = Quaternion.Euler(0, 0, curRotation - rotationBuffer);
    }
}

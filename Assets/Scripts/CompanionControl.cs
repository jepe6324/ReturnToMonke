using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionControl : MonoBehaviour
{
    CompanionMovement movement_;
    void Start()
    {
        movement_ = GetComponent<CompanionMovement>();
    }
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            if (movement_.GetState() == CompanionMovement.State.WALKING)
            {
                movement_.WallHangJump();
                GetComponent<AudioSource>().Play();
            }
            else if (movement_.GetState() == CompanionMovement.State.WALLHANG)
            {
                movement_.JumpOfWall();
                GetComponent<AudioSource>().Play();
            }
            else if (movement_.GetState() == CompanionMovement.State.HOOKHANG)
            {
                movement_.JumpOfHook();
                GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetKeyDown("down") &&
            (movement_.GetState() == CompanionMovement.State.WALKING ||
             movement_.GetState() == CompanionMovement.State.HOLD_POSITION))
		{
            //movement_.ToggleHoldPosition();
		} 
    }
}

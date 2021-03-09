using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CompanionMovement : MonoBehaviour
{
    public enum State
    {
        WALKING,
        JUMPING,
        COMMAND_JUMP,
        WALLHANG,
        HOOKHANG,
    }
    public Transform followTarget_;
    public float jumpPower, acceleration, maxSpeed, maxDistance;
    public GameObject wallBoost, hookBoost;

    State state_ = State.COMMAND_JUMP;
    Rigidbody2D rigidbody_;
    bool touchingWall_, touchingHook_ = false;
    float directionFromWall_;

    float gravity_;
    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        gravity_ = rigidbody_.gravityScale;
    }
    void Update()
    {
        switch (state_)
        {
            case State.WALKING:
                WalkUpdate();
                break;
            case State.JUMPING:
                JumpUpdate();
                break;
            case State.COMMAND_JUMP:
                CommandJumpUpdate();
                break;
            case State.WALLHANG:
                //WallHangUpdate();
                break;
            case State.HOOKHANG:
                //HookHangUpdate();
                break;
        }
    }
    void WalkUpdate()
    {
        rigidbody_.velocity = AccelerateX(acceleration);
	}
    void JumpUpdate() // Land as close to the player as possible
	{
        float originalMaxDistance = maxDistance;
        maxDistance = 0;
        rigidbody_.velocity = AccelerateX(acceleration);
        maxDistance = originalMaxDistance;
	}
    void CommandJumpUpdate()
	{
        if (touchingWall_)
		{
            rigidbody_.velocity = new Vector2(0, 0);
            rigidbody_.gravityScale = 0;
            wallBoost.SetActive(true);
            state_ = State.WALLHANG;
		}
        if (touchingHook_)
		{
            rigidbody_.velocity = new Vector2(0, 0);
            rigidbody_.gravityScale = 0;
            hookBoost.SetActive(true);
            state_ = State.HOOKHANG;
        }
    }
    public void SetTarget(Transform target, float maxDistance)
	{
        followTarget_ = target;
        this.maxDistance = maxDistance;
	}
    float GetDirectionToTarget()
	{
        return Mathf.Sign(followTarget_.position.x - transform.position.x);
    }
    Vector2 AccelerateX(float acceleration)
    {
        float direction = 0;
        float distance = Vector3.Distance(transform.position, followTarget_.position);
        if (distance > maxDistance)
        {
            direction = GetDirectionToTarget();
        }
        Vector2 velocity = rigidbody_.velocity;
        velocity.x += acceleration * direction * Time.deltaTime;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        return velocity;
    }
    public void Landed()
    {
        state_ = State.WALKING;
    }
    public void Falling()
    {
        if (state_ != State.COMMAND_JUMP)
        {
            state_ = State.JUMPING;
        }
    }
    public void WallHangJump()
	{
        float direction = GetDirectionToTarget();
        Vector2 velocity;
        velocity.x = jumpPower * direction;
        velocity.y = jumpPower;
        rigidbody_.velocity = velocity;
        state_ = State.COMMAND_JUMP;
	}
    public void JumpOfWall()
    {
        state_ = State.JUMPING;
        Vector2 velocity;
        velocity.x = (1.5f) * directionFromWall_; 
        velocity.y = jumpPower;
        rigidbody_.velocity = velocity;
        rigidbody_.gravityScale = gravity_;
        wallBoost.SetActive(false);
	}
    public void JumpOfHook()
	{
        state_ = State.JUMPING;
        Vector2 velocity;
        velocity.x = (maxSpeed) * GetDirectionToTarget();
        velocity.y = 1;
        rigidbody_.velocity = velocity;
        rigidbody_.gravityScale = gravity_;
        hookBoost.SetActive(false);
    }
    public State GetState() {
        return state_;
    }
    public void TouchingWall(float direction)
	{
        touchingWall_ = true;
        directionFromWall_ = direction;
	}
    public void NotTouchingWall(){ touchingWall_ = false; }
    public void TouchingHook() { touchingHook_ = true; }
    public void NotTouchingHook() { touchingHook_ = false; }
}

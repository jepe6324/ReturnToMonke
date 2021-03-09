using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CompanionMovement : MonoBehaviour
{
    public enum State
    {
        WALKING,
        JUMPING,
        WALL_HANG_JUMP,
        WALLHANG
    }
    public Transform followTarget_;
    public float jumpPower, acceleration, maxSpeed, maxDistance;
    public GameObject wallBoost;

    State state_ = State.WALL_HANG_JUMP;
    Rigidbody2D rigidbody_;
    bool touchingWall_ = false;
    float directionFromWall_;
    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
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
            case State.WALL_HANG_JUMP:
                WallHangJumpUpdate();
                break;
            case State.WALLHANG:
                //WallHangUpdate();
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
    void WallHangJumpUpdate()
	{
        if (touchingWall_)
		{
            rigidbody_.velocity = new Vector2(0, 0);
            rigidbody_.gravityScale = 0;
            wallBoost.SetActive(true);
            state_ = State.WALLHANG;
		}
    }
    public void SetTarget(Transform target, float maxDistance)
	{
        followTarget_ = target;
        this.maxDistance = maxDistance;
	}
    float GetDirectionToPlayer()
	{
        return Mathf.Sign(followTarget_.position.x - transform.position.x);
    }
    Vector2 AccelerateX(float acceleration)
    {
        float direction = 0;
        float distance = Vector3.Distance(transform.position, followTarget_.position);
        if (distance > maxDistance)
        {
            direction = GetDirectionToPlayer();
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
        if (state_ != State.WALL_HANG_JUMP)
        {
            state_ = State.JUMPING;
        }
    }
    public void WallHangJump()
	{
        float direction = GetDirectionToPlayer();
        Vector2 velocity;
        velocity.x = jumpPower * direction;
        velocity.y = jumpPower;
        rigidbody_.velocity = velocity;
        state_ = State.WALL_HANG_JUMP;
	}
    public void JumpOfWall()
    {
        state_ = State.JUMPING;
        Vector2 velocity;
        velocity.x = (1.5f) * directionFromWall_; 
        velocity.y = jumpPower;
        rigidbody_.velocity = velocity;
        rigidbody_.gravityScale = 0.5f;
        wallBoost.SetActive(false);
	}
    public State GetState() {
        return state_;
    }
    public void TouchingWall(float direction)
	{
        touchingWall_ = true;
        directionFromWall_ = direction;
	}
    public void NotTouchingWall()
	{
        touchingWall_ = false;
	}
}

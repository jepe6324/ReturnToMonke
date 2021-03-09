using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum State
    {
        WALKING,
        JUMPING
    }

    public float jumpPower, acceleration, airAcceleration, maxSpeed;
    
    private State state_ = State.JUMPING;
    private Rigidbody2D rigidbody_;
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
        }
    }
    void WalkUpdate()
    {
        Vector2 velocity = AccelerateX(acceleration);

        if (Input.GetKeyDown("space"))
		{
            velocity.y = jumpPower;
		}
        rigidbody_.velocity = velocity;
    }
    void JumpUpdate()
	{
        rigidbody_.velocity = AccelerateX(airAcceleration);
    }
    Vector2 AccelerateX(float acceleration)
    {
        float direction = Input.GetAxis("Horizontal");
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
        state_ = State.JUMPING;
	}
    public void WallBoost()
	{
        Vector2 velocity = rigidbody_.velocity;
        velocity.y = jumpPower;
        rigidbody_.velocity = velocity;
	}
}

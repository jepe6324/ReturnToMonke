using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        WALKING,
        JUMPING,
        HORIZONTAL_BOOST
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
            case State.HORIZONTAL_BOOST:
                HorizontalBoostUpdate();
                break;
        }
    }
    void WalkUpdate()
    {
        Vector2 velocity = AccelerateX(acceleration);

        if (Input.GetKeyDown("space"))
		{
            velocity.y = jumpPower;
            GetComponent<AudioSource>().Play();
		}
        rigidbody_.velocity = velocity;
    }
    void JumpUpdate()
	{
        rigidbody_.velocity = AccelerateX(airAcceleration);
    }
    void HorizontalBoostUpdate()
	{

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
    public void HookBoost()
	{
        Vector2 velocity = rigidbody_.velocity;
        velocity.x = Mathf.Sign(velocity.x) * 3.0f;
        velocity.y = 1.5f;
        rigidbody_.velocity = velocity;
        state_ = State.HORIZONTAL_BOOST;
	}
    public State GetState()
	{
        return state_;
	}
}

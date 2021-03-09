using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionCollision : MonoBehaviour
{
    public BoxCollider2D playerCollider;

    CompanionMovement movement_;
    BoxCollider2D collider_;
    void Start()
    {
        movement_ = GetComponent<CompanionMovement>();

        collider_ = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(collider_, playerCollider, true);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionIsWithFloor(collision))
        {
            movement_.Landed();
        }
        if (CollisionIsWithWall(collision))
		{
            movement_.TouchingWall(CollisionWithWallDirection(collision));
		}
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!CollisionIsWithWall(collision))
        {
            movement_.NotTouchingWall();
        }
        if (CollisionIsWithFloor(collision) && movement_.GetState() != CompanionMovement.State.WALL_HANG_JUMP)
		{
            movement_.Landed();
		}
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (!CollisionIsWithFloor(collision))
        {
            movement_.Falling();
        }
        if (!CollisionIsWithWall(collision))
		{
            movement_.NotTouchingWall();
		}
    }
    bool CollisionIsWithFloor(Collision2D collision)
    {
        for (int i = 0; i < collision.GetContacts(collision.contacts); i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (normal.y >= 0.5f)
            {
                return true;
            }
        }
        return false;
    }
    bool CollisionIsWithWall(Collision2D collision)
	{
        for (int i = 0; i < collision.GetContacts(collision.contacts); i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (Mathf.Abs(normal.x) >= 0.5f)
            {
                return true;
            }
        }
        return false;
    }
    float CollisionWithWallDirection(Collision2D collision)
	{
        for (int i = 0; i < collision.GetContacts(collision.contacts); i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            if (Mathf.Abs(normal.x) >= 0.5f)
            {
                return Mathf.Sign(normal.x);
            }
        }
        return 0;
    }
}

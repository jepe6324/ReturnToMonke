using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public BoxCollider2D companionCollider;

    PlayerMovement movement_;
    BoxCollider2D collider_;
    void Start()
    {
        movement_ = GetComponent<PlayerMovement>();

        collider_ = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(collider_, companionCollider, true);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollisionIsWithFloor(collision))
        {
            movement_.Landed();
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (CollisionIsWithFloor(collision))
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
    }
    void OnTriggerEnter2D(Collider2D collider)
	{
        if (collider.CompareTag("WallBoost"))
		{
            movement_.WallBoost();
		}
        if (collider.CompareTag("HookBoost"))
		{
            movement_.HookBoost();
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
}

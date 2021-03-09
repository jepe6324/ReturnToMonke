using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Collisions
{
	public enum Direction
	{
		LEFT = 1,
		RIGHT = 2,
		UP = 4,
		DOWN = 8
	}
	bool IsCollisionWithDirection(Direction direction, Collision2D collision)
	{
		for (int i = 0; i < collision.GetContacts(collision.contacts); i++)
		{
			Vector2 normal = collision.GetContact(i).normal;
			
		}
		return false;
	}
	Direction VectorToDirection(Vector2 vector)
	{

	}
}
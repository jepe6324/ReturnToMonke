using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Collisions
{
	public enum Direction
	{
		NONE = 0,
		LEFT = 1,
		RIGHT = 2,
		UP = 4,
		DOWN = 8
	}
	public bool IsCollisionInDirection(Direction direction, Collision2D collision)
	{
		for (int i = 0; i < collision.GetContacts(collision.contacts); i++)
		{
			Vector2 normal = collision.GetContact(i).normal;
			Direction normalDirection = VectorToDirection(normal);
			if ((direction & normalDirection) == direction) {
				return true;
			}
		}
		return false;
	}
	private Direction VectorToDirection(Vector2 vector)
	{
		Direction direction = Direction.NONE;
		if (vector.x > 0.5) {

			direction |= Direction.LEFT;
		}else if (vector.x < -0.5) {
			direction |= Direction.RIGHT;
		}
		if (vector.y > 0.5) {
			direction |= Direction.DOWN;
		}else if (vector.y < -0.5) {
			direction |= Direction.UP;
		} return direction;
	}
}
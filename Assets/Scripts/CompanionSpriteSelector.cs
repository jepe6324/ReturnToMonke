using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSpriteSelector : MonoBehaviour
{
    SpriteRenderer spriteRenderer_;
    CompanionMovement movement_;
    Rigidbody2D rigidBody_;

    public Sprite runSprite, jumpSprite, hangSprite;
    void Start()
    {
        spriteRenderer_ = GetComponent<SpriteRenderer>();
        movement_ = GetComponent<CompanionMovement>();
        rigidBody_ = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
		switch (movement_.GetState())
		{
            case CompanionMovement.State.COMMAND_JUMP:
            case CompanionMovement.State.JUMPING:
                if (spriteRenderer_.sprite != jumpSprite)
				{
                    spriteRenderer_.sprite = jumpSprite;
				}
                break;
            case CompanionMovement.State.HOOKHANG:
            case CompanionMovement.State.WALLHANG:
                if (spriteRenderer_.sprite != hangSprite)
				{
                    spriteRenderer_.sprite = hangSprite;
				}
                break;
            case CompanionMovement.State.WALKING:
                if (spriteRenderer_.sprite != runSprite)
                {
                    spriteRenderer_.sprite = runSprite;
                }
                break;
		}

        if (rigidBody_.velocity.x > 0)
		{
            spriteRenderer_.flipX = false;
		}
        else if (rigidBody_.velocity.x < 0)
        {
            spriteRenderer_.flipX = true;
		}
    }
}

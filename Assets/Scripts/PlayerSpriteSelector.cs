using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteSelector : MonoBehaviour
{
    SpriteRenderer spriteRenderer_;
    PlayerMovement movement_;
    Rigidbody2D rigidBody_;

    public Sprite runSprite, jumpSprite;
    void Start()
    {
        spriteRenderer_ = GetComponent<SpriteRenderer>();
        movement_ = GetComponent<PlayerMovement>();
        rigidBody_ = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        switch (movement_.GetState())
        {
            case PlayerMovement.State.JUMPING:
                if (spriteRenderer_.sprite != jumpSprite)
                {
                    spriteRenderer_.sprite = jumpSprite;
                }
                break;
            case PlayerMovement.State.WALKING:
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

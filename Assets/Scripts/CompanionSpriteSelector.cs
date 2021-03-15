using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSpriteSelector : MonoBehaviour
{
    SpriteRenderer spriteRenderer_;
    CompanionMovement movement_;

    public Sprite runSprite, jumpSprite, hangSprite;
    void Start()
    {
        spriteRenderer_ = GetComponent<SpriteRenderer>();
        movement_ = GetComponent<CompanionMovement>();
    }
    void Update()
    {
        
    }
}

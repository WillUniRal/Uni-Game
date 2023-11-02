using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private enum ItemType
    {
        gun,
        regenPotion,
        sheildPotion,
        spellOfJump,
        spellOfSpeed,
        spellOfInvisiblity
    }

    [SerializeField] private ItemType type; // mmm dropdown yum

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player == null) return;
            // Please ignore my bad code I did for omar. No powerup class :(
            switch (type)
            { 
                case ItemType.gun:
                    player.PickUpGun();
                    break;
                case ItemType.regenPotion:
                    player.ApplyRegen(5f);
                    break;
                case ItemType.spellOfJump:
                    player.ApplyJumpBoost(5f);
                    break;
                case ItemType.spellOfSpeed:
                    player.ApplySpeedBoost(5f);
                    break;
                case ItemType.spellOfInvisiblity:
                    player.ApplyInvis(5f);
                    break;
            }
            Destroy(gameObject);
            Destroy(this);
        }
    }

}

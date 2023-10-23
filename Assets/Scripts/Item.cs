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

    [SerializeField] private ItemType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            switch (type)
            {
                case ItemType.gun:
                    break;
                case ItemType.regenPotion:
                    break;
                case ItemType.sheildPotion:
                    break;
                case ItemType.spellOfJump:
                    break;
                case ItemType.spellOfSpeed:
                    break;
                case ItemType.spellOfInvisiblity:
                    break;
            }
            Destroy(this);
        }
    }

}

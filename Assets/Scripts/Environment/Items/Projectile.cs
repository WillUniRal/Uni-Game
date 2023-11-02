using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private enum Target
    {
        player,
        enemy
    }
    [SerializeField] private Target target; // creates a nice drop down in inspector
    private float lifetime;
    void Update()
    {
        lifetime += Time.deltaTime;

        if(lifetime>5f)
        {
            Destroy(gameObject); // Don't want infinite amounts of bullets do we
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject collider = collision.gameObject;
        if (target == Target.player) { //ignore player if shot from player
            Player player = collider.GetComponent<Player>();
            if (player == null) return;
            player.TakeDamage(20);
        }
        if (target == Target.enemy)  { //ignore enemy if shot from enemy 
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy == null) return;
            enemy.TakeDamage(50);
        }
        Destroy(gameObject);
    }
}

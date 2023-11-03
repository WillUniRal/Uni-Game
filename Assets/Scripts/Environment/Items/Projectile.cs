using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private enum Target
    {
        player,
        enemy
    }
    private enum Type
    {
        scanline,
        velocity
    }
    [SerializeField] private Target target; // creates a nice drop down in inspector
    [SerializeField] private Type bulletType;
    private float lifetime;
    void Update()
    {
        lifetime += Time.deltaTime;

        if(lifetime>5f)
        {
            Destroy(gameObject); // Don't want infinite amounts of bullets do we
        }
        if (bulletType != Type.scanline) return;
        Debug.Log("beepboop");
        transform.Translate(new Vector3(Time.deltaTime * 5f, 0f, 0f));

    }
    private void OnCollide(Collider2D col = null,Collision2D col2= null) 
    {
        GameObject collider;
        if (col != null) collider = col.gameObject;
        else if (col2 != null) collider = col2.gameObject;
        else return;
        if (target == Target.player)
        { //ignore player if shot from player
            Player player = collider.GetComponent<Player>();
            if (player == null) return;
            player.TakeDamage(20);
        }
        if (target == Target.enemy)
        { //ignore enemy if shot from enemy 
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy == null) return;
            enemy.TakeDamage(50);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(bulletType==Type.scanline) return;
        OnCollide(null,collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == Type.velocity) return;
        OnCollide(collision);
    }
}

using UnityEngine;
using System;
public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private float fireRate = 0.7f;

    private float nextFireTime;

    private void Update()
    {
        if (Time.time >= nextFireTime && Input.GetMouseButtonDown(0))
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public void Shoot() // the Shoot method
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // Get physics cos why not
        //PEW lossy scale is global scale, cos it needs the scale of the player as well
        rb.AddForce(new Vector3(-Math.Sign(transform.lossyScale.x),0f,0f) * bulletForce, ForceMode2D.Impulse); 
       
    }
}


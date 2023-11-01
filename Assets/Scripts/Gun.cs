using UnityEngine;

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

    public void Shoot() // Make the Shoot method public
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}


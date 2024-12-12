using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; // Enemy movement speed
    public GameObject bulletPrefab; // Bullet prefab
    public Transform attackPoint; // Attack point to fire bullets from
    public float fireRate = 2f; // Bullets fired per second
    public int playerLives = 3; // Number of player lives
     // Reference to the Lives UI Text

    private float nextFireTime = 0f;

    [Header("Score Setting")]
    public int scoreValue = 10;

    void Update()
    {
        // Move the enemy downward
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Check if it's time to fire a bullet
        if (Time.time > nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void FireBullet()
    {
        // Instantiate the bullet at the attack point's position and ensure it's oriented correctly
        Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);
    }
    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
    }
  



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Move the bullet downwards
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    // Detect collision with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle the collision with the player (destroy bullet)
            Destroy(this.gameObject); // Destroy the bullet
            Debug.Log("Enemy bullet hit the player");
        }
    }
}

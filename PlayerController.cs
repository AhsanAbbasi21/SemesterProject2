using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject missile;
    public Transform missileSpawnPosition;
    public float destroyTime = 5f;
    public Transform muzzleSpawnPosition;
    public AudioClip fireSound;
    private AudioSource audioSource;

    private int lives = 3; // Player lives

    private void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlayerMovement()
    {
        float xpos = Input.GetAxis("Horizontal");
        float ypos = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xpos, ypos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMissile();
            SpawnMuzzleFlash();
            audioSource.PlayOneShot(fireSound);
        }
    }

    void SpawnMissile()
    {
        GameObject gm = Instantiate(missile, missileSpawnPosition);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }

    void SpawnMuzzleFlash()
    {
        GameObject muzzle = Instantiate(GameManager.instance.muzzleFlash, muzzleSpawnPosition);
        muzzle.transform.SetParent(null);
        Destroy(muzzle, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance != null)
        {
            // Check collision with enemies or bullets
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
            {
                lives--; // Decrease lives
                Debug.Log("Player hit! Lives left: " + lives);

                if (lives <= 0)
                {
                    // Player dies
                    GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
                    Destroy(gm, 2f); // Destroy explosion effect after 2 seconds
                    Destroy(this.gameObject); // Destroy the player
                    GameManager.instance.GameOver();
                }
            }
        }
        else
        {
            Debug.LogError("GameManager.instance is null!");
        }
    }
}

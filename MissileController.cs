using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f;
    public AudioClip collisionSound;
    public AudioSource audioSource;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
            Destroy(gm, 2f);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);


            if (audioSource == null)
            {
                Debug.LogWarning("AudioSource reference is missing! Assign it in the Inspector.");
            }
            else if (!audioSource.enabled)
            {
                Debug.LogWarning("AudioSource is disabled! Enable it in the Inspector or script.");
            }
            else if (collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
                Debug.Log("Collision sound played!");
            }
            else
            {
                Debug.LogWarning("Collision sound is not assigned in the Inspector!");
            }
        }
    }
}

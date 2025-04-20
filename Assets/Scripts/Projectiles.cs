using UnityEngine;
using UnityEngine.InputSystem;


public class Projectiles : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public AudioClip shot;
    AudioSource audioSource;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyContorller enemy = collision.GetComponent<EnemyContorller>();
        if (enemy != null)
        {
            enemy.Fix();
        }
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction * force);
        audioSource.PlayOneShot(shot);
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.magnitude > 100.0f)
        {
            Destroy(gameObject);
        }
    }
}

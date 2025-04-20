using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyContorller : MonoBehaviour
{
    public bool isVertical = false;
    public bool canChangeAxis = false;
    public bool circleMovement = false;
    public float speed = 3.0f;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    int count = 0;
    int steps = 0;
    bool broken = true;
    Rigidbody2D enemy;
    Animator animator;

    AudioSource audioSource;

    public AudioClip fix;

    public ParticleSystem smokeEffect;


    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
            count++;
        }

        if (count > 1 && canChangeAxis)
        {
            count = 0;
            isVertical = !isVertical;
        }

        if (count > 0 && circleMovement)
        {
            count = 0;
            isVertical = !isVertical;
            steps++;


            if (steps == 0 || steps == 1)
            {
                direction = EnsureSign(direction, true);
            }
            else if (steps == 2 || steps == 3)
            {
                direction = EnsureSign(direction, false);
            }
            else
            {
                steps = 0;
            }

        }

    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
        Vector2 position = enemy.position;

        if (isVertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y += speed * direction * Time.deltaTime;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x += speed * direction * Time.deltaTime;
        }

        enemy.MovePosition(position);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    private int EnsureSign(int number, bool positive)
    {
        if (positive)
            return number < 0 ? -number : number;
        else
            return number > 0 ? -number : number;
    }

    public void Fix()
    {
        broken = false;
        enemy.simulated = false;
        smokeEffect.Stop();
        audioSource.Stop();
        audioSource.PlayOneShot(fix);
        animator.SetTrigger("Fixed");
    }
}

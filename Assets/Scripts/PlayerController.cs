using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    public InputAction MoveAction;
    public InputAction talkAction;
    private Rigidbody2D rigidbody2D;
    private Vector2 move;
    public int maxHP = 5;
    private int currentHP = 5;

    public int HP { get { return currentHP; } }

    public float speed = 3.0f;

    public float timeInvincible = 2.0f;
    public bool isInvincible;

    public GameObject projectilePrefab;
    float damageCooldown;

    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    AudioSource audioSource;

    public AudioClip hit;


    void Start()
    {
        Application.targetFrameRate = 60;
        MoveAction.Enable();
        talkAction.Enable();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHP = maxHP;
    }
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FindFriend();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position + move * speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }

            PlaySound(hit);
            animator.SetTrigger("Hit");
            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
        UIHeader.instance.SetHealthValue(currentHP / (float)maxHP);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectiles projectile = projectileObject.GetComponent<Projectiles>();
        projectile.Launch(moveDirection, 300);
        animator.SetTrigger("Launch");
    }


    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));

        if (hit.collider != null)
        {
            NPC character = hit.collider.GetComponent<NPC>();

            if (character != null)
            {
                UIHeader.instance.DisplayDialogue();
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}


using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    public InputAction MoveAction;
    private Rigidbody2D rigidbody2D;
    private Vector2 move;
    public int maxHP = 5;
    private int currentHP = 5;

    public int HP { get { return currentHP; } }

    public float speed = 3.0f;

    public float timeInvincible = 2.0f;
    public bool isInvincible;
    float damageCooldown;


    void Start()
    {
        Application.targetFrameRate = 60;
        MoveAction.Enable();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
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

            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
        UIHeader.instance.SetHealthValue(currentHP / (float)maxHP);
    }

}


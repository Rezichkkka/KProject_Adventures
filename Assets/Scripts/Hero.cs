using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int health; 
    [SerializeField] private float jumpForce = 6f;
    public bool isGrounded = false;

    [SerializeField] private Image[] hearts; 
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    public Joystick joystick;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        lives = 5;
        health = lives;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded) State = States.idle;
        if (joystick.Horizontal != 0)
            Run();
        if (isGrounded && joystick.Vertical > 0.5f)
            Jump();

        if (health > lives)
            health = lives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;

            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    private void Run()
    {
        if (isGrounded) State = States.run;
        Vector3 dir = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        isGrounded = collider.Length > 1;

        if (!isGrounded) State = States.jump;
    }

    public override void GetDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            Die();
        }
    }

    public override void Die()
    {
        foreach (var h in hearts)
            h.sprite = deadHeart;

        GameManager.instance.Loss(); 
        Destroy(gameObject); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WinSign"))
        {
             GameManager.instance.Win(); 
        }

        if (collision.CompareTag("Water"))
        {
            Die(); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Столкновение с врагом");
        }

    }


    
    public void JumpAfterEnemy()
    {
        rb.velocity = Vector2.up * (jumpForce / 2); 
    }

    public enum States
    {
        idle,
        jump,
        run
    }
}


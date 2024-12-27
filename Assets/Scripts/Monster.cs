using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{ 
    
    private float speed = 0.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;
    
    private float directionChangeInterval = 2.5f; 
    private float timeSinceLastChange = 0f;

    
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        lives = 3;
        dir = transform.right;
        sprite.flipX = dir.x < 0.0f;
    }

    private void Update()
    {
        Move();
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= directionChangeInterval)
        {
            ChangeDirection();
            timeSinceLastChange = 0f; 
        }
    }
    
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    }
    private void ChangeDirection()
    {
        dir = -dir;
        sprite.flipX = dir.x < 0.0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            float heroBottomY = Hero.Instance.transform.position.y - Hero.Instance.GetComponent<Collider2D>().bounds.extents.y;
            float monsterTopY = transform.position.y + GetComponent<Collider2D>().bounds.extents.y;
            
            Debug.Log("Hero Bottom Y: " + heroBottomY);
            Debug.Log("Monster Top Y: " + monsterTopY);
            
            RaycastHit2D hit = Physics2D.Raycast(Hero.Instance.transform.position, Vector2.down, 1f); 
            if (hit.collider != null && hit.collider.gameObject == gameObject) 
            {
                GetDamage(); 
                Hero.Instance.JumpAfterEnemy(); 
            }
            else
            {
                Hero.Instance.GetDamage();
            }
        }

        if (lives < 1)
            Die();
    }
}



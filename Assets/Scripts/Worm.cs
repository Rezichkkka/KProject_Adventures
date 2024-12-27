using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Entity
{
    private void Start()
    {
        lives = 3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {

            float heroBottomY = Hero.Instance.transform.position.y - Hero.Instance.GetComponent<Collider2D>().bounds.extents.y;
            float wormTopY = transform.position.y + GetComponent<Collider2D>().bounds.extents.y;

            Debug.Log("Hero Bottom Y: " + heroBottomY);  
            Debug.Log("Worm Top Y: " + wormTopY);      

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

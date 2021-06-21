using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    BoxCollider2D boxCollider2D;
    SpriteRenderer sprite;
    [SerializeField] private LayerMask FloorlayerMask;
    public float jumpVelocity;
    int coin;

   

  

    private void Awake()
    {
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            sprite = GetComponent<SpriteRenderer>();
            coin = GameObject.FindGameObjectsWithTag("Coin").Length;
          
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Isgrounded())
        {
            rb.velocity = Vector2.up * jumpVelocity;
            //anim.SetInteger("State", 2);
        }
        PlayerMovement();

        //Animations
        if(Isgrounded())
        {
            if (rb.velocity.x == 0)
            {
                anim.SetInteger("State", 0);
            }
            
            else
            {
                anim.SetInteger("State", 1);
            }
        }
    }
    private void PlayerMovement()
    {
        float movespeed = 5f;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            print("Left Arrow");
            rb.velocity = new Vector2(-movespeed, rb.velocity.y);
            sprite.flipX = true;
            
           // anim.SetInteger("State", 1);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            print("Right Arrow");
            rb.velocity = new Vector2(+movespeed, rb.velocity.y);
            sprite.flipX = false;
            //anim.SetInteger("State", 1);
            
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
           // anim.SetInteger("State", 0);
        }
    }
    private bool Isgrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, FloorlayerMask);
        //BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, FloorlayerMask);
        //CapsuleCast(Vector2.down, Vector2., CapsuleDirection2D.Vertical, 0, Vector2.down);

        //if (raycastHit2D.collider != null);
        return raycastHit2D.collider != null;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            anim.SetInteger("State", 0);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreTextScript.coinAmount += 1;
        Destroy(collision.gameObject);
        coin--;
       if(coin == 0)
        {
            SceneManager.LoadScene(2);
        }
    
    }

}

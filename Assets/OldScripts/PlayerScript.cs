using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    SpriteRenderer sprite;
    Rigidbody2D rb;
    public float playerSpeed;
    public float jumpVelocity;
    bool isPlayerGrounded = false;
    Animator anim;
    public int coin = 10;
    //GameObject gameObject;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isPlayerGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            Invoke("RepeatLevel", 0.8f);
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "Coin")
    //    {
    //        coin--;
    //    }
    //    if(coin == 0)
    //    {
    //        SceneManager.LoadScene(0);
    //    }
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(collision.gameObject);
    //}

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        if(inputX>0 && isPlayerGrounded == true)
        {
            FlipMovement(false);
            Movement(1);
            anim.SetInteger("State", 1);
            Jump();
        }
        else if(inputX<0 && isPlayerGrounded == true)
        {
            FlipMovement(true);
            Movement(-1);
            anim.SetInteger("State", 1);
            Jump();
        }
        else if(isPlayerGrounded == true)
        {
            anim.SetInteger("State", 0);
            Jump();
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("State", 2);
            //rb.velocity = Vector2.up * playerSpeed;
            rb.AddForce(Vector2.up * jumpVelocity);
            isPlayerGrounded = false;
        }
    }

    private void Movement(int x)
    {
        //rb.velocity = new Vector2(x, 0) * playerSpeed;
        rb.AddForce(new Vector2(x,0) * playerSpeed);
    }

    private void FlipMovement(bool value)
    {
        sprite.flipX = value;
    }
    private void RepeatLevel()
    {
        gameObject.SetActive(true);

        SceneManager.LoadScene(0);
    }

}

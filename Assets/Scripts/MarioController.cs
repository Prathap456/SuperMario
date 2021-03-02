using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarioController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float input1, input2;
    [SerializeField]
    float speed,jumpSpeed;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }


    // Update is called once per frame
    void Update()
    {
         input1 = Input.GetAxis("Horizontal");
         input2 = Input.GetAxis("Jump");
        animator.SetFloat("speed",Mathf.Abs(input1));

        if (input1 > 0)
        {
            rb.AddForce(new Vector2(1,0) * speed * Time.deltaTime);
            sr.flipX = false;
            
            
            

        }else if (input1 < 0)
        {
            rb.AddForce(new Vector2(-1, 0) * speed * Time.deltaTime);
            sr.flipX = true;

        }else if (input2 > 0)
        {
            rb.AddForce(new Vector2(0, 1) * jumpSpeed * Time.deltaTime);
            
            animator.SetBool("isJumping", true);
        }
        else if (input2 < 0)
        {
            
        }else if (input2 == 0)
        {
            animator.SetBool("isJumping", false);
        }
    }
}

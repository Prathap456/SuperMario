//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class MarioController : MonoBehaviour
//{
//    Rigidbody2D rb;
//    SpriteRenderer sr;
//    float input1, input2;
//    [SerializeField]
//    float speed,jumpSpeed;
//    public Animator animator;


//    // Start is called before the first frame update
//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        sr = GetComponent<SpriteRenderer>();

//    }


//    // Update is called once per frame
//    void Update()
//    {
//         input1 = Input.GetAxis("Horizontal");
//         input2 = Input.GetAxis("Jump");
//        animator.SetFloat("speed",Mathf.Abs(input1));

//        if (input1 > 0)
//        {
//            rb.AddForce(new Vector2(1,0) * speed * Time.deltaTime);
//            sr.flipX = false;




//        }else if (input1 < 0)
//        {
//            rb.AddForce(new Vector2(-1, 0) * speed * Time.deltaTime);
//            sr.flipX = true;

//        }else if (input2 > 0)
//        {
//            rb.AddForce(new Vector2(0, 1) * jumpSpeed * Time.deltaTime);

//            animator.SetBool("isJumping", true);
//        }
//        else if (input2 < 0)
//        {

//        }else if (input2 == 0)
//        {
//            animator.SetBool("isJumping", false);
//        }
//    }
//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D capsuleCollider2d;
    SpriteRenderer sprite;
    [SerializeField] private LayerMask FloorlayerMask;
    public float jumpVelocity;
    int coin;
    public float movespeed;





    private void Awake()
    {
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            capsuleCollider2d = GetComponent<CapsuleCollider2D>();
            sprite = GetComponent<SpriteRenderer>();
//            coin = GameObject.FindGameObjectsWithTag("Coin").Length;

        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Isgrounded())
        {
            rb.velocity = Vector2.up * jumpVelocity;
            anim.SetInteger("State", 2);
        }
        PlayerMovement();

       // Animations
        if (Isgrounded())
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
         
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;
            rb.velocity = new Vector2(-movespeed, rb.velocity.y);
            
           

             anim.SetInteger("State", 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;
            rb.velocity = new Vector2(+movespeed, rb.velocity.y);
            
            anim.SetInteger("State", 1);

        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
             anim.SetInteger("State", 0);
        }
    }
    private bool Isgrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(capsuleCollider2d.bounds.center, capsuleCollider2d.bounds.size, 0f, Vector2.down, .1f, FloorlayerMask);
        //BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, FloorlayerMask);
        //CapsuleCast(Vector2.down, Vector2., CapsuleDirection2D.Vertical, 0, Vector2.down);

        //if (raycastHit2D.collider != null);
        return raycastHit2D.collider != null;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetInteger("State", 0);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ScoreTextScript.coinAmount += 1;
        Destroy(collision.gameObject);
        coin--;
        if (coin == 0)
        {
            SceneManager.LoadScene(2);
        }

    }

}

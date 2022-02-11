using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private bool isGrounded;
    private int extraJump = 0;

    private float jump = 10f;
    private float speed=2.5f;
    public Rigidbody2D rb2d_Platform;

    private void Awake()
    { 
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("fall_platform"))
        {
            isGrounded = true;
            rb2d_Platform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("fall_platform"))
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");

        PlayerMove(horizontal);
        
    }

    private void Update()
    {
        if (isGrounded == true)
        {
            extraJump = 1;
        }

        bool vertical = Input.GetKeyDown(KeyCode.UpArrow);
        if ((vertical) && (extraJump == 0) && (isGrounded == true))
        {
            PlayerJump(vertical);
        }
        else if((vertical) && (extraJump > 0))
        {
            PlayerDoubleJump(vertical);
        }
            
    }

    private void PlayerMove(float horizontal)
    {
        Vector2 currentPosition = transform.position;
        currentPosition.x += speed * horizontal * Time.deltaTime;
        transform.position = currentPosition;
    }

    private void PlayerJump(bool vertical)
    {
        rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
    }

    public void PlayerDoubleJump(bool vertical)
    {
        rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
        extraJump = extraJump - 1;
    }
}

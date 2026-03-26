using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float JumpForce = 5.0f;


    public Rigidbody rb;

    public bool isGrounded = true;


    public int coinCount = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            isGrounded = true;

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3 (moveHorizontal * MoveSpeed, rb.linearVelocity.y, moveVertical * MoveSpeed);

        if(Input.GetButtonDown("Jump") && isGrounded ) // && 두 값을 만족할 때
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse); //위로 설정한 구치만큼 힘 증가
            isGrounded = false;
        }

        


    }
}

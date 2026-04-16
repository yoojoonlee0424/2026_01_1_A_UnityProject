using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("기본 이동 설정")]
    public float MoveSpeed = 5.0f;
    public float JumpForce = 5.0f;
    public float turnSpeed = 10.0f;

    [Header("점프 설정")]
    public float fallMultiplier = 2.5f;         //하강 중력 배율
    public float lowJumpMultiplier = 2.0f;      //짧은 점프 배율

    [Header("지면 감지 설정")]
    public float coyoteTime = 0.2f;                    //코요테 타임 지속 시간
    public float coyoteTimeCounter;                    //코요테 타임 카운터
    public bool realGrounded = true;                   //실제 지면에 닿았는지 여부

    [Header("글라이더 설정")]
    public GameObject gliderObject; //글라이더 오브젝트
    public float gliderFallSpeed = 1.0f; //글라이더 낙하 속도
    public float gliderMoveSpeed = 7.0f; //글라이더 이동 속도
    public float gliderMaxTime = 5.0f; //글라이더 최대 지속 시간
    public float gliderTimeLeft; //글라이더 남은 시간
    public bool isGliding = false; //글라이더 활성화 여부

    public Rigidbody rb;

    public bool isGrounded = true; //점프 가능 여부


    public int coinCount = 0;   //코인 수집 변수


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        coyoteTimeCounter = 0.0f;


        if (gliderObject != null)
        {
            gliderObject.SetActive(false); //글라이더 오브젝트 비활성화
        }

        gliderTimeLeft = gliderMaxTime; //글라이더 남은 시간 초기화

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGrounded = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGrounded = true;
        }
    }



    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGrounded = false;
        }
    }



    void UpdateGrounededState()
    {
        if (realGrounded)           //지면에 있으면 코요테 타임 초기화
        {
            coyoteTimeCounter = coyoteTime; //코요테 타임 초기화
            isGrounded = true; 
        }
        else
        {
            //지면이 없어도 코요테 타임이 남아있으면 점프 가능
            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime; //코요테 지속적으로 타임 감소
                isGrounded = true; //코요테 타임 동안 점프 가능
            }
            else
            {
                isGrounded = false;
            }
        }
    }



    // Update is called once per frame
    void Update()


    {
        UpdateGrounededState();      //지면 감지

        //움직임 입력 받기
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //이동 방향 계산
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if(movement.magnitude > 0.1f)                                   // 이동 입력이 있을 때만 회전
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            //부드러운 회전 효과 적용
        }


        //G로 글라이더 활성화(누르는 동안)
        if (Input.GetKey(KeyCode.G) && !isGliding && gliderTimeLeft > 0)    //G키를 누르면서 땅에 있지 않고 글라이더 시간이 남아있을 때
        {
            if(!isGliding)
            {
                EnableGlider();
            }

            gliderTimeLeft -= Time.deltaTime;                                //글라이더 시간 감소

            if(gliderTimeLeft <= 0)                                         //사용 시간이 다 되면 글라이더 비활성화
            {
                DisableGlider();
            }

        }
        else if(isGliding)
        {
            DisableGlider();                                            //G키에서 손을 뗐을 때 글라이더 비활성화
        }


        if(isGliding)                                           //글라이더 이동 적용
        {
            ApplyGliderMovement(moveHorizontal, moveVertical);      //글라이더 이동 적용 함수 호출

            
        }
        else
        {
            // 강체 속도 값으로 캐릭터 이동 적용
            rb.linearVelocity = new Vector3(moveHorizontal * MoveSpeed, rb.linearVelocity.y, moveVertical * MoveSpeed);

            //착시 점프 높이 조절
            if (rb.linearVelocity.y < 0) //하강 중일 때
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;        //하강 중일 때 중력 가속도를 증가시켜 빠르게 떨어지도록 함
            }
            else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))                                           //점프 중이지만 점프 버튼을 잛게 눌렀을 때
            {
                rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

       

        //점프 입력 받기
        if (Input.GetButtonDown("Jump") && isGrounded )                          // && 두 값을 만족할 때
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);              //위로 설정한 구치만큼 힘 증가
            isGrounded = false;
            realGrounded = false;
            coyoteTimeCounter = 0.0f;                                           //점프 시 코요테 타임 초기화
        }



        //지면에 있으면 글라이더 시간 회복, 글라이더 비활성화
        if (isGrounded)
        {
            
            if (isGliding)
            {
                DisableGlider(); //글라이더 비활성화
            }

            gliderTimeLeft = gliderMaxTime; //글라이더 시간 회복
        }

    }

    //글라이더 활성
    void EnableGlider()
    {
        isGliding = true;

        if(gliderObject != null)
        {
            gliderObject.SetActive(true);                                       //글라이더 오브젝트 활성화
        }
        
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, -gliderFallSpeed, rb.linearVelocity.z); //글라이더 낙하 속도 적용    

    }

    //글라이더 비활성
    void DisableGlider()
    {
        isGliding = false;

        if (gliderObject != null)
        {
            gliderObject.SetActive(false);                                  //글라이더 오브젝트 비활성화
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); //즉시 낙하

    }



    //글라이더 이동 적용
    void ApplyGliderMovement(float horizontal, float vertical)              //수평 수직을 받아서 글라이더 이동 적용
    {
        //글라이더 : 천천히 떨어짐 수평으로 빠르게 이동가능
        Vector3 gliderVelocity = new Vector3(horizontal * gliderMoveSpeed, -gliderFallSpeed, vertical * gliderMoveSpeed);

        rb.linearVelocity = gliderVelocity; //글라이더 이동 속도 적용
    }




}

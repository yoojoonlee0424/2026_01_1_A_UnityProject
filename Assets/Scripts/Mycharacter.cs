using UnityEngine;

public class Mycharacter : MonoBehaviour
{
    public int Health = 100;
    public float Timer = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = Health + 100;                                                  // 시작시 체력추가
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer - Time.deltaTime;                                     //매 프레임 시간 감소

        if (Timer <= 0)                                                     //만약 Timer 의 수치가 0이하로 내려갈 경우
        {
            Timer = 1.0f;                                                   // 다시 1초로 변경
            Health = Health - 20;                                           //처력 감소
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Health = Health + Random.Range(-50,100);
        }

        if (Health <= 0)
        { 
            Destroy(this.gameObject);
        }
    }
}

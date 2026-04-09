using NUnit.Framework;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public CubeGenarator[] gen_ed_Cubes = new CubeGenarator[5]; // 클래스 배열

    public float timer = 0.0f;
    public float interval = 3.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            RandomCubeAcitiv();
            timer = 0.0f;
        }



    }


    public void RandomCubeAcitiv()
    {
        for (int i = 0; i < gen_ed_Cubes.Length; i++)   //생성 함수를 랜덤 호출
        {
            int randomNum = Random.Range(0, 2); //랜덤값은 1,0 (50%)

            if(randomNum == 1)                  // 랜덤 1이면
            {
                gen_ed_Cubes[i].GenCube();       //큐브 클래스 생성함수 호출
            }

        }
    }

}

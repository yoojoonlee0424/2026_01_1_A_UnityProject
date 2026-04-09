using UnityEngine;

public class CubeGenarator : MonoBehaviour
{

    public GameObject cubePrefab;
    public int totalCubes = 10;             //총 큐브 수
    public float cubeSpacing = 1.0f;        //큐브 공간

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenCube();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenCube()
    {
        Vector3 myPosition = transform.position;        //이 컴포넌트가 있는 오브젝트 위치

        for(int i = 0; i < totalCubes; i++)
        {
            /*
            for (int j = 0; j < totalCubes; j++)
            {
                Vector3 xposition = new Vector3(myPosition.x, myPosition.y, myPosition.z + (i * cubeSpacing));
                Instantiate(cubePrefab, xposition, Quaternion.identity);     //생성
            }
            */

            Vector3 position = new Vector3(myPosition.x, myPosition.y, myPosition.z + (i * cubeSpacing));
            Instantiate(cubePrefab, position, Quaternion.identity);     //생성
        }

    }


}

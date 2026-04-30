using UnityEngine;

public class FruitGame : MonoBehaviour
{
    public GameObject[] fruitPerfabs;                   //과일 배열
    public float[] fruitSize = { 0.5f, 0.7f, 0.9f, 1.1f, 1.3f, 1.5f, 1.7f, 1.9f };

    public GameObject currentFruit;
    public int currentFruitType;

    public float fruitStartHeight = 6.0f;
    public float gameWidth = 6.0f;
    public bool isGameOver = false;
    public Camera mainCamera;

    public float fruitTimer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        SpawnnewFruit();
        fruitTimer = -3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            return;
        }


        if(fruitTimer >= 0)
        {
            fruitTimer -= Time.deltaTime;
        }

        if(fruitTimer < 0 && fruitTimer > -2)
        {
            SpawnnewFruit();
            fruitTimer = -3.0f;


        }



        if(currentFruit != null)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            Vector3 newPos = currentFruit.transform.position;

            newPos.x = worldPos.x;

            float halfFruitSize = fruitSize[currentFruitType] / 2f;

            if (newPos.x < -gameWidth / 2 - halfFruitSize)
            {
                newPos.x = -gameWidth / 2 - halfFruitSize;
            }

            if (newPos.x > gameWidth / 2 + halfFruitSize)
            {
                newPos.x = gameWidth / 2 + halfFruitSize;
            }


            currentFruit.transform.position = newPos;
        }


        if(Input.GetMouseButtonDown(0) && fruitTimer == -3.0f)
        {
            DropFruit();
        }

    }


    //과일 생성 함수
    private void SpawnnewFruit()
    {
        if (!isGameOver)
        {

            currentFruitType = Random.Range(0, 3);

            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            Vector3 spawnPos = new Vector3(worldPos.x, fruitStartHeight, 0);

            float halfFruitSize = fruitSize[currentFruitType] / 2f;


            spawnPos.x = Mathf.Clamp(spawnPos.x, -gameWidth / 2 + halfFruitSize, gameWidth / 2 - halfFruitSize);

            currentFruit = Instantiate(fruitPerfabs[currentFruitType], spawnPos, Quaternion.identity);
            currentFruit.transform.localScale = new Vector3(fruitSize[currentFruitType], fruitSize[currentFruitType], 1);

            Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                rb.gravityScale = 0.0f;
            }
        }
    }

    private void DropFruit()
    {
        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        if(rb != null )
        {
            rb.gravityScale = 1.0f;
            currentFruit = null;
            fruitTimer = 1.0f;
        }
    }



    public void MergeFruits(int fruitType, Vector3 position)
    {
        if(fruitType < fruitPerfabs.Length -1)
        {
            GameObject newFruite = Instantiate(fruitPerfabs[fruitType + 1], position, Quaternion.identity);
            newFruite.transform.localScale = new Vector3(fruitSize[fruitType + 1], fruitSize[fruitType + 1], 1.0f);

            // 점수 추가 로직은 여기에
        }
    }

}

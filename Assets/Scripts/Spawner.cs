using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject coinPrefabs;
    public GameObject MissilePrefabs;

    [Header("스폰 타이밍 설정")]
    public float minSpawn = 0.5f;
    public float maxSpawn = 2.0f;

    [Header("동전 스폰 확률")]
    [Range(0, 100)]
    public int coinSpwanChanace = 50;

    public float timer = 0.0f;
    public float nextSpawntime = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetNextSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > nextSpawntime)
        {
            SpawnObject();
            timer = 0.0f;
            SetNextSpawn();

        }

    }


    void SpawnObject()
    {
        Transform spawnTransform = transform;

        //미사일 확률
        int randomValue = Random.Range(0, 100);

        if(randomValue < coinSpwanChanace)
        {
            Instantiate(coinPrefabs, spawnTransform.position, spawnTransform.rotation);
        }
        else
        {
            Instantiate(MissilePrefabs, spawnTransform.position, spawnTransform.rotation);
        }
            
    }


    void SetNextSpawn()
    {
        nextSpawntime = Random.Range(minSpawn, maxSpawn);
    }



}

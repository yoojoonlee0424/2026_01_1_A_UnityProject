using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int fruitType;               // index 설정
    public bool hasMerged = false;      //머지 플래그

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged) // 이미 합쳐지면 무시
        {
            return;
        }
        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();

        if(otherFruit != null && !otherFruit.hasMerged && otherFruit.fruitType == fruitType) //과일이고 같은 타입이면
        {
            hasMerged = true;
            otherFruit.hasMerged = true;

            Vector3 mergePosition = (transform.position + otherFruit.transform.position) / 2f; 

            //머지 호출
            FruitGame gameManager = FindAnyObjectByType<FruitGame>();

            if (gameManager != null)
            {
                gameManager.MergeFruits(fruitType,mergePosition);
            }



            //과일 제거
            Destroy(otherFruit.gameObject);
            Destroy(gameObject);

        }

    }




}

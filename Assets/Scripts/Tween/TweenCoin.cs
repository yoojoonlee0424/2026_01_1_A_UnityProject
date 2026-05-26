using UnityEngine;
using DG.Tweening;

public class TweenCoin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinTween();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void coinTween()
    {
        // 코인 생성시 랜덤한 위치로 튀도록 목표 위치 잡기
        Vector3 randomPosition = transform.position
            + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        //코인이 바닥에 떨어지는 것처럼 점프로 이동
        //DOJump(목표 위치, 점프높이, 점프 횟수, 시간)
        transform.DOJump(randomPosition, 1.2f, 1, 0.4f).SetLink(gameObject);

        transform.DORotate(new Vector3(0f, 360f, 0f), 0.4f, RotateMode.FastBeyond360).SetLink(gameObject);
    }
}

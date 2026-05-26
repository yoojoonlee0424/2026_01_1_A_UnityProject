using UnityEngine;
using DG.Tweening;
using TMPro;



public class TweenSample : MonoBehaviour
{
    [Header("효과를 위한 Ui/Object 타겟")]
    public RectTransform UITarget;
    public GameObject ObjectTarget;

    [Header("글자 연출 타겟")]
    public TMP_Text countText;
    public int currentValue = 0;
    public int addValue = 100;

    private int TargetValue;

    [Header("색 변형 연출 타겟")]
    public Color FlashColor = Color.yellow;

    private Color originalColor;

    [Header("페이드 UI 그룹")]
    public CanvasGroup fadeTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countText.text = currentValue.ToString();

        originalColor = countText.color;

        fadeTarget.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayPunchUIScale();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayPunchObjScale();
        }

        if( Input.GetKeyDown(KeyCode.Q))
        {
            PlayUIShake();
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            PlayCountUp();
            PlayColorFlash();
        }

        if(Input.GetKey(KeyCode.E))
        {
            PlayFade();
            
        }

    }

    public void PlayPunchUIScale()
    {
        if (UITarget == null)
        {
            return;
        }

        UITarget.DOKill();                  //이전에 실행 중 이던 Tween 효과 삭제
        UITarget.localScale = Vector3.one;  //크기도 초기화
        UITarget.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f); // 방향, 크기, 시간, 진동횟수, 탄성

    }

    public void PlayPunchObjScale()
    {
        if (ObjectTarget == null)
        {
            return;
        }

        ObjectTarget.transform.DOKill();                  //이전에 실행 중 이던 Tween 효과 삭제
        ObjectTarget.transform.localScale = Vector3.one;  //크기도 초기화
        ObjectTarget.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f); // 방향, 크기, 시간, 진동횟수, 탄성

    }

    public void PlayUIShake()
    {
        if (UITarget == null)
        {
            return;
        }

        UITarget.DOKill();
        UITarget.DOShakeAnchorPos(0.3f, 20f, 20, 90f);  //시간, 강도, 진동 횟수, 랜덤성


    }

    public void PlayCountUp()
    {
        if(countText == null)
        {
            return;
        }

        TargetValue += addValue;
        DOTween.Kill("countTween", true);

        DOTween.To(
            () => currentValue,
            value =>
            {
                currentValue = value;
                countText.text = currentValue.ToString();
            },
            TargetValue,
            0.5f


            )
            .SetEase(Ease.OutQuad)
            .SetId("CountTween");

    }

    public void PlayColorFlash()
    {
        if(countText == null)
        {
            return;
        }

        countText.DOKill();
        countText.color = originalColor;

        countText.DOColor(FlashColor, 0.1f)
            .OnComplete(() =>
            {
                countText.DOColor(originalColor, 0.2f);  //완료 되면 원래 색으로
            });

    }

    public void PlayFade()
    {
        if(fadeTarget == null)
        {
            return;
        }
        fadeTarget.DOKill();
        fadeTarget.alpha = 0;

        Sequence seq = DOTween.Sequence();  //여러 Tween 순서대로 사용시 시퀀스 사용

        seq.Append(fadeTarget.DOFade(1f, 0.2f));
        seq.AppendInterval(0.5f);
        seq.Append(fadeTarget.DOFade(0f, 0.3f));

    }

}

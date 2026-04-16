using UnityEngine;
using TMPro;
using UnityEditor.UI;

public class GliderUi : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public float Timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        TimerText.text = "글라이더 시간 : " + Timer.ToString("0.00");
    }
}

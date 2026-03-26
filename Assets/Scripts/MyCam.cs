using UnityEngine;

public class MyCam : MonoBehaviour
{

    public Transform ball;
    public float CamDis = -10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetpos = new Vector3(ball.transform.position.x, ball.transform.position.y + 2, ball.transform.position.z + CamDis);
        transform.position = Vector3.Lerp(transform.position, targetpos, Time.deltaTime);
    }
}

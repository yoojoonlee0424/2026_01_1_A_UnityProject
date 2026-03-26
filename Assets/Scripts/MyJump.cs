using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class MyJump : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float power = 200f;
    public Text timeUI;
    public float Timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {

        Timer = Timer + Time.deltaTime;
        timeUI.text = Timer.ToString();




        if (Input.GetKeyDown(KeyCode.Space))
        {
            power = power * Random.Range(1,2);
            rigidbody.AddForce(transform.up * power);
        }

        if (this.gameObject.transform.position.y > 50 || this.gameObject.transform.position.y < -30)
        {
            Destroy(this.gameObject);
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class MyBall : MonoBehaviour
{
    public new Rigidbody rigidbody;

    public object obj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.name + "와 충돌함"); 

        if (collision.gameObject.tag == "Ground")
        {

            Debug.Log("땅과 충돌");

        }
    }


    //OnTriggerStay

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리어 안으로 들어옴");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("트리어 밖으로 나감");
    }


}

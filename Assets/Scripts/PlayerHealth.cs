using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int Maxhealth= 3;
    public int CurrentHealth;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = Maxhealth;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Misslie"))
        {
            CurrentHealth--;
            Destroy(other.gameObject);

            if(CurrentHealth <= 0)
            {
                Gameover();
            }
        }
    }



    void Gameover()
    {
        gameObject.SetActive(false);
        Invoke("Restart", 3.0f);
    }


    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

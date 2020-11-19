using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_health : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float maxhealth = 100f;

    public Slider slider;

    void Start()
    {
        health = maxhealth;
        slider.value = CalculateHealth(); 
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth(); 
        if(health <= 0f){
            //dead
            SceneManager.LoadScene(3);
        }
    }

    void OnCollisionStay(Collision collision) {
       
       if(collision.collider.tag == "Fire"){
           health -= 1;
       }
       Debug.Log("Tag is " + collision.collider.name);
    }

    void OnCollisionEnter(Collision collision) {
       
       if(collision.collider.tag == "Dummie"){
           SceneManager.LoadScene(3);
       }
       if(collision.collider.tag == "GameOver"){
           SceneManager.LoadScene(4);
       }
       Debug.Log("Tag is " + collision.collider.name);
    }

    float CalculateHealth(){
        return health / maxhealth;
    }
}

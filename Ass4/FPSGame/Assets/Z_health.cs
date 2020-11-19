using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Z_health : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float maxhealth;

    public Slider slider;
    public GameObject healthBar;
    private Animator anim;
    
    void Start()
    {
        health = maxhealth;
        slider.value = CalculateHealth(); 
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth(); 

        if(health < maxhealth){
            healthBar.SetActive(true);
        }

        if(health <= 0f){
            //dead
            anim.SetBool("isDead", true);
        }
    }

    float CalculateHealth(){

        return health / maxhealth;
    }

    void OnCollisionEnter(Collision collision) {
       
       if(collision.collider.tag == "Bullet"){
           health -= 10;
       }
       if(collision.collider.tag == "Healing"){
           health = maxhealth;
       }
       Debug.Log("Tag is " + collision.collider.name);

    }
}

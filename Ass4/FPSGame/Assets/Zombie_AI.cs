using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Zombie_AI : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent Mob;
    private Animator anim;

    public GameObject Player;

    public float DistanceRun = 4.0f;

    public float health;
    public float maxhealth;

    public Slider slider;
    public GameObject healthBar;

    public enum Mode{
        Wonder,
        Chase,
        Dead
    }

    Mode currentMode = Mode.Wonder;
    
    void Start()
    {
        Mob = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        health = maxhealth;
        slider.value = CalculateHealth(); 
        healthBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(currentMode == Mode.Dead){
            return;
        }

        float distance = Vector3.Distance(transform.position, Player.transform.position);
        slider.value = CalculateHealth(); 
        if(health < maxhealth){
            healthBar.SetActive(true);
        }

        if(health <= 0f){
            //dead
            anim.SetBool("isDead", true);
            currentMode = Mode.Dead;
        }

        if(distance < DistanceRun && currentMode != Mode.Dead){
            currentMode = Mode.Chase;
            anim.SetBool("isChasing", true);
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;

            Mob.SetDestination(newPos);
        }
        else if(currentMode == Mode.Wonder){
            Mob.SetDestination(RandomNavmeshLocation(10f));
        }
        else{
            Mob.SetDestination(transform.position);
        }
    }

    public Vector3 RandomNavmeshLocation(float radius) {
         Vector3 randomDirection = Random.insideUnitSphere * radius;
         randomDirection += transform.position;
         NavMeshHit hit;
         Vector3 finalPosition = Vector3.zero;
         if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
             finalPosition = hit.position;            
         }
         return finalPosition;
     }
    
    float CalculateHealth(){

        return health / maxhealth;
    }

    void OnCollisionEnter(Collision collision) {
       
       if(collision.collider.tag == "Bullet"){
           health -= 11;
       }
       Debug.Log("Tag is " + collision.collider.name);

    }
}

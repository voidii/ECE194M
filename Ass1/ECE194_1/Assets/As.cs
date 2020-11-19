using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class As : MonoBehaviour
{
    public float maxThrust;
    public float maxTorque;
    public int as_size;
    public Rigidbody2D rb;
    public GameObject smaller_as;
    public int point;
    public GameObject player;
    public GameObject sound;


    
    // Start is called before the first frame update
    void Start()
    {
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);

        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = transform.position;

        if(transform.position.y > 39)
        {
            newPos.y = -39;
        }

        if(transform.position.y < -39)
        {
            newPos.y = 39;
        }

        if(transform.position.x > 52)
        {
            newPos.x = -52;
        }

        if(transform.position.x < -52)
        {
            newPos.x = 52;
        }

        transform.position = newPos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            if(as_size == 2)
            {
                Instantiate(smaller_as, transform.position, transform.rotation);
                Instantiate(smaller_as, transform.position, transform.rotation);
            }

            player.SendMessage("ScorePoints", point);

            GameObject s = Instantiate(sound, transform.position, transform.rotation);
            Destroy(gameObject);

            Debug.Log("Sound");
            Destroy(s, 2f);
        }
    }

}

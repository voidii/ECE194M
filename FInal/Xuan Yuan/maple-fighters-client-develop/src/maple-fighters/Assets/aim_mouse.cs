using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim_mouse : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 direction;
    GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            
            player = GameObject.FindWithTag("Player");
            transform.position = player.transform.position;
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle =360-Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        
    }
}

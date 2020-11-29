using UnityEngine;
using System.Collections;

public class Impulse : MonoBehaviour {
    public float force = 1f;
    public GameObject smokePrefab;
    GameObject smoke;
    [HideInInspector]
    public bool hit = false;
    Transform front;
    Rigidbody r;

    void Start()
    {
        smoke = Instantiate(smokePrefab);
        smoke.SetActive(false);
        front = transform.Find("front");
    }

    void OnTriggerEnter(Collider collider)
    {
        r = collider.gameObject.GetComponent<Rigidbody>();
        if (r != null)
        {
            r.AddForce(transform.forward * force, ForceMode.Impulse);
            hit = true;
            smoke.transform.position = front.position;
            smoke.transform.rotation = Quaternion.LookRotation(-transform.forward);
            smoke.SetActive(true);
            Destroy(smoke, 3);
            smoke = Instantiate(smokePrefab);
            smoke.SetActive(false);
        }
    }

    void OnDestroy()
    {
        Destroy(smoke);
    }

}

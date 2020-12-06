using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    SpriteRenderer m_SpriteRenderer;
    bool in_animation = false;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //m_SpriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !in_animation)
        {
            //m_SpriteRenderer.enabled = true;
            //Vector2 translation = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            //Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //float angle =360-Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            //transform.eulerAngles = new Vector3(0, 0, angle);
            StartCoroutine(PlayAndDisappear());
            //animator.SetTrigger("Hook");
        }


    }

    IEnumerator PlayAndDisappear(){
        in_animation = true;
        animator.SetTrigger("Hook");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        //m_SpriteRenderer.enabled = false;
        in_animation = false;
    }
}

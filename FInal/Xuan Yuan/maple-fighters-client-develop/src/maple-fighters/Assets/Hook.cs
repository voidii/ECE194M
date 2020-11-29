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
    }

    // Update is called once per frame
    void Update()
    {
        if(in_animation){
            animator.ResetTrigger("Hook");
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("hook")){
            m_SpriteRenderer.enabled = true;
            in_animation = true;
        }
        else{
            m_SpriteRenderer.enabled = false;
            in_animation = false;
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            
            in_animation = true;
            animator.SetTrigger("Hook");
        }

    }
}

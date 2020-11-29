using System.Collections;


using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

namespace SkeletonEditor
{

    public class PlayerController : MonoBehaviour
    {
        public float mouseRotateSpeed = 0.3f;

        private Animator animator;
        private Quaternion initRotation;
        public Transform Camera;

        public float max_speed = 15f;
        float Movespeed = 0;


        private int currentAnimation;
        public List<string> animations;
       

        private bool startMouseRotate;
        private Vector3 prevMousePosition;
        Rigidbody m_Rigidbody;
        public CharacterController controller;
        public Transform cam;
        public GameObject MainCam;
        public GameObject AimCam;

        public float speed = 6;
        public float gravity = -9.81f;
        public float jumpHeight = 3;
        Vector3 velocity;
        bool isGrounded;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        float turnSmoothVelocity;
        public float turnSmoothTime = 0.1f;

        public static PlayerController Instance { get; private set; }

        void Awake() {
            if (Instance != null) {
                Destroy(this.gameObject);
            }
            Instance = this;
        }

        void Start() {
            animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            initRotation = transform.rotation;

          
            animations = new List<string>()
            {
                "Attack",
                "Active",
                "Passive"   
            };
        }

        void Update() {

            if (Input.GetMouseButtonDown(1) && MainCam.activeInHierarchy) {
                MainCam.SetActive(false);
                AimCam.SetActive(true);
            }
            else if(Input.GetMouseButtonUp(1) && !MainCam.activeInHierarchy)
            {
                MainCam.SetActive(true);
                AimCam.SetActive(false);
            }
            
            
            //transform.Rotate(new Vector3(0, (Input.mousePosition.x - prevMousePosition.x) * mouseRotateSpeed, 0));
            //prevMousePosition = Input.mousePosition;
            if (Input.GetMouseButtonDown(0)) {
                startMouseRotate = true;
                animator.SetTrigger("Attack");
                startMouseRotate = false;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                startMouseRotate = true;
                animator.SetTrigger("Attack");
                startMouseRotate = false;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                startMouseRotate = true;
                animator.SetTrigger("Active");
                startMouseRotate = false;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                startMouseRotate = true;
                animator.SetTrigger("Passive");
                startMouseRotate = false;
            }
            

            //float h = Input.GetAxis("Analog X");
            //float v = Input.GetAxis("Analog Y");
            

            //Movespeed += Time.deltaTime * 10;    
            //if(Movespeed > max_speed){
            //    Movespeed = max_speed;
            //}
            //if (!startMouseRotate) {
            //    if (Input.GetKey(KeyCode.A)) {
            //        //transform.Rotate(Vector3.down, 45f * Time.deltaTime * 10f);
            //        h = 1f;
            //        transform.Translate(Vector3.forward * Time.deltaTime * Movespeed, Space.Self);
            //    }
            //    if (Input.GetKey(KeyCode.D)) {
            //        //transform.Rotate(new Vector3(0, 10, 0));
            //        h = 1f;
            //        transform.Translate(Vector3.forward * Time.deltaTime * Movespeed, Space.Self);
            //    }
            //    if (Input.GetKey(KeyCode.W)) {
            //        h = 1f;
            //        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(Camera.transform.forward.x, 0, Camera.transform.forward.z),Vector3.up), 10f);
            //        //Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
            //        //targetDirection = Camera.main.transform.TransformDirection(targetDirection);
            //        //targetDirection.y = 0.0f;
            //        transform.Translate(Vector3.forward * Time.deltaTime * Movespeed, Space.Self);
            //    }
            //    if (Input.GetKey(KeyCode.S)) {
//
            //        //transform.Rotate(Vector3.up, 45f * Time.deltaTime * 10f);
            //        h = 1f;
            //        transform.Translate(Vector3.forward * Time.deltaTime * Movespeed, Space.Self);
            //    }
            //    if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            //        && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D)
            //        ) {
            //        Movespeed = 0;
            //    }
            //}

            //var speed = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
            //animator.SetFloat("speedv", speed);

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
            //gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            //walk
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            animator.SetFloat("speedv", Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical)));
            }
    }
}
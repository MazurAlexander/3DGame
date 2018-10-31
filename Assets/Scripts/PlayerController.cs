using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private MouseController m_MouseLook;

    public float speed;
    public float jummpValue;
    public float gravity;
    public static PlayerController I;
    private Vector3 moveDirection;
    private bool stopAnimtion = true;
    public bool vase = false;
    public float timer = 5f;

    private Camera m_Camera;
    private CharacterController pl_controller;
    public Animator pl_animator;
    private AnimationEvent pl_event;


    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        m_Camera = Camera.main;
        m_MouseLook.Init(transform, m_Camera.transform);
    }

    // Use this for initialization
    void Start()
    {
        pl_animator = GetComponent<Animator>();
        pl_controller = GetComponent<CharacterController>();
    }
    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }
    // Update is called once per frame
    void Update()
    {
        RotateView();
        if (stopAnimtion)
        {
            PlayerMove();
            GamingGravity();
            PlayerAnimtaion();
        }
        Hit();

    }

    private void PlayerMove()//передвежение персоонаджа
    {
        if (pl_controller.isGrounded)
        {


            pl_animator.ResetTrigger("Jump");
            pl_animator.SetBool("Falling", false);

            //moveDirection = Vector3.zero;
            //moveDirection.x = Input.GetAxis("Horizontal") * speed;
            //moveDirection.z = Input.GetAxis("Vertical") * speed;


            //PlayerAnimtaion();

            //if (Vector3.Angle(Vector3.forward, moveDirection) > 1f || Vector3.Angle(Vector3.forward, moveDirection) == 0)//вращение за парсоонажем
            //{
            //    Vector3 direct = Vector3.RotateTowards(transform.forward, moveDirection, speed, 0.0f);
            //    transform.rotation = Quaternion.LookRotation(direct);
            //}

        }
        else
        {
            if (gravity < -3f)
            {
                pl_animator.SetBool("Falling", true);
            }
        }
        moveDirection.y = gravity;
        pl_controller.Move(moveDirection * Time.deltaTime);
        m_MouseLook.UpdateCursorLock();

    }

    private void PlayerAnimtaion()
    {

        if (timer > 0)
        {
            if (Input.GetKey(KeyCode.W))//
            {
                timer -= Time.deltaTime;
                pl_animator.SetBool("Walk", true);
                SpeedControll();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                pl_animator.SetBool("WalkLeft", true);
                SpeedControll();
                timer -= Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                SpeedControll();
                timer -= Time.deltaTime;
                pl_animator.SetBool("WalkRight", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                SpeedControll();
                timer -= Time.deltaTime;
                pl_animator.SetBool("WalkBackward", true);
            }
            else
            {
                speed = 1;
                timer = 5;
                pl_animator.SetBool("WalkBackward", false);
                pl_animator.SetBool("Walk", false);
                pl_animator.SetBool("WalkRight", false);
                pl_animator.SetBool("WalkLeft", false);
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                pl_animator.SetBool("Run", true);
                SpeedControll();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                pl_animator.SetBool("RunLeft", true);
                SpeedControll();
            }
            else if (Input.GetKey(KeyCode.D))
            {

                pl_animator.SetBool("RunRight", true);
                SpeedControll();
            }
            else if (Input.GetKey(KeyCode.S))
            {

                pl_animator.SetBool("RunBackward", true);
                SpeedControll();

            }
            else
            {


                pl_animator.SetBool("Run", false);
                pl_animator.SetBool("RunRight", false);
                pl_animator.SetBool("RunLeft", false);
                pl_animator.SetBool("RunBackward", false);
                timer = 5;
                speed = 1;

            }
        }

    }

    private void SpeedControll()
    {
        if (speed < 3)
        {
            speed += 0.01f;
        }
        else
        {
            speed = 3f;
        }
    }

    private void GamingGravity()//Гравиатиця + анимация прыжка
    {
        if (!pl_controller.isGrounded) gravity -= 20f * Time.deltaTime;
        else gravity = -1f;
        if (Input.GetKeyDown(KeyCode.Space) && pl_controller.isGrounded)
        {
            gravity = jummpValue;
            pl_animator.SetTrigger("Jump");
        }
    }

    private void СhoiceHit()//выбор удара(L,R)
    {
        System.Random rnd = new System.Random();
        int a = rnd.Next(0, 2);
        pl_animator.SetInteger("LightHit", a);
        stopAnimtion = false;
    }
    private void AllAnimationStop()
    {
        pl_animator.SetBool("WalkBackward", false);
        pl_animator.SetBool("Walk", false);
        pl_animator.SetBool("WalkRight", false);
        pl_animator.SetBool("WalkLeft", false);
        pl_animator.SetBool("Run", false);
        pl_animator.SetBool("RunRight", false);
        pl_animator.SetBool("RunLeft", false);
        pl_animator.SetBool("RunBackward", false);
    }
    private void Hit()
    {
        if (Input.GetMouseButtonDown(0))//атакана налкм
        {
            AllAnimationStop();
            СhoiceHit();
            pl_animator.SetBool("Attack", true);

        }
        if (Input.GetMouseButtonDown(1))//аткак на пкм
        {
            pl_animator.SetBool("Attack", false);
            pl_animator.SetBool("StrongAttack", true);
        }
        if (pl_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Strong"))//если прешел в анимацию
        {
            pl_animator.SetInteger("LightHit", 5);
            pl_animator.SetBool("StrongAttack", false);
            stopAnimtion = true;
        }
        if (pl_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_R") || pl_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_L"))
        {
            StartCoroutine(StopMoving());
        }
    }
    IEnumerator StopMoving()//карутина для полной остановки перса(+ время для полнйо анимации без возможности двигатся)
    {

        pl_animator.SetInteger("LightHit", 5);
        yield return new WaitForSeconds(2f);
        stopAnimtion = true;
    }
    private void AttackVase()
    {
        if (vase)
        {
            VaseController.I.CloudOrFire();
            vase = false;
        }
    }

}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float moveY;
    public float jumpTime;

    public Transform interactionPoint;
    public float interactionRange = 1f;

    [SerializeField] private float weight = 2f;

    [SerializeField]
    private float _moveSpeed = 4f;
    public float moveSpeed { get { return _moveSpeed; } }
    [SerializeField]
    private float _walkRate = 0.2f;
    public float walkRate { get { return _walkRate; } }
    [SerializeField]
    private float _jumpSpeed = 5f;
    public float jumpSpeed { get { return _jumpSpeed; } }

    [SerializeField]
    private Camera _cam;
    public Camera cam { get { return _cam; } }
    [SerializeField]
    private GroundChecker groundChecker;

    [SerializeField] private float _damage;
    public float damage { get { return _damage; } }

    [SerializeField]
    private GameObject _sword;
    public GameObject sword { get { return _sword; } }
    [SerializeField]
    private GameObject _swordSheath;
    public GameObject SwordSheath { get { return _swordSheath; } }
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    //public Wolf monster;

    private Animator animator;
    public Transform enemy;

    private Vector3 startPos, endPos;
    protected float timer;
    protected float timeToFloor;

    Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        var mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, Cal(t, height) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    float Cal(float t, float height)
    {
        return -4 * height * t * t + 4 * height * t;
    }

    protected IEnumerator Move()
    {
        timer = 0;
        while (transform.position.y >= startPos.y)
        {
            onSkill = true;
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 2, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }
        onSkill = false;
    }
    Rigidbody rgb;

    bool onSkill = false;







    public GameObject shield { get { return _shield; } }
    public Transform dropPoint;
    private void Awake()
    {
        // Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        animator = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody>();


    }

    private void Update()
    {
        if(onSkill == false)   Gravity();
        //  if (Input.GetKeyDown(KeyCode.K))
        //  {
        //      leap.Jump();
        //  }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Charge();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //controller.enabled = false;
            Jumping2();
           // controller.enabled = true;
        }
    }

    void Charge()
    {
        animator.SetTrigger("Charge");
        float dis = Vector3.Distance(transform.position, enemy.position);
        transform.position = Vector3.Lerp(transform.position, transform.position, dis * Time.deltaTime);
       /* if (dis >= 1)
        {
            dis = Vector3.Distance(transform.position, enemy.position);
            transform.position = Vector3.MoveTowards(transform.position, enemy.position, 2f * Time.deltaTime);
        }*/
    }
    void Jumping2()
    {
        animator.SetTrigger("HeroicLeap");
        startPos = transform.position;
        endPos = enemy.position;
        StartCoroutine("Move");
    }

    void Jumping()
    {
        animator.SetTrigger("HeroicLeap");
        Vector3 hpos = transform.position + ((enemy.position - transform.position) / 2);

        /*        Vector3[] Jumppath = { new Vector3(transform.position.x, transform.position.y, transform.position.z),
                                           new Vector3(hpos.x, hpos.y+3, hpos.z),
                                           new Vector3(enemy.position.x, enemy.position.y, enemy.position.z)};*/

        Vector3[] Jumppath = { transform.position,
                                   new Vector3(hpos.x, hpos.y+1, hpos.z),
                                   new Vector3(enemy.position.x-1, enemy.position.y, enemy.position.z-1)};

        controller.transform.DOPath(Jumppath, 3f, PathType.CatmullRom, PathMode.Full3D).SetLookAt(enemy.position);
    }


    private void Gravity()
    {
        if (jumpTime > 0f)
        {
            moveY = jumpSpeed;
            jumpTime -= Time.deltaTime;
        }
        else if (groundChecker.IsGrounded)
        {
            moveY = 0;
        }
        else
        {
            moveY += Physics.gravity.y * weight * Time.deltaTime;
        }

        controller.Move(Vector3.up * moveY * Time.deltaTime);
        animator.SetFloat("JumpHeight", controller.transform.position.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }

    private void OnDrawGizmosSelected()
    {

    }
}

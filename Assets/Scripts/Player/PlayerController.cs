using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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

    private NavMeshAgent agent;
    private Animator animator;
    public Transform enemy;

    [SerializeField] private LayerMask hitLayerMask;
    [SerializeField] private GameObject leftWeapon;
    [SerializeField] private GameObject rightWeapon;
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
            transform.rotation = Quaternion.LookRotation(enemy.position - transform.position);
            onSkill = true;
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 2, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }
        onSkill = false;
    }

    protected IEnumerator ChargeRush()
    {
        transform.Translate(Vector3.forward * 2f);
        yield return null;

    }



    bool onSkill = false;



    public GameObject shield { get { return _shield; } }
    public Transform dropPoint;
    private void Awake()
    {
        // Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        Gravity();

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("MouseMove");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.LookAt(enemy.position);
            animator.SetTrigger("Charge");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //controller.enabled = false;
            //transform.LookAt(new Vector3(enemy.position.x, enemy.position.y, enemy.position.z));
            transform.LookAt(enemy.position);
            Jumping2();
            // controller.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetBool("BladeStorm", !animator.GetBool("BladeStorm"));
        }

        if (Input.GetKeyDown(KeyCode.F1))
            leftWeapon.SetActive(!leftWeapon.activeSelf);

        if (Input.GetKeyDown(KeyCode.F2))
            leftWeapon.SetActive(!rightWeapon.activeSelf);

    }

    void Charge()
    {
        animator.SetTrigger("Charge");
       
        Vector3 dir = enemy.position - transform.position;
        if (dir.magnitude >= 0.001f)
        {
            float moveDist = Mathf.Clamp(5f * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
        }
        animator.SetTrigger("ChargeAttack");
    }
    void Jumping2()
    {
        animator.SetTrigger("HeroicLeap");
        startPos = transform.position;
        endPos = enemy.position;
        StartCoroutine("Move");
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

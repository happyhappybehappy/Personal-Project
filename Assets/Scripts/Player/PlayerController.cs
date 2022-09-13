using System;
using System.Collections;
using System.Collections.Generic;
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



    public GameObject shield { get { return _shield; } }
    public Transform dropPoint;
    private void Awake()
    {
        // Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        groundChecker = GetComponent<GroundChecker>();
    }

    private void Update()
    {
        Gravity();
        if (Input.GetKeyDown(KeyCode.K))
        {
            //   monster.TakeDamage(2f);
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.TestTools;

public class PlayerNormalMoveBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private LayerMask layerMask;

    public Animator animator;
    private Transform transform;
    private CharacterController characterController = null;
    private PlayerController playerController = null;
    private Camera cam = null;
    //private IInteractable tempTarget;
    private float curRate;

    public float mouseMoveSpeed = 4f;
    public Vector3 movePoint;
    public Camera mainCamera;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;
        this.transform = animator.transform;
        mainCamera = Camera.main;

        if (characterController == null)
        {
            characterController = animator.GetComponent<CharacterController>();
        }
        if (playerController == null)
        {
            playerController = animator.GetComponent<PlayerController>();
            cam = playerController.cam;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Move();
        Jump();
        ChangeMode();
       // Interaction();
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("123");
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                movePoint = raycastHit.point;
            }
        }
        MouseMove();
    }

    private void MouseMove()
    {

        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {
            Vector3 thisUpdatePoint = (movePoint - transform.position).normalized * mouseMoveSpeed;
            characterController.SimpleMove(thisUpdatePoint);
        }
    }

    private void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (moveInput.sqrMagnitude > 1f)
        {
            moveInput = moveInput.normalized;
        }

        if (Input.GetButton("Walk"))
        {
            curRate -= Time.deltaTime;
            if (curRate < playerController.walkRate)
                curRate = playerController.walkRate;
        }
        else
        {
            curRate += Time.deltaTime;
            if (curRate > 1f)
                curRate = 1f;
        }
        moveInput *= curRate;

        Vector3 forwardVec = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;

        Vector3 moveVec = moveInput.x * rightVec + moveInput.z * forwardVec;

        // 이동 방향 바라보기
        if (moveVec.magnitude > 0f)
            transform.forward = moveVec;
        animator.SetFloat("MoveSpeed", moveInput.magnitude);

        characterController.Move(moveVec * playerController.moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }
    }

    private void ChangeMode()
    {
        if (Input.GetButtonDown("Battle"))
        {
            animator.SetBool("Battle", true);
        }
    }
    /*
    private void Interaction()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);
        Collider[] colliders = Physics.OverlapSphere(playerController.interactionPoint.position, playerController.interactionRange, layerMask);
        IInteractable target = colliders[0].GetComponent<IInteractable>();

        if (target != null) tempTarget = target;

        if (!Input.GetButtonDown("Interaction"))
        {
            if (target == null)
            {
                tempTarget.OnUnFocused();
                tempTarget = null;
            }
            else
            {
                tempTarget.OnFocused(animator.gameObject);
            }

            return;
        }

        // 키를 눌렀을 때


        if (Input.GetButtonDown("Interaction") && colliders.Length > 0)
        {
            //  IInteractable target = colliders[0].GetComponent<IInteractable>();
            tempTarget?.Interaction();
        }

    }

    public void DrinkPotion()
    {
        animator.SetTrigger("Drink");
    }*/



}

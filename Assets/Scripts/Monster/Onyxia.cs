using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Onyxia : Monster, IDamagable, IFlyable
{
    public enum State { Idle, Trace, Fly, Attack, Hit, Die}
    private MonsterStateMachine<State, Onyxia> stateMachine;

    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private float attackRange;
    [SerializeField] public float moveSpeed;
    [SerializeField] public GameObject target;
    [SerializeField] private float hp = 100;

    public Animator animator;
    private ViewDetector viewDetector;
    public GameObject traceTarget;
    public GameObject attackTarget;
    public CharacterController characterController;
    public PlayerController player;


    [Header("View Detector")]
    [SerializeField] private float viewRadius = 1f;
    [SerializeField, Range(0f, 360f)] private float viewAngle = 30f;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask ObstacleMask;



    private void Awake()
    {
        viewDetector = GetComponent<ViewDetector>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        stateMachine = new MonsterStateMachine<State, Onyxia>();
        stateMachine.Init(this);

        stateMachine.AddState(State.Idle, new DragonState.IdleState());
        stateMachine.AddState(State.Trace, new DragonState.TraceState());
        stateMachine.AddState(State.Attack, new DragonState.AttackState());
        stateMachine.AddState(State.Fly, new DragonState.FlyState());
        stateMachine.AddState(State.Hit, new DragonState.HitState());
        stateMachine.AddState(State.Die, new DragonState.DieState());

        stateMachine.ChangeState(State.Idle);
    }

    private void Update()
    {
        FindTraceTarget();
    }
    public void FindTraceTarget()
    {
        FindTarget();
        //traceTarget = target;
        Debug.Log(traceTarget);

        if (Input.GetKeyDown(KeyCode.K))
            Debug.Log(stateMachine.curState);

    }
    public void FindTarget()
    {

        // 1. 범위 내에 있는가
        Collider[] targets = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targets.Length; i++)
        {
            Vector3 dirToTarget = (targets[i].transform.position - transform.position).normalized;


            // 2. 각도 내에 있는가
            // Vector3.Dot(transform.forward, dirToTarget) == Cos("타겟까지 각도");
            // cos과 각도를 비교할 수는 없으니, 시야각의 반을 Cos해주어서 Cos과 Cos끼리 비교
            // 더 큰 값이 더 작은 각도
            if (Vector3.Dot(transform.forward, dirToTarget) < Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad))
                continue;

            // 3. 중간에 장애물이 있는가
            float disToTarget = Vector3.Distance(transform.position, targets[i].transform.position);
            if (Physics.Raycast(transform.position, dirToTarget, disToTarget, ObstacleMask))
                continue;

            Debug.DrawRay(transform.position, dirToTarget * disToTarget, Color.red);

            traceTarget = targets[i].gameObject;
            return;
        }
        traceTarget = null;
    }

    public void Move()
    {
        Vector3 moveDir = traceTarget.transform.position - transform.position;
        characterController.Move(moveDir.normalized * Time.deltaTime * moveSpeed);
        transform.LookAt(new Vector3(traceTarget.transform.position.x, transform.position.y, traceTarget.transform.position.z));
    }

    public void FindAttackTarget()
    {
        Collider[] attackTargets = Physics.OverlapSphere(transform.position, attackRange, targetLayerMask);
        if (attackTargets.Length > 0)
        {
            attackTarget = attackTargets[0].gameObject;
            return;
        }

        attackTarget = null;
    }


    public void ChangeState(State nextState)
    {
        stateMachine.ChangeState(nextState);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + viewAngle * 0.5f);
        Vector3 lefttDir = AngleToDir(transform.eulerAngles.y - viewAngle * 0.5f);

        Debug.DrawRay(transform.position, lookDir * viewRadius, Color.green);
        Debug.DrawRay(transform.position, rightDir * viewRadius, Color.blue);
        Debug.DrawRay(transform.position, lefttDir * viewRadius, Color.blue);
    }

   

    public void TakeHit(float damage)
    {
        Hp -= damage;

        if (Hp > 0)
        {
            if (Hp > 50)
                ChangeState(State.Hit);
            else 
                ChangeState(State.Fly);
        }
        else ChangeState(State.Die);
     }


    public void Fly()
    {
    }
}

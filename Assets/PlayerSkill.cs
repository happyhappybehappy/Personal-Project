using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSkill : MonoBehaviour
{
    public enum State {Idle, HeroicLeap, Charge}
    private SkillStateMachine<State, PlayerSkill> skillStateMachine;

    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] public float skilledSpeed;
    [SerializeField] public GameObject target;
    [SerializeField] private float attackRange;

    public Animator animator;
    public CharacterController characterController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        skillStateMachine = new SkillStateMachine<State, PlayerSkill>();
        skillStateMachine.Init(this);

        skillStateMachine.AddState(State.Idle, new PlayerSkillStates.IdleState());
        skillStateMachine.AddState(State.HeroicLeap, new PlayerSkillStates.HeroicLeapState());
        skillStateMachine.AddState(State.Charge, new PlayerSkillStates.ChargeState());

        skillStateMachine.ChangeState(State.Idle);
    }

    public void ChangeState(State nexeState)
    {
        skillStateMachine.ChangeState(nexeState);
    }

    private void Update()
    {
        WaitInputOnSkill();
    }

    public void WaitInputOnSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            skillStateMachine.ChangeState(State.Charge);

        else if (Input.GetKeyDown(KeyCode.Alpha2))
            skillStateMachine.ChangeState(State.HeroicLeap);
    }

}

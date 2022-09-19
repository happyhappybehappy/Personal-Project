using PlayerSkillStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonState
{
    public class BaseState : State<Onyxia>
    {
        public override void Enter(Onyxia Owner)
        {

        }
        public override IEnumerator Excute(Onyxia Owner)
        {
            yield return null;
        }

        public override void Exit(Onyxia Owner)
        {
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(Onyxia Owner)
        {
            //base.Enter(Owner); // 초기화
            Owner.animator.SetBool("Run", false);
        }

        public override IEnumerator Excute(Onyxia Owner)
        {
            while (true)
            {
                Owner.FindTraceTarget();
                // 하는 게 없음

                if (Owner.traceTarget != null)
                    Owner.ChangeState(Onyxia.State.Trace);

                yield return null;
            }
        }

        public override void Exit(Onyxia Owner)
        {
            Debug.Log("idle 끝");
        }
    }

    public class TraceState : BaseState
    {
        public override void Enter(Onyxia Owner)
        {
            Owner.animator.SetBool("Run", true);
        }

        public override IEnumerator Excute(Onyxia Owner)
        {
            while (true)
            {
                if (Owner.traceTarget == null)
                {
                    Owner.ChangeState(Onyxia.State.Idle);
                    //yield return null;
                }

                if (Owner.attackTarget != null)
                {
                    Owner.ChangeState(Onyxia.State.Attack);
                    //   yield return null;
                }

              
                Owner.FindAttackTarget();

                Owner.Move();

                Debug.Log("trace");
                yield return null;
            }
        }

        public override void Exit(Onyxia Owner)
        {
            //  Owner.animator.SetBool("Run Forward", false);
        }
    }

    public class AttackState : BaseState
    {
        [SerializeField] public int randNum;


        public override void Enter(Onyxia Owner)
        {
            Owner.animator.SetBool("IsAttack", true);
            randNum = Random.Range(0, 4);
            switch (randNum)
            {
                case 0:
                    Owner.animator.SetTrigger("Bite");
                    break;
                case 1:
                    Owner.animator.SetTrigger("RightHand");
                    break;
                case 2:
                    Owner.animator.SetTrigger("LeftHand");
                    break;
                case 3:
                    Owner.animator.SetTrigger("Wind");
                    break;
                default :
                    break;
            }
        }

        public override IEnumerator Excute(Onyxia Owner)
        { 
            yield return new WaitForSeconds(1.0f);
            Owner.animator.SetBool("IsAttack", false);
            Owner.ChangeState(Onyxia.State.Trace);
        }

        public override void Exit(Onyxia Owner)
        {
        }
    }

    public class FlyState : BaseState
    {
        public override void Enter(Onyxia Owner)
        {

        }

        public override IEnumerator Excute(Onyxia Owner)
        {
   
            yield return null;
        }

        public override void Exit(Onyxia Owner)
        {

        }
    }

    public class HitState : BaseState
    {
        public override void Enter(Onyxia Owner)
        {
            Owner.animator.SetTrigger("Hit");

        }

        public override IEnumerator Excute(Onyxia Owner)
        {
            yield return new WaitForSeconds(1f);
        }

        public override void Exit(Onyxia Owner)
        {

        }
    }

    public class DieState : BaseState
    {
        public override void Enter(Onyxia Owner)
        {
            Owner.animator.SetTrigger("Die");
        }

        public override IEnumerator Excute(Onyxia Owner)
        {
            yield return new WaitForSeconds(1.5f);
        }

        public override void Exit(Onyxia Owner)
        {

        }
    }
}
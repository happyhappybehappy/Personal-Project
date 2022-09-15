using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerSkillStates
{
    public class BaseState : State<PlayerSkill>
    {
        public override void Enter(PlayerSkill Player)
        {
           
        }
        public override IEnumerator Excute(PlayerSkill Player)
        {
            yield return null;
        }

        public override void Exit(PlayerSkill Player)
        {
        }
    }

    public class IdleState : BaseState
    {
        public override void Enter(PlayerSkill Player)
        {
            Player.animator.SetBool("HeroicLeap", false);
            Player.animator.SetBool("Charge", false);
        }

        public override IEnumerator Excute(PlayerSkill Player)
        {
            if (Player.animator.GetBool("HeroicLeap"))
            {
                Player.ChangeState(PlayerSkill.State.HeroicLeap);
            }

            else if (Player.animator.GetBool("Charge"))
            {
                Player.ChangeState(PlayerSkill.State.Charge);
            }

            yield return null;

        }
        public override void Exit(PlayerSkill Player)
        {
        }
    }

    public class HeroicLeapState : BaseState
    {
        public Transform player;
        public Transform target;
        public PlayerController playerController;
        Vector3 firstPos, secondPos, thirdPos;

        public override void Enter(PlayerSkill Player)
        {
            playerController = Player.GetComponent<PlayerController>();
            if (target == null) target = playerController.GetComponent<PlayerController>().enemy;
            if (player == null) player = playerController.GetComponent<Transform>();

            firstPos = player.position;
            secondPos = target.position - player.position;
            thirdPos = target.position;
        }
        public override IEnumerator Excute(PlayerSkill Player)
        {
            Debug.Log("1234");
            Vector3 hpos = player.position - (target.position - player.position);

            Vector3[] jumpPath = { new Vector3(player.position.x, player.position.y, player.position.z),
                                   new Vector3(hpos.x, hpos.y-2, hpos.z),
                                   new Vector3(target.position.x, target.position.y, target.position.z)};

            player.DOPath(jumpPath, 1f, PathType.CatmullRom, PathMode.Full3D);
            Debug.Log("123444");
            yield return new WaitForSeconds(0.1f);
            Player.ChangeState(PlayerSkill.State.Idle);
        }
        public override void Exit(PlayerSkill Player)
        {
        }

    }

    public class ChargeState : BaseState
    {
        private PlayerController playerController = null;
        private CharacterController characterController = null;
        float dis;
        public Transform player, target;
        Vector3 moveDir;

        public override void Enter(PlayerSkill Player)
        {
            playerController = Player.GetComponent<PlayerController>();
            if (target == null) target = playerController.enemy;
            if (player == null) player = playerController.GetComponent<Transform>();

            moveDir = target.position - player.position;
        }

        public override IEnumerator Excute(PlayerSkill Player)
        {
            Debug.Log("1");
            characterController.Move(moveDir.normalized * Time.deltaTime * 2f);
            Debug.Log("2");
            player.LookAt(target.position);
            Debug.Log("3");
            yield return new WaitForSeconds(0.1f);
            Debug.Log("4");
            Player.ChangeState(PlayerSkill.State.Idle);
            Debug.Log("5");
        }
        public override void Exit(PlayerSkill Player)
        {
        }
    }


 
}

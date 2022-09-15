using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class PlayerChargeAttack : StateMachineBehaviour
{
    private Animator animator;
    private PlayerController playerController = null;
    private CharacterController characterController = null;
    //private Camera cam = null;
    float dis;
    public Transform player, other;
    Vector3 moveDir;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        this.animator = animator;

        if (player == null) player = animator.GetComponent<Transform>();
        if (playerController == null) playerController = animator.GetComponent<PlayerController>();
        if (characterController == null) characterController = animator.GetComponent<CharacterController>();
        if (other == null) other = playerController.enemy;
        moveDir = other.position - player.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        characterController.SimpleMove(moveDir.normalized);
        player.LookAt(other.position);
        //agent.destination = other.position;
        //Charge();
        // if(Vector3.Distance(player.position, other.position) >= 1f)  Charge();
        //playerController.StartCoroutine(ChargeAttack());
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public void Charge()
    {
        dis = Vector3.Distance(player.position, other.position);
        if (dis >= 1)
        {
           dis = Vector3.Distance(player.position, other.position);
           player.position = Vector3.MoveTowards(player.position, other.position, 2f * Time.deltaTime);
        }
        // player.position = Vector3.MoveTowards(player.position, other.position, 1f * Time.deltaTime);
        // agent.destination = other.position;
        //player.position = Vector3.Lerp(player.position, other.position, 1f);
        //player.position = Vector3.MoveTowards(player.position, other.position, Time.deltaTime);
    }

    IEnumerator ChargeAttack()
    {
        float dis = Vector3.Distance(player.position, other.position);
            //dis = Vector3.Distance(player.position, other.position);
            //player.position = Vector3.MoveTowards(player.position, other.position, 2f * Time.deltaTime);
            player.position = Vector3.Lerp(player.position, other.position, dis * Time.deltaTime);
        yield return null;
    }

}

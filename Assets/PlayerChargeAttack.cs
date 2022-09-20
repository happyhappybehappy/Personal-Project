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
        /*dis = Vector3.Distance(player.position, other.position);
        if (dis >= 2f)
        {
        player.LookAt(other.position);
            dis = Vector3.Distance(player.position, other.position);
            player.position = Vector3.MoveTowards(player.position, other.position, 10f * Time.deltaTime);
        }*/
        dis = Vector3.Distance(player.position, other.position);
        if(dis >=1f)
        {
            player.LookAt(other.position);
            player.position = Vector3.Lerp(player.position, other.position, 0.1f);
        }
        else
        {
            animator.SetBool("Charge", false);
            animator.SetTrigger("ChargeAttack");
        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public void Charge()
    {
        dis = Vector3.Distance(player.position, other.position);
        if (dis >= 0.011)
        {
           dis = Vector3.Distance(player.position, other.position);
           player.position = Vector3.MoveTowards(player.position, other.position, 4f * Time.deltaTime);
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

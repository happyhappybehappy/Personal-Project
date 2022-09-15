using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using DG.Tweening;
public class HeroicLeap : StateMachineBehaviour
{
    private float diameter, radius, variableX, variableY, variableZ, theta;
    private int count;
    private Vector3 midPoint;
    public Transform transform;
    public Transform other;
    public PlayerController playerController;

    Vector3 firstPos, secondPos, thirdPos;
    public float firingAngle = 45f;
    private float gravity = 9.8f;
    private Transform Projectile;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (transform == null) transform = animator.GetComponent<Transform>();
        if (playerController == null) playerController = animator.GetComponent<PlayerController>();
        if (other == null)  other = playerController.enemy;
        if (Projectile == null) Projectile = animator.GetComponent<Transform>();

        /*
                diameter = Mathf.Sqrt(Mathf.Pow((transform.position.x - other.transform.position.x), 2) + (Mathf.Pow((transform.position.z - other.transform.position.z), 2)));//technically we need y as well but for your usage you don't need it unless you are jumping to a different y height
                midPoint = ((other.transform.position - transform.position) / 2);//this is the point from where the radius vector will extend, aka center of the circle/arc
                radius = diameter / 2;
                count = 0;*/

        firstPos = transform.position;
        //Vector3 secondPos = firstPos + new Vector3(5, 5, 0);
        secondPos = other.position-transform.position;
        thirdPos = other.position;
        //Vector3 thirdPos = firstPos + new Vector3(10, 0, 0);

        playerController.StartCoroutine(Jumping());


    }
    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(0.1f);
        Projectile.position = transform.position + new Vector3(0, 10.0f, 0);

        float dis = Vector3.Distance(Projectile.position, other.position);

        float projectileVelocity = dis / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        float Vx = Mathf.Sqrt(projectileVelocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectileVelocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = dis / Vx;

        Projectile.rotation = Quaternion.LookRotation(other.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // Jump();
    }
    public void Jump() 
    {
        Vector3 hpos = transform.position - other.position;// ((other.position - transform.position));
       // Vector3 hpos = transform.position - ((other.position - transform.position) / 2);

        Vector3[] Jumppath = { new Vector3(transform.position.x, transform.position.y, transform.position.z),
                                   new Vector3(hpos.x, hpos.y+3, hpos.z),
                                   new Vector3(other.position.x, other.position.y, other.position.z)};

        transform.DOPath(Jumppath, 3f, PathType.Linear, PathMode.Full3D).SetLookAt(other.position);
/*        if (count <= 180)
            {
                count++;
                theta = Mathf.Deg2Rad * count;
                //theta = count * Mathf.Deg2Rad;//have to convert to radians to be used in sin, cos
                variableX = Mathf.Sin(theta) / radius;//X vector
                variableY = Mathf.Cos(theta) / radius;//Y vector
                variableZ = Mathf.Sqrt((Mathf.Pow(variableX, 2) + Mathf.Pow(variableY, 2)) / (2 * radius * radius));//Z vector

            //transform.position = midPoint + new Vector3(variableX, variableY, variableZ);//radius vector extending out from the midpoint, this will move around the arc/circle at 1degree per frame
            //   transform.position = Vector3.Slerp(transform.position, midPoint + new Vector3(variableX, variableY, variableZ), 0.05f);
            //transform.DOPath(new[] { secondPos, firstPos + Vector3.up, secondPos + Vector3.left * 2, thirdPos, secondPos + Vector3.right * 2, thirdPos + Vector3.up }, 1f, PathType.CubicBezier).SetEase(Ease.Unset);

*/
        
    }













}

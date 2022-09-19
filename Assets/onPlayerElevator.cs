using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class onPlayerElevator : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject player;
    private CharacterController characterController;
    private bool isUp = false;
    private bool isTrigger = false;
    float timer;
    int waitingTime;
    Vector3 targetUpPos;
    Vector3 targetDownPos;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        timer = 0.0f;
        waitingTime = 2;
        targetUpPos = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        targetDownPos = transform.position;
    }

    private void Update()
    {
        if (isTrigger)
        {

            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                if (isUp) // 현재 엘리베이터가 위에 있다면 내려가기
                {
                    //characterController.Move(new Vector3(transform.position.x, transform.position.y - 10));
                    transform.position = Vector3.MoveTowards(transform.position, targetDownPos, Time.deltaTime * 2f);

                    if (transform.position == targetDownPos)
                    {
                        isUp = false;
                        timer = 0;

                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetUpPos, Time.deltaTime * 2f);
                    if (transform.position == targetUpPos)
                    {
                        isUp = true;
                        timer = 0;
                    }
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("엘리");
        if (collision.gameObject.tag == "Player")
        {
            isTrigger = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("엘리");
        if (other.gameObject.tag == "Player")
        {
            isTrigger = true;
        }
    }


}


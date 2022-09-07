using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private CharacterController characterController;
    private Vector3 movePoint;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 1f);

            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                movePoint = raycastHit.point;
                Debug.Log("movePoint : " + movePoint.ToString());
                Debug.Log("¸ÂÀº °´Ã¼ : " + raycastHit.transform.name);
            }
        }
        if (Vector3.Distance(transform.position, movePoint) > 0.1f)
        {
            Vector3 thisUpdatePoint = (movePoint - transform.position).normalized * speed;
            characterController.SimpleMove(thisUpdatePoint);
        }

    }
}
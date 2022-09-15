using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotate : MonoBehaviour
{
    private Camera cam;
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraClickedRotate();
    }

    public void CameraClickedRotate()
    {
        if (Input.GetMouseButton(1))
        {
            xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed; ;
            yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed; ;

            yRotate = yRotate + yRotateMove;
            //xRotate = transform.eulerAngles.x + xRotateMove; 
            xRotate = xRotate + xRotateMove;

            xRotate = Mathf.Clamp(xRotate, -90, 90); // ��, �Ʒ� ����

            //transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

            Quaternion quat = Quaternion.Euler(new Vector3(xRotate, yRotate, 0));
            transform.rotation
                = Quaternion.Slerp(transform.rotation, quat, Time.deltaTime /* x speed */);
        }
    }
}

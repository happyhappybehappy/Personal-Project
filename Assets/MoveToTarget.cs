using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;
    Vector3 startPos, endPos;
    float timer;
    float timeToFloor;


    Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        var mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, Cal(t,height) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    float Cal(float t, float height)
    {
        return -4 * height * t* t + 4 * height * t;
    }

    protected IEnumerator Move()
    {
        timer = 0;
        while (transform.position.y >= startPos.y)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 10, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            startPos = transform.position;
            endPos = target.position;
            StartCoroutine("Move");
        }
    }
}

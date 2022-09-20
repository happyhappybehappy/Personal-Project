using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private float hp;
    protected float Hp
    {
        set { hp = value; }
        get { return hp; }
    }

    private float atk;
    protected float Atk
    {
        set { atk = value; }
        get { return atk; }
    }



}


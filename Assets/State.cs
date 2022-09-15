using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    public abstract void Enter(T Player);
    public abstract IEnumerator Excute(T Player);
    public abstract void Exit(T Player);
}

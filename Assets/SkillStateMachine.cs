using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class SkillStateMachine<T1, T2> where T2 : MonoBehaviour
{
    private T2 Player;
    public State<T2> curState;
    private Coroutine curCorutine;
    private Dictionary<T1, State<T2>> states;

    public void Init(T2 Player)
    {
        this.Player = Player;
        curState = null;
        curCorutine = null;
        states = new Dictionary<T1, State<T2>>();
    }

    public void AddState(T1 type, State<T2> state)
    {
        states.Add(type, state);
    }

    public void ChangeState(T1 type)
    {

        if (curCorutine != null)
        {
            Player.StopCoroutine(curCorutine);
        }

        if (curState != null)
            curState.Exit(Player);

        curState = states[type];

        curState.Enter(Player);
        curCorutine = Player.StartCoroutine(curState.Excute(Player));
    }
}

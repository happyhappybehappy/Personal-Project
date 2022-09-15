using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickeable
{
    void OnFocused(GameObject gameObject);
    void OnUnFocused();
    void Interaction();
}

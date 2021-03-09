using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State state;
    void Update()
    {
        state.Update();
    }
    public void SetState(State newState)
    {
        state = newState;
        state.Enter();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State// : MonoBehaviour
{
	StateMachine machine_;
	[HideInInspector] public static string name_;
	State(StateMachine machine)
	{
		machine_ = machine;
	}
	public abstract void Update();
	public abstract void Enter();
	virtual public void Exit(State newState)
	{
		machine_.SetState(newState);
	}
}
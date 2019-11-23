﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class StateManager : MonoBehaviour
{
	State currentState;
	Dictionary<string, State> allStates = new Dictionary<string, State>();

	[HideInInspector]
	public Transform mTransform;

	private void Start()
	{
		mTransform = this.transform;

		Init();
	}

	public abstract void Init();

	public void FixedTick()
	{
		if (currentState == null)
			return;

		currentState.FixedTick();
	}

	public void Tick()
	{
		if (currentState == null)
			return;

		currentState.Tick();
	}

	public void LateTick()
	{
		if (currentState == null)
			return;

		currentState.LateTick();
	}

	public void ChangeState(string targetId)
	{
		if (currentState != null)
		{
			//run on exit action of current state
		}

		State targetState = GetState(targetId);
		//run on enter actions
		currentState = targetState;
		currentState.onEnter?.Invoke();
	}

	State GetState(string targetId)
	{
		allStates.TryGetValue(targetId, out State retVal);
		return retVal;
	}

	protected void RegisterState(string stateId, State state)
	{
		allStates.Add(stateId, state);
	}
}

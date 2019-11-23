using UnityEngine;
using System.Collections;

public class MonitorInteractingAnimation : StateAction
{
	CharacterStateManager states;
	string targetBool;
	string targetState;

	public MonitorInteractingAnimation(CharacterStateManager characterStateManager, string targetBool, string targetState)
	{
		states = characterStateManager;
		this.targetBool = targetBool;
		this.targetState = targetState;
	}

	public override bool Execute()
	{
		bool isInteracting = states.anim.GetBool(targetBool);

		if (isInteracting)
		{
			return false;
		}
		else
		{
			states.ChangeState(targetState);

			return true;
		}
	}
}

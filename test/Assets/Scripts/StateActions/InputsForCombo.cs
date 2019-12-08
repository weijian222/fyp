using UnityEngine;
using System.Collections;

public class InputsForCombo : StateAction
{
	bool Rb, Rt, Lb, Lt, isAttacking;
	PlayerStateManager states;

	public InputsForCombo(PlayerStateManager playerStates)
	{
		states = playerStates;
	}

	public override bool Execute()
	{
		if (states.canDoCombo == false)
			return false;

		Rb = Input.GetButton("RB");
		Rt = Input.GetButton("RT");
		Lb = Input.GetButton("LB");
		Lt = Input.GetButton("LT");

		AttackInputs attackInput = AttackInputs.rt;

		if (Rb || Rt || Lb || Lt)
		{
			isAttacking = true;

			if (Rb)
			{
				attackInput = AttackInputs.rb;
			}

			if (Rt)
			{
				attackInput = AttackInputs.rt;
			}

			if (Lb)
			{
				attackInput = AttackInputs.lb;
			}

			if (Lt)
			{
				attackInput = AttackInputs.lt;
			}
		}

		if (isAttacking)
		{
			states.hasCombo = true;
		}

		return false;
	}
}

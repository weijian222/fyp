using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : StateAction
{
	PlayerStateManager s;

	bool Rb, Rt, Lb, Lt, isAttacking, InventoryInput, b_input, x_input, y_input, leftArrow, RightArrow, upArrow, downArrow;


	public InputManager(PlayerStateManager states)
	{
		s = states;
	}

	public override bool Execute()
	{
		bool retVal = false;
		isAttacking = false;

		s.horizontal = Input.GetAxis("Horizontal");
		s.vertical = Input.GetAxis("Vertical");
		Rb = Input.GetButton("RB");
		Rt = Input.GetButton("RT");
		Lb = Input.GetButton("LB");
		Lt = Input.GetButton("LT");
		InventoryInput = Input.GetButton("Inventory");
		b_input = Input.GetButton("B");
		y_input = Input.GetButtonDown("Y");
		x_input = Input.GetButton("X");
		leftArrow = Input.GetButton("Left");
		RightArrow = Input.GetButton("Right");
		upArrow = Input.GetButton("Up");
		downArrow = Input.GetButton("Down");
		s.mouseX = Input.GetAxis("Mouse X");
		s.mouseY = Input.GetAxis("Mouse Y");

		s.moveAmount = Mathf.Clamp01(Mathf.Abs(s.horizontal) + Mathf.Abs(s.vertical));

		retVal = HandleAttacking();
		
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (s.lockOn)
			{
				s.OnClearLookOverride();
			}
			else
			{
				s.OnAssignLookOverride(s.target);
			}
		}

		if (s.canDoCombo)
		{
			bool isInteracting = s.anim.GetBool("isInteracting");
			if (!isInteracting)
			{
				s.canDoCombo = false;
			}
		}
		
		return retVal;
	}

	bool HandleAttacking()
	{
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

		if (y_input)
		{
			//isAttacking = false;
		}

		if (isAttacking)
		{
			s.PlayTargetItemAction(attackInput);
			s.ChangeState(s.attackStateId);
		}

		return isAttacking;
	}
}

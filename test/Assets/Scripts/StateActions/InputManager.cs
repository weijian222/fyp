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

		s.horizontal = Input.GetAxis("Horizontal");
		s.vertical = Input.GetAxis("Vertical");
		Rb = Input.GetButton("RB");
		Rt = Input.GetButton("RT");
		Lb = Input.GetButton("LB");
		Rt = Input.GetButton("LT");
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
		
		return retVal;
	}

	bool HandleAttacking()
	{
		if (Rb || Rt || Lb || Lt)
		{
			//isAttacking = true;
		}

		if (y_input)
		{
			isAttacking = false;
		}

		if (isAttacking)
		{
			//s.PlayTargetAnimation("");
			//s.ChangeState(s.attackStateId);
		}

		return isAttacking;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateManager : CharacterStateManager
{
	[Header("Inputs")]
	public float mouseX;
	public float mouseY;
	public float moveAmount;
	public Vector3 rotateDirection;

	[Header("States")]
	public bool isGrounded;

	[Header("References")]
	public new Transform camera;

	[Header("Movement Stats")]
	public float frontRayOffset = .5f;
	public float movementSpeed = 1;
	public float adaptSpeed = 1;
	public float rotationSpeed = 10;

	[HideInInspector]
	public LayerMask ignoreForGroundCheck;

	public string locomotionId = "locomotion";
	public string attackStateId = "attackState";

	public override void Init()
	{
		base.Init();

		State locomotion = new State(
			new List<StateAction>() //fixedUpdateActions
			{
				new MovePlayerCharacter(this)
			},
			new List<StateAction>() //updateActions
			{
				new InputManager(this)
			},
			new List<StateAction>() //lateUpdateActions
			{
			}
			);

		State attackState = new State(
			new List<StateAction>() //fixedUpdateActions
			{
			},
			new List<StateAction>() //updateActions
			{
			},
			new List<StateAction>() //lateUpdateActions
			{
			}
			);

		RegisterState(locomotionId, locomotion);
		RegisterState(attackStateId, attackState);

		ChangeState(locomotionId);

		ignoreForGroundCheck = ~(1 << 9 | 1 << 10);
	}

	private void FixedUpdate()
	{
		delta = Time.fixedDeltaTime;
		base.FixedTick();
	}

	private void Update()
	{
		delta = Time.deltaTime;
		base.Tick();
	}

	private void LateUpdate()
	{
		base.LateTick();
	}
}

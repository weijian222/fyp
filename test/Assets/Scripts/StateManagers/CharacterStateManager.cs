using UnityEngine;
using System.Collections;

public abstract class CharacterStateManager : StateManager
{
	[Header("References")]
	public Animator anim;
	public new Rigidbody rigidbody;
	public AnimatorHook animHook;
	//public ClothManager clothManager;
	public WeaponHolderManager weaponHolderManager;

	[Header("States")]
	public bool isGrounded;
	public bool useRootMotion;
	public bool lockOn;
	public bool isTwoHanded;
	public bool canDoCombo;
	public bool hasCombo;
	public Transform target;

	[Header("Controller Values")]
	public float vertical;
	public float horizontal;
	public float delta;
	public Vector3 rootMovement;

	[Header("Item Actions")]
	ItemActionContainer[] itemActions;
	public ItemActionContainer[] defaultItemActions = new ItemActionContainer[4];

	[Header("Runtime References")]
	//public List<ClothItem> startingCloths;
	public WeaponItem leftWeapon;
	public WeaponItem rightWeapon;

	protected WeaponItem currentWeaponInUse;
	protected ItemActionContainer currentItemAction;


	public override void Init()
	{
		anim = GetComponentInChildren<Animator>();
		animHook = GetComponentInChildren<AnimatorHook>();
		rigidbody = GetComponentInChildren<Rigidbody>();
		//clothManager = GetComponentInChildren<ClothManager>();
		weaponHolderManager = GetComponentInChildren<WeaponHolderManager>();
		anim.applyRootMotion = false;

		animHook.Init(this);
		itemActions = new ItemActionContainer[4];
		PopulateListWithDefaultItemActions();
	}

	void PopulateListWithDefaultItemActions()
	{
		for (int i = 0; i < defaultItemActions.Length; i++)
		{
			itemActions[i] = defaultItemActions[i];
		}
	}

	ItemActionContainer GetItemActionContainer(AttackInputs ai, ItemActionContainer[] l)
	{
		for(int i = 0; i < l.Length; i++)
		{
			if (l[i].attackInput == ai)
			{
				return l[i];
			}
		}

		return null;
	}

	public void PlayTargetAnimation(string targetAnim, bool isInteracting, bool isMirror = false)
	{
		anim.SetBool("isMirror", isMirror);
		anim.SetBool("isInteracting", isInteracting);
		anim.CrossFade(targetAnim, 0.2f);
	}

	public void PlayTargetItemAction(AttackInputs attackInput)
	{
		ItemActionContainer iac = GetItemActionContainer(attackInput, itemActions);
		if (iac != null)
		{
			iac.ExecuteItemAction(this);
		}
	}

	public virtual void OnAssignLookOverride(Transform target)
	{
		this.target = target;
		if (target != null)
			lockOn = true;
	}

	public virtual void OnClearLookOverride()
	{
		lockOn = false;
	}

	public virtual void UpdateItemActionsWithCurrent()
	{
		ItemActionContainer[] newItemActions = new ItemActionContainer[4];

		for (int i = 0; i < newItemActions.Length; i++)
		{
			newItemActions[i] = new ItemActionContainer();
			newItemActions[i].animName = defaultItemActions[i].animName;
			newItemActions[i].attackInput = defaultItemActions[i].attackInput;
			newItemActions[i].itemAction = defaultItemActions[i].itemAction;
			newItemActions[i].isMirrored = defaultItemActions[i].isMirrored;
			newItemActions[i].itemActual = defaultItemActions[i].itemActual;
		}

		if (weaponHolderManager.rightItem != null)
		{
			//anim.CrossFade(weaponHolderManager.rightItem.oneHanded_anim, 0.2f);

			for (int i = 0; i < weaponHolderManager.rightItem.itemActions.Length; i++)
			{
				ItemActionContainer iac = GetItemActionContainer(weaponHolderManager.rightItem.itemActions[i].attackInput, newItemActions);
				if (iac == null)
					continue;

				iac.animName = weaponHolderManager.rightItem.itemActions[i].animName;
				//iac.attackInput = weaponHolderManager.rightItem.itemActions[i].attackInput;
				iac.itemAction = weaponHolderManager.rightItem.itemActions[i].itemAction;
				iac.itemActual = weaponHolderManager.rightItem;
			}
		}

		if (weaponHolderManager.leftItem != null)
		{
			//anim.CrossFade("L" + weaponHolderManager.leftItem.oneHanded_anim, 0.2f);

			for (int i = 0; i < weaponHolderManager.leftItem.itemActions.Length; i++)
			{
				ItemActionContainer weaponAction = weaponHolderManager.leftItem.itemActions[i];
				if (weaponAction == null)
					continue;

				AttackInputs ai = AttackInputs.lb;
				if (weaponAction.attackInput == AttackInputs.rb)
					ai = AttackInputs.lb;
				if (weaponAction.attackInput == AttackInputs.rt)
					ai = AttackInputs.lt;

				ItemActionContainer iac = GetItemActionContainer(ai, newItemActions);

				iac.animName = weaponHolderManager.leftItem.itemActions[i].animName;
				iac.itemAction = weaponHolderManager.leftItem.itemActions[i].itemAction;
				iac.itemActual = weaponHolderManager.leftItem;
			}
		}

		itemActions = newItemActions;
	}

	public void AssignCurrentWeaponAndAction(WeaponItem weapon, ItemActionContainer iac)
	{
		currentWeaponInUse = weapon;
		currentItemAction = iac;
	}

	public void HandleDamageCollider(bool status)
	{
		if (currentWeaponInUse == null)
			return;
		currentWeaponInUse.weaponHook.DamageColliderStatus(status);
	}

	public virtual void CheckForComboPrompt()
	{
		
	}

	public void DisableCombo()
	{
		if (currentItemAction != null)
		{
			currentItemAction.animIndex = 0;
		}
	}
}

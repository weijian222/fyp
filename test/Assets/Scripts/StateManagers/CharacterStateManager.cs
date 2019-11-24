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
}

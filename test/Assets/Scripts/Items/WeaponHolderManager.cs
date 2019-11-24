using UnityEngine;
using System.Collections;

public class WeaponHolderManager : MonoBehaviour
{
	public WeaponHolderHook leftHook;
	public WeaponHolderHook rightHook;

	public void Init()
	{
		WeaponHolderHook[] weaponHolderHooks = GetComponentsInChildren<WeaponHolderHook>();
		foreach (WeaponHolderHook hook in weaponHolderHooks)
		{
			if (hook.isLeftHook)
			{
				leftHook = hook;
			}
			else
			{
				rightHook = hook;
			}
		}
	}

	public void LoadWeaponOnHook(WeaponItem weaponItem, bool isLeft)
	{
		if (isLeft)
		{
			leftHook.LoadWeaponModel(weaponItem);
		}
		else
		{
			rightHook.LoadWeaponModel(weaponItem);
		}
	}
}

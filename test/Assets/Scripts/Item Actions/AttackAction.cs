using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Test/Item Actions/Attack Action")]
public class AttackAction : ItemAction
{
	public override void ExecuteAction(ItemActionContainer ic, CharacterStateManager cs)
	{
		if (ic.animIndex > ic.animName.Length - 1)
		{
			return;
		}

		cs.AssignCurrentWeaponAndAction((WeaponItem) ic.itemActual, ic);
		cs.PlayTargetAnimation(ic.animName[ic.animIndex], true, ic.isMirrored);

		ic.animIndex++;
		//if (ic.animIndex > ic.animName.Length - 1)
		//{
		//	ic.animIndex = 0;
		//	cs.canDoCombo = false;
		//}
	}
}

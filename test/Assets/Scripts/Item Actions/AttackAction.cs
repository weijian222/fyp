using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Test/Item Actions/Attack Action")]
public class AttackAction : ItemAction
{
	public override void ExecuteAction(ItemActionContainer ic, CharacterStateManager cs)
	{
		cs.PlayTargetAnimation(ic.animName, true, ic.isMirrored);
	}
}

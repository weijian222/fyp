using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemActionContainer
{
	public string animName = "punch 1";
	public ItemAction itemAction;
	public AttackInputs attackInput;
	public bool isMirrored;

	public void ExecuteItemAction(CharacterStateManager characterStateManager)
	{
		itemAction.ExecuteAction(this, characterStateManager);
	}
}

using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Test/Items/Weapon Item")]
public class WeaponItem : Item
{
	public GameObject modelPrefab;

	public ItemActionContainer[] itemActions;
}

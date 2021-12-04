using System;

using UnityEngine;


[CreateAssetMenu(fileName = "BuildingInfo", menuName = "Building/Info")]
public class BuildingInfo : ScriptableObject
{
	[Serializable]
	public class LevelInfo
	{
		public int UpgradeCost;
		public int IncomePerCustomer;
	}

	public string Title;
	public LevelInfo[] Levels;
}

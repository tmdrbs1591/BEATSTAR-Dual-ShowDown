using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DataBase : ScriptableObject
{
	public List<DataBaseEntity> Entities; // Replace 'EntityType' to an actual type that is serializable.
}

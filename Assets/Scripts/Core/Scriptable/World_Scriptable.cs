using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World_Scriptable", menuName = "Scriptable/World", order = 0)]
public class World_Scriptable : ScriptableObject
{
    public LevelData_Scriptable[] levels;
}


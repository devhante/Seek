using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Misson", order = 1)]
public class Mission : ScriptableObject
{
    public string[] demands;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;
}
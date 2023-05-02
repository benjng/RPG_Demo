using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    // Entity
    public float runSpeed, walkSpeed;
    public GameObject[] itemDrops;
}

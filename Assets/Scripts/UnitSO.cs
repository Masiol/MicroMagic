using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Unit ", order = 1)]
public class UnitSO : ScriptableObject
{
    public int health = 100;
    public float attackRange = 5f;
    public int damage = 10;
    public float moveSpeed = 5f;
    public float maxScaleDuringAttack;
    public float minScaleDuringAttack;
    public float attackSpeed;
    public float scaleSpeed;
}

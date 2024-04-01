using System.Collections.Generic;
using UnityEngine;

public abstract class AttackData : ScriptableObject
{
    public abstract void Attack(Transform attacker, Transform target, string targetTag);
}
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New Parabolic Attack Data", menuName = "Attack Data/Parabolic Attack")]
public class ParabolicAttackData : AttackData
{
    public float height = 1f;
    public float duration = 2f;

    public override void Attack(Transform attacker, Transform target, string targetTag)
    {
        if (attacker == null || target == null)
        {
            Debug.LogWarning("Attacker or target is null.");
            return;
        }

        GameObject projectilePrefab = attacker.GetComponent<Unit>().projectilePrefab;
        int damage = attacker.GetComponent<Unit>().unitSO.damage;

        GameObject projectileObject = Instantiate(projectilePrefab, attacker.position, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        if (projectile != null)
        {
            Vector3 targetPosition = target.position;

            ParabolicMove parabolicMove = projectileObject.AddComponent<ParabolicMove>();
            parabolicMove.height = height;
            parabolicMove.duration = duration;
            parabolicMove.MoveOnParabola(attacker.transform, target);
            projectile.SetupObject((targetPosition - attacker.position).normalized, targetTag, damage);
        }
        else
        {
            Debug.LogWarning("Projectile component not found.");
            Destroy(projectileObject);
        }
    }
}

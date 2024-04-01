using UnityEngine;

[CreateAssetMenu(fileName = "New Linear Attack Data", menuName = "Attack Data/Linear Attack")]
public class LinearAttackData : AttackData
{
    public override void Attack(Transform attacker, Transform target, string targetTag)
    {
        GameObject projectilePrefab = attacker.GetComponent<Unit>().projectilePrefab;
        int damage = attacker.GetComponent<Unit>().unitSO.damage;

        GameObject projectileObject = Instantiate(projectilePrefab, attacker.position, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        if (projectile != null && target != null)
        {
            Vector3 direction = (target.position - attacker.position).normalized;
            projectile.SetupObject(direction, targetTag, damage);

            // Ustawienie ruchu pocisku
            projectileObject.transform.Translate(direction * projectile.speed * Time.deltaTime, Space.World);
        }
    }
}
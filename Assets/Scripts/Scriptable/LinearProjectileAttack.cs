using UnityEngine;

public class LinearProjectileAttack : MonoBehaviour, IAttack
{
    public GameObject projectilePrefab;

    public void Attack(Transform target, string targetTag, int damage)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        if (projectile != null && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            projectile.SetupObject(direction, targetTag, damage);

            // Ustawienie ruchu pocisku
            projectileObject.transform.Translate(direction * projectile.speed * Time.deltaTime, Space.World);
        }
    }
}
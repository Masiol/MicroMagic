using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "New Parabolic Attack Data", menuName = "Attack Data/Parabolic Attack")]
public class ParabolicAttackData : AttackData
{
    public float height = 2f;
    public float duration = 1.25f;

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

            // Oblicz docelow¹ pozycjê docelow¹
            Vector3 endPosition = targetPosition + Vector3.up * height;

            // Ustawienie obiektu pocisku
            projectile.SetupObject(Vector3.zero, targetTag, damage);

            // Poruszanie siê po trajektorii parabolicznej
            projectile.transform.DOMove(endPosition, duration).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                // Po zakoñczeniu ruchu, przeka¿ obra¿enia i zniszcz pocisk
                // projectile.Hit();
            });

            // Animowanie lotu pocisku po paraboli
            projectile.transform.DOPath(new Vector3[] { attacker.position, (attacker.position + targetPosition) / 2 + Vector3.up * height, endPosition }, duration, PathType.CatmullRom)
                .SetEase(Ease.Linear);
        }
        else
        {
            Debug.LogWarning("Projectile component not found.");
            Destroy(projectileObject);
        }
    }
}

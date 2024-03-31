using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 10f;
    private string objectTag;
    private int damage;
    public GameObject hitEffect;
    private float checkDelay = 0.25f;
    private float timer = 0f;

    private void Start()
    {
        transform.Rotate(-90f, 0f, 0f);
    }

    private void Update()
    {
        Move();

        if (objectTag == null)
        {
            timer += Time.deltaTime;
            if (timer >= checkDelay)
            {
                timer = 0f;
                if (objectTag == null)
                {
                    Destroy(gameObject);
                    return;
                }
            }
        }
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetupObject(Vector3 _dir, string _tag, int _damage)
    {
        direction = _dir;
        objectTag = _tag;
        damage = _damage;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (objectTag == null) return;
        if (_other.CompareTag(objectTag))
        {
            Health health = _other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            SpawnHitEffect(_other.ClosestPoint(transform.position), _other.transform);
            Destroy(gameObject, 0.15f);
        }
        else if (_other.CompareTag("Obstacle"))
        {
            Destroy(gameObject, 0.5f);
        }
    }

    private void SpawnHitEffect(Vector3 hitPoint, Transform hitTransform)
    {
        GameObject effect = Instantiate(hitEffect, hitPoint, Quaternion.identity);

        if (hitTransform != null)
        {
            effect.transform.up = hitTransform.up;
            effect.transform.forward = hitTransform.forward;
        }
    }
}

using System.Collections;
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

    [SerializeField] private bool specjalAttack;

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
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (objectTag == null) return;
        if (_other.CompareTag(objectTag))
        {
            Health health = _other.GetComponent<Health>();
            if (health != null)
            {
                if(specjalAttack)
                StartCoroutine(DoDamage(health));
                else
                    health.TakeDamage(damage);
            }
            if(!specjalAttack)
            SpawnHitEffect(_other.ClosestPoint(transform.position), _other.transform);
            else
            {
            SpawnHitEffect(_other.transform.position, _other.transform);
            }
                
            Destroy(gameObject);
        }
        else if (_other.CompareTag("Obstacle"))
        {
            Destroy(gameObject, 0.1f);
        }
    }

    private void SpawnHitEffect(Vector3 hitPoint, Transform hitTransform)
    {
        if (!specjalAttack)
        {
            GameObject effect = Instantiate(hitEffect, hitPoint, Quaternion.identity);

            if (hitTransform != null)
            {
                effect.transform.up = hitTransform.up;
                effect.transform.forward = hitTransform.forward;
            }
        }
        else
        {
            //Vector3 hitPointOffset = new Vector3(hitTransform.position.x, hitTransform.position.y - 0.5f, hitTransform.position.z);
            GameObject effect2 = Instantiate(hitEffect, new Vector3(hitPoint.x, 8.39f, hitPoint.z), Quaternion.identity);
            effect2.transform.localScale = transform.localScale;
        }

    }

    private IEnumerator DoDamage(Health health)
    {
        yield return new WaitForSeconds(1);
        health.TakeDamage(damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEditor;

public class Unit : MonoBehaviour
{
    public UnitSO unitSO;
    private NavMeshAgent agent;
    public Transform target;
    public string targetTag;
    public bool isAttacking;
    private float lastAttackTime;

    [SerializeField] private bool isEnemyUnit;
    private bool gameStarted = false;

    public float floatHeight = 1f;
    public float floatDuration = 1f;
    public Ease floatEaseType = Ease.InOutQuad;

    public float minDelay = 0f; // Minimalne opóŸnienie
    public float maxDelay = 1f;

    private Vector3 initialPosition;
    public AttackData attackData;

    public GameObject projectilePrefab;


    private void Start()
    {
        GameManager.OnGameStart += StartUnitMoving;
        agent = GetComponent<NavMeshAgent>();
        targetTag = isEnemyUnit ? "Unit" : "EnemyUnit";

        if (isEnemyUnit)
        {
            initialPosition = transform.position;
            float randomDelay = Random.Range(minDelay, maxDelay);
            GetComponent<UnitValidate>().setUnitOnGround = true;
            Invoke("StartFloating", randomDelay);
        }
        //FindClosestEnemy();
    }
    private void Update()
    {
        if (gameStarted)
        {
            if (!isAttacking)
                FindClosestEnemy();
            else
            {
                if (Time.time - lastAttackTime >= unitSO.attackSpeed)
                {
                    lastAttackTime = Time.time;
                    AttackTarget();
                }
            }
            CheckForAttackTarget();
        }
    }

    private void StartUnitMoving()
    {
        gameStarted = true;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
            StartCoroutine(MoveToTarget());
            // Tutaj mo¿esz wykonaæ jakieœ dodatkowe dzia³ania zwi¹zane z najbli¿szym wrogiem
            // np. atakowaæ, œledziæ, itp.
        }
        else
        {
            // Jeœli nie znaleziono wrogów, wykonaj odpowiednie dzia³ania
            Debug.LogWarning("Nie znaleziono wrogów.");
        }
    }
    private IEnumerator MoveToTarget()
    {
        while (target != null && !isAttacking)
        {
            if (target != null) // Dodaj ten warunek
            {
                agent.SetDestination(target.position);
                yield return null;

               /* if (Vector3.Distance(transform.position, target.position) <= unitSO.attackRange)
                {
                    CheckForAttackTarget();
                }*/
            }
            else
            {
                yield break; // Jeœli target jest nullem, przerwij pêtlê
            }
        }
    }
    private void CheckForAttackTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, unitSO.attackRange);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(targetTag))
            {
                // Znaleziono cel ataku w zasiêgu
                target = col.transform;
                isAttacking = true;
                agent.ResetPath(); // Przerywamy pod¹¿anie do wroga
                // Tutaj mo¿esz wykonaæ dodatkowe dzia³ania zwi¹zane z atakiem na cel
                Debug.Log("Znaleziono cel ataku: " + target.name);
                break;
            }
            else
            {
                target = null;
                isAttacking = false;
                StartCoroutine(MoveToTarget());
            }

        }
    }
    private void AttackTarget()
    {
        Debug.Log("atak");
        if (target != null && target.gameObject.activeSelf)
        {
            transform.DOScale(unitSO.maxScaleDuringAttack, unitSO.scaleSpeed).OnComplete(() =>
            {
                // Po zakoñczeniu skalowania, wracamy do normalnej skali
                transform.DOScale(unitSO.minScaleDuringAttack, unitSO.scaleSpeed);

                // Instancjonowanie obiektu ataku
                if (attackData != null && target != null)
                {
                    attackData.Attack(transform, target, targetTag);
                }
            });
        }
        else
        {
            FindClosestEnemy();
            isAttacking = false;
        }
    }
    public void SetInitialPosY()
    {
        initialPosition = transform.position;
    }
    public void StartFloating()
    {
        float targetY = initialPosition.y + floatHeight;

        // Animacja ping-pongu od wartoœci pocz¹tkowej do wartoœci docelowej i z powrotem
        transform.DOMoveY(targetY, floatDuration).SetEase(floatEaseType).SetLoops(-1, LoopType.Yoyo);
    }
}

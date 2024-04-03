using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEditor;
using System;

public class Unit : MonoBehaviour
{
    public UnitSO unitSO;
    public bool isEnemyUnit;
    private NavMeshAgent agent;
    public Transform target;
    private string targetTag;
    private bool isAttacking;
    private float lastAttackTime;
    private Vector3 initialPosition;
    
    private bool gameStarted = false;

    [SerializeField] private float floatHeight = 1f;
    [SerializeField] private float floatDuration = 1f;
    [SerializeField] private Ease floatEaseType = Ease.InOutQuad;

    [SerializeField] private float minDelay = 0f;
    [SerializeField] private float maxDelay = 1f;

    
    [SerializeField] private AttackData attackData;

    public GameObject projectilePrefab;

    private void Start()
    {
        GameManager.OnGameStart += StartUnitMoving;
        agent = GetComponent<NavMeshAgent>();
        targetTag = isEnemyUnit ? "Unit" : "EnemyUnit";

        if (isEnemyUnit)
        {
            initialPosition = transform.position;
            float randomDelay = UnityEngine.Random.Range(minDelay, maxDelay);
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
        //agent = GeComponent<NavMeshAgent>();

        if (agent != null)
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
        }
    }
    private IEnumerator MoveToTarget()
    {
        while (target != null && !isAttacking)
        {
            if (target != null) 
            {
                agent.SetDestination(target.position);
                yield return null;
            }
            else
            {
                yield break;
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
                target = col.transform;
                isAttacking = true;
                agent.ResetPath();

                // P³ynne obracanie siê w kierunku celu
                if (target != null)
                {
                    Vector3 targetDirection = target.position - transform.position;
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 40);
                }

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
        Debug.Log("Atak");
        if (target != null)
        {

            transform.GetChild(0).DOPunchScale(new Vector3(unitSO.maxScaleDuringAttack / 4, unitSO.maxScaleDuringAttack / 4, unitSO.maxScaleDuringAttack / 4), unitSO.scaleSpeed, 1, 1)
                .SetEase(Ease.InSine);

            if (attackData != null && target != null)
            {
                attackData.Attack(transform, target, targetTag);
            }

            if(unitSO.useParticleWhenAttack)
            {
                Vector3 newRotation = new Vector3(2.415f,25.893f,-113.325f);
                Instantiate(unitSO.particlePrefab, transform.position, transform.rotation);
            }
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

        transform.DOMoveY(targetY, floatDuration).SetEase(floatEaseType).SetLoops(-1, LoopType.Yoyo);
    }

}

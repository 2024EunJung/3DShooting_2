using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // 적 상태 목록
    public enum EnemyState
    {
        Idle,
        Walk,
        Attack,
        Damaged,
        Dead
    }

    public EnemyState eState = EnemyState.Idle; // 기본 상태 설정

    public float hp = 100; // 적 체력
    public Slider hpBar; // 체력바 UI

    private Transform player; // 플레이어
    private NavMeshAgent agent; // 이동을 담당하는 NavMeshAgent
    private Animator anim; // 애니메이터
    private float distance; // 플레이어와의 거리
    private float attackCoolTime; // 공격 쿨타임

    // 사망 이벤트 정의
    public event Action OnEnemyDeath;

    void Start()
    {
        // 플레이어 Transform 가져오기
        player = FindObjectOfType<Player>().transform;

        // 필요한 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 플레이어와 거리 계산
        distance = Vector3.Distance(transform.position, player.position);

        // 애니메이터에 속도 전달
        anim.SetFloat("speed", agent.velocity.magnitude);

        // 상태에 따른 행동
        switch (eState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Walk:
                Walk();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    public void Damaged(float damage)
    {
        // 체력 감소
        hp -= damage;

        // 체력바 업데이트
        if (hpBar != null)
        {
            hpBar.value = hp;
        }

        // 피격 후 상태 전환
        if (hp > 0)
        {
            anim.SetTrigger("damage");
            eState = EnemyState.Damaged;
            agent.isStopped = true;
        }
        else
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        anim.SetTrigger("death");
        eState = EnemyState.Dead;

        // 사망 이벤트 호출
        OnEnemyDeath?.Invoke();

        // NavMeshAgent 비활성화
        agent.enabled = false;

        // 물리 효과 활성화
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        // 2초 대기 후 적 제거
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void DamagedEnd()
    {
        if (hp > 0)
        {
            eState = EnemyState.Idle;
            agent.isStopped = false;
        }
    }

    void Idle()
    {
        if (distance <= 8)
        {
            eState = EnemyState.Walk;
            agent.isStopped = false;
        }
    }

    void Walk()
    {
        if (distance > 8)
        {
            eState = EnemyState.Idle;
            agent.isStopped = true;
            agent.ResetPath();
        }
        else if (distance <= 2)
        {
            eState = EnemyState.Attack;
            agent.isStopped = true;
            agent.ResetPath();
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    void Attack()
    {
        if (distance > 2)
        {
            eState = EnemyState.Walk;
            agent.isStopped = false;
        }
        else
        {
            attackCoolTime += Time.deltaTime;

            if (attackCoolTime >= 1)
            {
                anim.SetTrigger("attack");
                attackCoolTime = 0;
            }
        }
    }

    void RealAttack()
    {
        // 플레이어에게 10 데미지 전달
        player.SendMessage("Damaged", 10);
    }
}

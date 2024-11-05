using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // 적이 가질 수 있는 상태 목록
    public enum EnemyState
    {
        Idle,
        Walk,
        Attack,
        Damaged,
        Dead
    }
    // 상태를 담아둘 변수를 만들고, 기본 상태로 시작
    public EnemyState eState = EnemyState.Idle;

    public float hp = 100; // 적의 체력
    public Slider hpBar; // 적의 체력바

    Transform player; // 플레이어
    NavMeshAgent agent; // NavMeshAgent 컴포넌트
    Animator anim; // Animator 컴포넌트
    float distance; // 플레이어와의 거리
    float attackCoolTime; // 공격 주기를 위한 쿨타임

    void Start()
    {
        // Player 컴포넌트로 찾은 플레이어의 Transform 컴포넌트 가져오기
        player = FindObjectOfType<Player>().transform;

        // 나의 NavMeshAgent 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 적과 플레이어 사이의 거리 계산
        distance = Vector3.Distance(transform.position, player.position);

        anim.SetFloat("velocity", agent.velocity.magnitude);

        // 기본, 이동, 공격 상태일 때 할일 나누기
        switch (eState)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Walk: Walk(); break;
            case EnemyState.Attack: Attack(); break;
        }
    }

    // 공격 받는 기능
    void Damaged(float damage)
    {
        // 공격 받은 데미지 만큼 체력 감소
        hp -= damage;
        // 감소한 체력을 체력바에 표시
        hpBar.value = hp;

        agent.isStopped = true; // 이동 중단
        agent.ResetPath(); // 경로 초기화

        if(hp > 0)// 체력이 남아 있다면
        {
            anim.SetTrigger("damaged"); // 피격 애니메이션 실행
            eState = EnemyState.Damaged; // 피격 상태로 전환
        }
        else // 체력이 남아 있지 않다면
        {
            anim.SetTrigger("dead"); //죽음 애니메이션 실행
            eState = EnemyState.Dead; // 죽음 상태로 전환
        }
    }
    void DamagedEnd()
    {
        eState = EnemyState.Idle; // 기본 상태로 전환
    }
    void Idle() // 기본 상태일 때 계속 할 일
    {
        // 플레이어와의 거리가 8 이하라면
        if(distance <= 8)
        {
            eState = EnemyState.Walk; // 이동 상태로 전환
            agent.isStopped = false; // 이동 시작
        }
    }
    void Walk() // 이동 상태일 때 계속 할 일
    {
        // 플레이어와의 거리가 8보다 크다면
        if(distance > 8)
        {
            eState = EnemyState.Idle; // 기본 상태로 전환
            agent.isStopped = true; // 이동 중단
            agent.ResetPath(); // 경로 초기화
        }
        // 플레이어와의 거리가 2 이하라면
        else if(distance <= 2)
        {
            eState = EnemyState.Attack;
            agent.isStopped = true; // 이동 중단
            agent.ResetPath(); // 경로 초기화

        }
        // 다른 상태로 전환하지 않을 때는
        else
        {
            // 플레이어의 위치를 목적지로설정
            agent.SetDestination(player.position);
        }
    }
    void Attack() // 공격 상태일 때 계속 할 일
    {
        // 플레이어와의 거리가 2보다 크다면
        if (distance > 2)
        {
            eState = EnemyState.Walk; // 이동 상태로 전환
            agent.isStopped = false; // 이동 시작
        }
        // 다른 상태로 전환하지 않을 때는
        else
        {
            // 공격 상태일 때 쿨타임 계산
            attackCoolTime += Time.deltaTime;

            // 공격 쿨타임이 1초 이상이 되면
            if (attackCoolTime >= 1)
            {
                anim.SetTrigger("attack"); // 공격 애니메이션 실행
                attackCoolTime = 0; // 다시 1초를 셀 수 있게 초기화
            }
        }
    }
    // 공격 애니메이션 중간에 호출
    void RealAttack()
    {
        // 플레이어에게 10만큼 공격 받으라고 전달
        player.SendMessage("Damaged", 10);
    }
}

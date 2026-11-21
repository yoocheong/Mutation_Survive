using System;
using UnityEngine;

// 적 공격 관리 (적 프리팹에 붙임)
// 플레이어와 닿아 있는 동안 일정 간격마다 데미지를 줌
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 5f;         // 1회 공격 데미지
    [SerializeField] private float attackInterval = 1f; // 공격 간격(초)

    private EnemyHealth health;     // 같은 오브젝트의 체력 컴포넌트 (생존 여부 확인용)
    private float lastAttackTime;   // 마지막으로 공격한 시각

    // 적 공격력 초기화 메서드 (Enemy에서 스폰할 때 레벨에 맞는 공격력으로 설정)
    public void Initialize(float damage)    // 매개변수로 공격력을 받는다.
    {
        this.damage = damage;
    }

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();   // 처음 생성될 때 한 번만 가져옴
    }

    // 풀에서 다시 꺼낼 때마다 공격 쿨타임 초기화
    private void OnEnable()
    {
        // 풀에서 나오자마자 바로 때릴 수 있게 (현재 시각 - 마지막 공격 시각이 항상 간격 이상이 되도록)
        lastAttackTime = -attackInterval;
    }

    // 플레이어와 닿아 있는 동안 attackInterval마다 데미지
    private void OnCollisionStay(Collision collision)
    {
        if (!health.IsLive) return;     // 죽은 적은 공격하지 않음
        if (!collision.collider.CompareTag("Player")) return;   // 플레이어가 아니면 무시
        if (Time.time - lastAttackTime < attackInterval) return;    // 아직 공격 간격이 안 지났으면 무시

        PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();  // 부딪힌 플레이어의 체력 컴포넌트 가져오기
        if (!player) return;
        player.TakeDamage(damage);  // 플레이어에게 데미지

        lastAttackTime = Time.time; // 공격한 시각 저장 (다음 공격까지 쿨타임 시작)
    }
}
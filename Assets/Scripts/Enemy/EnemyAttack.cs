using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 5f;          // 1회 공격 데미지
    [SerializeField] private float attackInterval = 1f;  // 공격 간격(초)

    private EnemyHealth health;
    private float lastAttackTime;

    public void Initialize(float damage)
    {
        this.damage = damage;
    }

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        // 풀에서 나오자마자 바로 때릴 수 있게
        lastAttackTime = -attackInterval;
    }

    // 플레이어와 닿아 있는 동안 attackInterval마다 데미지
    private void OnCollisionStay(Collision collision)
    {
        if (!health.IsLive) return;
        if (!collision.collider.CompareTag("Player")) return;
        if (Time.time - lastAttackTime < attackInterval) return;
        
        PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
        if (!player) return;
        player.TakeDamage(damage);

        lastAttackTime = Time.time;

    }
}
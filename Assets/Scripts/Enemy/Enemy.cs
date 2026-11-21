using UnityEngine;

// 이동, 체력, 공격 컴포넌트를 묶어서 스폰할 때 한 번에 초기화
public class Enemy : MonoBehaviour
{
    private EnemyMovement movement; // 적 이동 컴포넌트
    private EnemyHealth health;     // 적 체력 컴포넌트
    private EnemyAttack attack;     // 적 공격 컴포넌트

    // 처음 생성될 때 한 번만 컴포넌트를 찾아서 저장 (풀 재사용 시에도 그대로 유지)
    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
        attack = GetComponent<EnemyAttack>();
    }

    // 적 능력치 초기화 메서드 (Spawner에서 스폰할 때 호출)
    public void Initialize(Spawner.SpawnData data)  // 매개변수로 현재 레벨의 스폰 정보를 받는다.
    {
        movement.Initialize(data.speed);    // 이동 속도 설정
        health.Initialize(data.hp);         // 체력 설정
        attack.Initialize(data.damage);     // 공격력 설정
    }
}
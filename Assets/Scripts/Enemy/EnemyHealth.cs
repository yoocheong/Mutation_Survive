using System;
using UnityEngine;

// 데미지를 받아 체력이 0 이하가 되면 사망 처리 후 풀로 반환
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hp;      // 적 체력
    [SerializeField] private float maxHp;   // 적 최대 체력

    public bool IsLive { get; private set; }    // 생존 여부 (외부에서는 읽기만 가능)

    public event Action<EnemyHealth> OnDead;    // 적 사망 이벤트 (경험치 드랍 등에서 구독)

    // 풀에서 다시 꺼낼 때마다 상태 초기화
    private void OnEnable()
    {
        IsLive = true;
        hp = maxHp;
    }

    // 적 체력 초기화 메서드 (Spawner에서 스폰할 때 레벨에 맞는 체력으로 설정)
    public void Initialize(float maxHp) // 매개변수로 최대 체력을 받는다.
    {
        this.maxHp = maxHp;
        hp = maxHp;
        IsLive = true;
    }

    // 데미지 처리 메서드 (무기, 투사체에서 호출)
    public void TakeDamage(float damage)    // 매개변수로 받을 데미지를 받는다.
    {
        if (!IsLive) return;    // 이미 죽은 적은 무시 (같은 프레임에 여러 번 맞아도 사망 처리 한 번만)

        hp -= damage;

        if (hp > 0)
        {
            // 살아있을 때 피격 처리 (피격 애니메이션, 넉백 등 추가 예정)
        }
        else
        {
            Dead(); // 체력이 0 이하면 Dead()함수 호출
        }
    }

    // 적 사망 메서드 (오브젝트 풀 재사용을 위해 파괴하지 않고 비활성화)
    private void Dead()
    {
        IsLive = false;
        OnDead?.Invoke(this);   // 사망 이벤트를 구독한 곳에 알림 (죽은 적 자신을 전달)
        this.gameObject.SetActive(false);   // 풀로 반환
    }
}
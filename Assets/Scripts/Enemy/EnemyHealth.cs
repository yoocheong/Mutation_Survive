using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hp;  // 적 체력
    [SerializeField] private float maxHp;   // 적 최대 체력

    public bool IsLive { get; private set;}

    public event Action<EnemyHealth> OnDead;    // 적 사망 이벤트

    // 
    private void OnEnable()
    {
        IsLive = true;
        hp = maxHp;
    }

    //
    public void Initialize(float maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        IsLive = true;
    }

    //
    public void TakeDamage(float damage)
    {
        if (!IsLive) return;

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
        OnDead?.Invoke(this);
        this.gameObject.SetActive(false);
    }

}

using UnityEngine;

// 플레이어 체력 관리 (플레이어 오브젝트에 붙임)
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float hp;      // 플레이어 체력
    [SerializeField] private float maxHp;   // 플레이어 최대 체력

    public bool IsLive { get; private set; }    // 생존 여부 (외부에서는 읽기만 가능)

    // 시작할 때 체력을 최대로 채우고 생존 상태로 설정
    private void Awake()
    {
        hp = maxHp;
        IsLive = true;
    }

    // 체력 회복 메서드 (레벨업 회복 강화에서 호출, 최대 체력을 넘지 않음)
    public void Heal(float amount)  // 매개변수로 회복량을 받는다.
    {
        if (!IsLive) return;    // 죽은 상태에서는 회복 안 됨
        hp = Mathf.Min(hp + amount, maxHp);     // 회복 후 체력과 최대 체력 중 작은 값으로 설정
    }

    // 데미지 처리 메서드 (적 공격에서 호출)
    public void TakeDamage(float damage)    // 매개변수로 받을 데미지를 받는다.
    {
        if (!IsLive) return;    // 이미 죽었으면 무시

        hp = Mathf.Max(hp - damage, 0f);    // 체력이 0 아래로 내려가지 않게 제한

        if (hp > 0)
        {
            // 살아있을 때 피격 처리 (피격 애니메이션, 넉백 등 추가 예정)
        }
        else
        {
            Dead(); // 체력이 0 이하면 Dead()함수 호출
        }
    }

    // 플레이어 사망 메서드
    private void Dead()
    {
        IsLive = false;
    }
}
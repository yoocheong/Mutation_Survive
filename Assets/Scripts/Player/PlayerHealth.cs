using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float maxHp;

    public bool IsLive { get; private set; }

    private void Awake()
    {
        hp = maxHp;
        IsLive = true;
    }

    public void Heal(float amount)
    {
        hp = Mathf.Min(hp + amount, maxHp);
    }

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

    private void Dead()
    {
        IsLive = false;
    }
}

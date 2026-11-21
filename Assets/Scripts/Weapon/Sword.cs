using UnityEngine;

// MeleeWeapon이 배치하고 회전시키며, 적과 닿으면 직접 데미지를 줌
public class Sword : MonoBehaviour
{
    public float damage;    // 검 데미지
    public int pierceCount; // 관통 횟수 (-1이면 무한 관통)

    // 검 초기화 메서드 (MeleeWeapon에서 배치하거나 레벨업할 때 호출)
    public void Initialize(float damage, int pierceCount)
    {
        this.damage = damage;
        this.pierceCount = pierceCount;
    }

    // 적과 닿았을 때 호출
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return; // 적이 아니면 무시

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();  // 맞은 적의 체력 컴포넌트 가져오기
        if (!enemy || !enemy.IsLive) return;    // 없거나 이미 죽은 적이면 무시

        enemy.TakeDamage(damage);   // 적에게 데미지

        // 무한 관통(-1)이 아니면 관통 횟수 소모
        if (pierceCount == -1) return;

        pierceCount--;
        if (pierceCount < 0)
        {
            gameObject.SetActive(false);    // 관통 횟수를 다 쓰면 비활성화 (풀로 반환)
        }
    }
}
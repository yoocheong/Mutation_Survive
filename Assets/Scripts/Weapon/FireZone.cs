using UnityEngine;

// 지속 시간 동안 일정 간격마다 범위 안의 적에게 데미지를 줌
public class FireZone : MonoBehaviour
{
    public float tickInterval = 0.5f;   // 몇 초마다 데미지 줄지

    private float damage;       // 1회 데미지
    private float duration;     // 불장판 지속 시간
    private float radius;       // 불장판 범위 (반지름)
    private LayerMask targetLayer;  // 데미지를 줄 대상의 레이어 (적 레이어)

    private float lifeTimer;    // 생성 후 지난 시간 (지속 시간 체크용)
    private float tickTimer;    // 다음 데미지까지 시간 측정용

    // 불장판 초기화 메서드 (MolotovWeapon에서 생성할 때 호출)
    // 풀에서 재사용되므로 생성할 때마다 값을 새로 설정
    public void Initialize(float damage, float duration, float radius, LayerMask targetLayer)
    {
        this.damage = damage;
        this.duration = duration;
        this.radius = radius;
        this.targetLayer = targetLayer;

        lifeTimer = 0f;     // 풀 재사용 대비 타이머 리셋
        tickTimer = 0f;

        // 범위에 맞게 크기 조절 (기본 Cylinder 지름이 1이라 반지름 × 2)
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(radius * 2f, scale.y, radius * 2f);  // 높이(y)는 유지
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;

        // 지속 시간이 끝나면 비활성화 (풀로 반환)
        if (lifeTimer >= duration)
        {
            gameObject.SetActive(false);
            return;
        }

        tickTimer += Time.deltaTime;

        // 데미지 간격(tickInterval)이 지나면 타이머 초기화 후 데미지
        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;
            DealDamage();
        }
    }

    // 범위 안의 모든 적에게 데미지를 주는 메서드
    private void DealDamage()
    {
        // 불장판 위치를 중심으로 radius 반지름 안에 있는 targetLayer 콜라이더를 모두 가져옴
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, targetLayer);

        // 감지된 대상들을 하나씩 확인
        foreach (Collider hit in hits)
        {
            EnemyHealth enemy = hit.GetComponent<EnemyHealth>();    // 대상의 체력 컴포넌트 가져오기
            if (!enemy) continue;   // 체력 컴포넌트가 없으면 이 대상은 건너뜀
            enemy.TakeDamage(damage);   // 적에게 데미지
        }
    }
}
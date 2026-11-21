using UnityEngine;

// 가장 가까운 적을 향해 총알을 발사하는 원거리 무기
// 발사 수가 늘어나면 가운데를 기준으로 부채꼴 모양으로 퍼져서 발사
public class RangedWeapon : WeaponBase
{
    public float scanRange = 10f;   // 적 탐색 범위 (반지름)
    public LayerMask targetLayer;   // 탐색할 대상의 레이어 (적 레이어만 감지)

    public int per = 0;             // 총알 관통 횟수 (원거리 무기 전용)
    public float spreadAngle = 15f; // 여러 발 발사 시 총알 사이 각도

    public Vector3 bulletRotationOffset = new Vector3(90f, 0f, 0f); // 총알 모델 회전 보정값

    private float timer;    // 다음 발사까지 시간 측정용

    // 컴포넌트를 처음 붙이거나 Reset 했을 때 인스펙터 기본값 설정
    private void Reset()
    {
        speed = 0.5f;   // 발사 간격(초)
        count = 1;      // 처음엔 1발
    }

    // 원거리 무기는 시작할 때 따로 준비할 것이 없으므로 비워둠
    public override void Initialize() { }

    // 레벨업 시 데미지/발사 수 증가 + 관통 횟수 증가
    public override void LevelUp(float damage, int count)
    {
        base.LevelUp(damage, count);    // 부모 기능 실행 (데미지, 발사 수 증가)
        per += 1;   // 원거리 무기만 관통 +1
    }

    // 매 프레임 발사 타이밍 확인
    protected override void UpdateWeapon()
    {
        timer += Time.deltaTime;

        // 발사 간격(speed)이 지나면 타이머 초기화 후 발사
        if (timer >= speed)
        {
            timer = 0f;
            Fire();
        }
    }

    // 총알 발사 메서드
    private void Fire()
    {
        Transform target = FindNearestTarget(); // 가장 가까운 적 찾기
        if (target == null) return; // 범위 안에 적이 없으면 발사하지 않음

        Vector3 dir = target.position - transform.position; // 나에서 적으로 향하는 방향
        dir.y = 0f; // 높이 차이는 무시 (수평으로만 발사)
        if (dir == Vector3.zero) return;    // 적과 위치가 겹치면 방향을 정할 수 없으므로 발사하지 않음
        dir.Normalize();    // 방향만 남기고 길이를 1로 맞춤

        // 발사 수만큼 총알 생성
        for (int i = 0; i < count; i++)
        {
            // 가운데를 기준으로 좌우로 퍼지게 각도 계산 (예: 3발이면 -15, 0, 15도)
            float angle = spreadAngle * (i - (count - 1) / 2f);
            Vector3 shotDir = Quaternion.Euler(0, angle, 0) * dir;  // 기본 방향을 Y축 기준으로 angle만큼 회전

            Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;  // 풀에서 총알 꺼내기
            bullet.position = transform.position + Vector3.up * 1f; // 플레이어 위치보다 조금 위에서 발사

            bullet.GetComponent<Bullet>().Initialize(damage, per, shotDir); // 총알 데미지, 관통 횟수, 방향 설정
        }
    }

    // 범위 안에서 가장 가까운 적을 찾아 반환하는 메서드
    private Transform FindNearestTarget()
    {
        // 내 위치를 중심으로 scanRange 반지름 안에 있는 targetLayer 콜라이더를 모두 가져옴
        Collider[] hits = Physics.OverlapSphere(transform.position, scanRange, targetLayer);

        Transform nearest = null;   // 가장 가까운 적 (없으면 null 반환)
        float minDist = Mathf.Infinity; // 지금까지 찾은 가장 가까운 거리 (처음엔 무한대로 설정)

        // 감지된 적들을 하나씩 확인
        foreach (Collider hit in hits)
        {
            float dist = Vector3.Distance(transform.position, hit.transform.position);  // 나와 적 사이의 거리

            // 지금까지 찾은 거리보다 더 가까우면 갱신
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest; // 가장 가까운 적 반환
    }
}
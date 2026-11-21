using UnityEngine;

// 범위 안의 적에게 화염병을 던지는 무기
// 화염병은 포물선으로 날아가고, 떨어진 자리에 일정 시간 동안 데미지를 주는 불장판을 생성
public class MolotovWeapon : WeaponBase
{
    public float scanRange = 8f;    // 적 탐색 범위 (반지름)
    public LayerMask targetLayer;   // 탐색할 대상의 레이어 (적 레이어만 감지)

    [Header("포물선")]
    public float flightTime = 0.8f; // 날아가는 시간
    public float arcHeight = 3f;    // 최고 높이

    [Header("불장판")]
    public int fireZonePrefabId;    // PoolManager에서 불장판 번호
    public float fireDuration = 3f; // 불장판 지속 시간
    public float fireRadius = 1.5f; // 불장판 범위 (반지름)

    private float timer;    // 다음 투척까지 시간 측정용

    // 컴포넌트를 처음 붙이거나 Reset 했을 때 인스펙터 기본값 설정
    private void Reset()
    {
        speed = 2f;     // 던지는 간격(초)
        count = 1;      // 한 번에 몇 개 던질지
    }

    // 화염병 무기는 시작할 때 따로 준비할 것이 없으므로 비워둠
    public override void Initialize() { }

    // 매 프레임 투척 타이밍 확인
    protected override void UpdateWeapon()
    {
        timer += Time.deltaTime;

        // 투척 간격(speed)이 지나면 타이머 초기화 후 던지기
        if (timer >= speed)
        {
            timer = 0f;
            Throw();
        }
    }

    // 화염병 투척 메서드
    private void Throw()
    {
        // 내 위치를 중심으로 scanRange 반지름 안에 있는 적을 모두 가져옴
        Collider[] hits = Physics.OverlapSphere(transform.position, scanRange, targetLayer);
        if (hits.Length == 0) return;   // 범위 안에 적이 없으면 던지지 않음

        // 투척 개수만큼 화염병 생성
        for (int i = 0; i < count; i++)
        {
            // 범위 안 적 중 랜덤으로 하나 골라서 던지기
            Vector3 targetPos = hits[Random.Range(0, hits.Length)].transform.position;
            targetPos.y = transform.position.y; // 땅 높이에 떨어지게

            Vector3 startPos = transform.position + Vector3.up * 1f;    // 플레이어 위치보다 조금 위에서 출발

            Transform bottle = GameManager.instance.poolManager.Get(prefabId).transform;  // 풀에서 화염병 꺼내기
            bottle.GetComponent<MolotovProjectile>().Launch(startPos, targetPos, flightTime, arcHeight, this);  // 출발/도착 위치, 비행 시간, 높이, 던진 무기 전달
        }
    }

    // 불장판 생성 메서드 (화염병이 땅에 닿으면 MolotovProjectile에서 호출)
    public void SpawnFire(Vector3 position)
    {
        Transform fire = GameManager.instance.poolManager.Get(fireZonePrefabId).transform;    // 풀에서 불장판 꺼내기
        fire.position = position;   // 화염병이 떨어진 위치에 배치
        fire.GetComponent<FireZone>().Initialize(damage, fireDuration, fireRadius, targetLayer);    // 데미지, 지속 시간, 범위, 대상 레이어 전달
    }

    // 레벨업 시 데미지/개수 증가 + 불장판 범위와 지속 시간 증가
    public override void LevelUp(float damage, int count)
    {
        base.LevelUp(damage, count);    // 부모 기능 실행 (데미지, 개수 증가)
        fireRadius += 0.3f;     // 불장판 범위 증가
        fireDuration += 0.5f;   // 지속 시간 증가
    }
}
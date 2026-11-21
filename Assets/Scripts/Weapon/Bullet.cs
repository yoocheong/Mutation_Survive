using UnityEngine;

// 원거리 무기에서 발사되는 총알 (적에게 데미지를 주고, 관통 횟수만큼 뚫고 지나감)
public class Bullet : MonoBehaviour
{
    public float damage;        // 총알 데미지
    public int pierceCount;     // 남은 관통 횟수 (0이면 한 마리 맞히고 사라짐)
    public float lifeTime = 3f; // 아무것도 안 맞으면 몇 초 뒤 사라질지

    private Quaternion originRotation;  // 프리팹의 원래 회전값 (모델 방향 보정용)
    private Rigidbody rigid;    // 총알 이동에 사용하는 리지드바디
    private float lifeTimer;    // 발사 후 지난 시간 측정용

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        originRotation = transform.rotation;    // 프리팹 원래 회전 저장
    }

    // 총알 초기화 메서드 (무기에서 발사할 때 호출)
    // 풀에서 재사용되므로 발사할 때마다 값을 새로 설정
    public void Initialize(float damage, int pierceCount, Vector3 dir)
    {
        this.damage = damage;
        this.pierceCount = pierceCount;
        transform.rotation = Quaternion.LookRotation(dir) * originRotation; // 날아가는 방향을 바라보도록 회전 (원래 회전값 유지)
        rigid.linearVelocity = dir * 15f;   // 발사 방향으로 속도 부여
        lifeTimer = 0;  // 수명 타이머 초기화
    }

    private void Update()
    {
        if (!rigid) return; // 리지드바디가 없으면 처리하지 않음

        lifeTimer += Time.deltaTime;

        // 수명이 다 되면 비활성화 (화면 밖으로 날아간 총알이 계속 쌓이는 것 방지)
        if (lifeTimer >= lifeTime)
            Deactivate();
    }

    // 다른 콜라이더와 겹쳤을 때 호출
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") || !rigid) return;   // 적이 아니면 무시 (플레이어, 벽 등)


        EnemyHealth enemy = other.GetComponent<EnemyHealth>();  // 맞은 적의 체력 컴포넌트 가져오기
        if (!enemy || !enemy.IsLive) return;   // 없거나 이미 죽은 적이면 무시
        enemy.TakeDamage(damage);   // 적에게 데미지
        pierceCount--;  // 관통 1 소모

        // 관통 횟수를 다 쓰면 비활성화
        if (pierceCount < 0)
            Deactivate();
    }

    // 총알 비활성화 메서드 (오브젝트 풀 재사용을 위해 파괴하지 않고 비활성화)
    private void Deactivate()
    {
        rigid.linearVelocity = Vector3.zero;    // 다음에 꺼낼 때 이전 속도가 남지 않도록 초기화
        gameObject.SetActive(false);
    }
}
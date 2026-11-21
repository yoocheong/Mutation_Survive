using UnityEngine;

// EnemyHealth의 사망 이벤트를 구독해서 동작
[RequireComponent(typeof(EnemyHealth))]     // EnemyHealth가 반드시 같이 붙어 있어야 함
public class ExpDrop : MonoBehaviour
{
    [SerializeField] private int gemPrefabId;       // PoolManager에서 보석 프리팹 번호
    [SerializeField] private int expAmount = 1;     // 보석이 주는 경험치 양

    private EnemyHealth health;     // 같은 오브젝트의 체력 컴포넌트 (사망 이벤트 구독용)

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();   // 처음 생성될 때 한 번만 가져옴
    }

    // 활성화될 때 사망 이벤트 구독
    private void OnEnable()
    {
        health.OnDead += Drop;
    }

    // 비활성화될 때 구독 해제 (풀 재사용 시 구독이 중복으로 쌓이는 것 방지)
    private void OnDisable()
    {
        health.OnDead -= Drop;
    }

    // 경험치 보석 드랍 메서드 (적이 죽으면 OnDead 이벤트로 호출됨)
    private void Drop(EnemyHealth dead)     // 매개변수로 죽은 적을 받는다. (이벤트 형식에 맞추기 위함)
    {
        GameObject gem = GameManager.instance.poolManager.Get(gemPrefabId);     // 풀에서 보석 꺼내기
        gem.transform.position = transform.position;    // 적이 죽은 위치에 배치
        gem.GetComponent<ExpGem>().Initialize(expAmount);   // 보석이 줄 경험치 설정
    }
}
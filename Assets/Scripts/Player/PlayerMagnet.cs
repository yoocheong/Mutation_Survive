using UnityEngine;

// 일정 범위 안의 경험치 보석을 플레이어 쪽으로 끌어당기는 자석
[RequireComponent(typeof(SphereCollider))]
public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private float range = 3f;  // 자석 범위 (반지름)

    private SphereCollider magnetCollider;  // 범위 감지용 원형 콜라이더

    // 현재 자석 범위를 반환하는 읽기 전용 프로퍼티 (LevelUpUI에서 범위 증가할 때 사용)
    public float Range => range;

    private void Awake()
    {
        magnetCollider = GetComponent<SphereCollider>();
        magnetCollider.isTrigger = true;    // 밀어내지 않고 감지만 하도록 트리거로 설정
        magnetCollider.radius = range;      // 콜라이더 크기를 자석 범위로 설정
    }

    // 자석 범위 변경 메서드 (레벨업 강화에서 호출)
    public void SetRange(float newRange)    // 매개변수로 새 범위를 받는다.
    {
        range = newRange;
        magnetCollider.radius = range;  // 콜라이더 크기도 같이 변경
    }

    // 범위 안에 무언가 들어왔을 때 호출
    private void OnTriggerEnter(Collider other)
    {
        // 들어온 게 경험치 보석이면 플레이어 쪽으로 끌어당김
        if (other.TryGetComponent(out ExpGem gem))
        {
            gem.StartMagnet(transform.parent);  // 부모(플레이어)를 목표로 넘김
        }
    }
}

using UnityEngine;

// 주변의 적을 탐색해서 가장 가까운 적을 찾는 클래스
public class Scanner : MonoBehaviour
{
    public float scanRange;     // 탐색 범위 (반지름)
    public LayerMask targetLayer;   // 탐색할 대상의 레이어 (적 레이어만 감지)
    public Collider[] targets;  // 탐색 범위 안에서 감지된 대상들을 저장하는 배열
    public Transform nearestTarget; // 감지된 대상 중 가장 가까운 대상

    // 물리 업데이트 주기마다 주변 대상 탐색
    private void FixedUpdate()
    {
        // 내 위치를 중심으로 scanRange 반지름 안에 있는 targetLayer 콜라이더를 모두 가져옴
        targets = Physics.OverlapSphere(this.transform.position, scanRange, targetLayer);
        nearestTarget = GetNearest();   // 감지된 대상 중 가장 가까운 대상 저장
    }

    // 가장 가까운 대상을 찾아 반환하는 메서드
    private Transform GetNearest()
    {
        Transform result = null;    // 가장 가까운 대상을 저장할 지역변수 (없으면 null 반환)
        float minDistance = 100f;   // 지금까지 찾은 가장 가까운 거리 (처음엔 큰 값으로 설정)

        // 감지된 대상들을 하나씩 확인
        foreach (Collider target in targets)
        {
            Vector3 myPosition = this.transform.position;   // 내 위치
            Vector3 targetPosition = target.transform.position; // 대상 위치

            float distance = Vector3.Distance(myPosition, targetPosition);  // 나와 대상 사이의 거리

            // 지금까지 찾은 거리보다 더 가까우면 최소 거리와 결과 갱신
            if (distance < minDistance)
            {
                minDistance = distance;
                result = target.transform;
            }
        }

        return result;  // 가장 가까운 대상 반환
    }
}
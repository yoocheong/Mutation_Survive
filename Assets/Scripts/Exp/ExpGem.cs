using UnityEngine;

public class ExpGem : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // 플레이어에게 날아가는 속도

    private int exp;            // 이 보석이 주는 경험치
    private Transform target;   // 날아갈 목표 (null이면 가만히 있음)

    // 풀에서 다시 꺼낼 때마다 목표 초기화 (이전 목표를 따라가지 않도록)
    private void OnEnable()
    {
        target = null;
    }

    // 경험치 양 설정 메서드 (ExpDrop에서 호출)
    public void Initialize(int exp)
    {
        this.exp = exp;
    }

    // 자석 범위에 들어왔을 때 목표 설정 (PlayerMagnet에서 호출)
    public void StartMagnet(Transform player)
    {
        target = player;
    }

    private void Update()
    {
        if (target == null) return; // 목표가 없으면 이동하지 않음

        // 목표 위치로 일정 속도로 이동
        transform.position = Vector3.MoveTowards(
            transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    // 플레이어 본체에 닿으면 경험치 획득
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;    // 플레이어가 아니면 무시

        GameManager.instance.GetExp(exp);   // 경험치 추가
        gameObject.SetActive(false);        // 풀로 반환
    }
}

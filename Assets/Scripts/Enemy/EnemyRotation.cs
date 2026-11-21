using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;  // 회전 속도 (클수록 빠르게 돌아봄)
    private Transform playerTransform;  // 바라볼 대상 (플레이어)

    // 처음 생성될 때 플레이어 위치 정보를 한 번만 가져옴
    private void Awake()
    {
        playerTransform = GameManager.instance.playerMovement.transform;
    }

    // 매 프레임 플레이어 방향으로 회전
    private void Update()
    {
        LookAtPlayer();
    }

    // 플레이어 방향으로 부드럽게 회전하는 메서드
    private void LookAtPlayer()
    {
        Vector3 direction = playerTransform.position - this.transform.position; // 나에서 플레이어로 향하는 방향
        direction.y = 0f;   // 높이 차이는 무시 (위아래로 기울지 않고 수평으로만 회전)

        if (direction == Vector3.zero) return;  // 플레이어와 위치가 겹치면 방향을 정할 수 없으므로 회전하지 않음

        Quaternion targetRotation = Quaternion.LookRotation(direction); // 플레이어를 바라보는 회전값
        // 현재 회전에서 목표 회전으로 조금씩 회전 (부드러운 회전)
        transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 
    }
}
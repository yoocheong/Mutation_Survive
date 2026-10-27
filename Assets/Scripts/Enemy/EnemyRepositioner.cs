using UnityEngine;

public class EnemyRepositioner : MonoBehaviour
{
    [SerializeField] private float repositionDistance = 20f;  // 재배치 범위(거리)
    [SerializeField] private float distance = 30f;  // 재배치 시 플레이어 이동 방향 * distance만큼 이동
    [SerializeField] private float randomRange = 5f;  // 재배치 시 랜덤한 x, z 좌표 범위
    private Transform playerTransform;  // 플레이어 Transform
    private Transform enemyTransform;    // 적 Transform
    private PlayerMovement playerMovement;  // playerMovement 스크립트

    private void Start()
    {
        playerMovement = GameManager.instance.playerMovement;
        playerTransform = playerMovement.transform;

        enemyTransform = GetComponent<Transform>();      // 적 Transform 참조
    }

    private void Update()
    {
        RepositionEnemy();
    }

    //  적 재배치 하는 함수
    private void RepositionEnemy()
    {
        float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);    // 적과 플레이어 사이의 거리

        // 적과 플레이어 사이의 거리가 distance 보다 이상이면 적 재배치
        if (distanceToPlayer <= repositionDistance) return;

        Vector3 randomOffset = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));   // 랜덤한 x, z 좌표
        // 플레이어 이동 방향 * distance만큼 이동시키고 랜덤한 x, z 좌표를 더해서 적 재배치
        Vector3 newPosition = playerTransform.position + playerMovement.direction * distance + randomOffset; 

        enemyTransform.position = newPosition;   // 재배치
    }
}
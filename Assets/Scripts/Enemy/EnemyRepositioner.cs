using UnityEngine;

public class EnemyRepositioner : MonoBehaviour
{
    [SerializeField] private float repositionDistance = 20f;  // 재배치 범위(거리)
    [SerializeField] private float distance = 30f;  // 재배치 시 플레이어 이동 방향 * distance만큼 이동
    [SerializeField] private float randomRange = 5f;  // 재배치 시 랜덤한 x, z 좌표 범위
    private Transform playerTransform;  // 플레이어 Transform
    private Transform transform;    // 이 스크립트가 붙은 오브젝트(적)의 Transform
    private PlayerMovement playerMovement;  // playerMovement 스크립트

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<Transform>();     // Player 트랜스폼 값 참조
        playerMovement = GameObject.FindGameObjectWithTag(Constants.Tags.Player).GetComponent<PlayerMovement>();     // PlayerMovement 스크립트 참조

        transform = GetComponent<Transform>();      // 이 스크립트가 붙은 오브젝트(적)의 Transform 참조
    }

    private void Update()
    {
        RepositionEnemy();
    }

    //  이 스크립트가 붙은 오브젝트(적) 재배치 하는 함수
    private void RepositionEnemy()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);    // 적과 플레이어 사이의 거리

        // 적과 플레이어 사이의 거리가 distance 보다 이상이면 적 재배치
        if (distanceToPlayer <= repositionDistance) return;

        Vector3 randomOffset = new Vector3(Random.Range(-randomRange, randomRange), 0, Random.Range(-randomRange, randomRange));   // 랜덤한 x, z 좌표
        Vector3 newPosition = playerTransform.position + playerMovement.direction * distance + randomOffset;   // 플레이어 이동 방향 * distance만큼 이동시키고 랜덤한 x, z 좌표를 더해서 적 재배치

        transform.position = newPosition;   // 재배치
    }
}
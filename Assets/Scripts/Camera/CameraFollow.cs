using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;  // 플레이어 위치
    private Vector3 offSet; // 카메라 offSet

    private void Awake()
    {
        offSet = new Vector3(0f, 9f, -5f);  // 카메라 offSet 초기화
    }

    private void LateUpdate()
    {
        Follow();
    }

    // 플레이어 따라다니는 함수
    private void Follow()
    {
        transform.position = new Vector3(
            player.position.x + offSet.x,
            offSet.y,
            player.position.z + offSet.z);
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;  // 플레이어 위치
    [SerializeField] private Vector3 offSet;    // 카메라 offSet

    private void Awake()
    {
        offSet = new Vector3(0f, 11.5f, -8.5f); // offSet 설정
    }

    // 플레이어 따라다니는 코드
    private void LateUpdate()
    {
        transform.position = new Vector3(
            player.position.x + offSet.x,
            offSet.y,
            player.position.z + offSet.z);
    }
}

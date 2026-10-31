using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;  // 플레이어 Transform
    private Vector3 offSet; // 카메라 offSet

    private void Awake()
    {
        offSet = new Vector3(0f, 12f, -5f);  // 카메라 offSet 초기화
    }

    private void Start()
    {
        player = GameManager.instance.playerMovement.transform; // 플레이어 Transform 변수에 저장
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    // 플레이어 따라다니는 메서드
    private void FollowPlayer()
    {
        if (!player) return;

        transform.position = new Vector3(
            player.position.x + offSet.x,
            offSet.y,
            player.position.z + offSet.z);
    }
}
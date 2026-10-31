using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    [SerializeField] private float speed = 5.0f;    // 이동 속도
    [SerializeField] private float rotSpeed = 10.0f;    // 회전 속도

    public Vector3 direction = Vector3.zero;   // 방향 벡터 0,0,0 으로 설정

    private void Awake()
    {
        playerRigidbody = this.GetComponent<Rigidbody>();  // Rigidbody 컴포넌트 가져오기
    }

    // 매 프레임 마다 입력 처리
    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        PlayerRotation();
        MovePlayer();
    }

    // 플레이어 회전 메서드
    private void PlayerRotation()
    {
        if (direction != Vector3.zero)  // 입력이 있을 때만 회전
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);  // 회전할 방향 정함
            Quaternion newRot = Quaternion.Slerp(playerRigidbody.rotation, targetRot, rotSpeed * Time.fixedDeltaTime);    // targetRot 까지 얼마나 회전할지 정함
            playerRigidbody.MoveRotation(newRot); // 회전 적용
        }
    }

    // 플레이어 이동 메서드
    private void MovePlayer()
    {
        Vector3 moveAmount = direction * speed * Time.fixedDeltaTime;   // 물리 프레임마다 이동량
        playerRigidbody.MovePosition(playerRigidbody.position + moveAmount);   // 이동 적용
    }

    // 입력 처리하는 메서드
    private void HandleInput()
    {
        direction.x = Input.GetAxisRaw(Constants.Input.InputHorizontal);
        direction.z = Input.GetAxisRaw(Constants.Input.InputVertical);
        direction.Normalize();  // 벡터 정규화
    }

}


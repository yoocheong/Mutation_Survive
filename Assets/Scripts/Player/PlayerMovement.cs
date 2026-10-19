using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float speed = 5.0f;    // 이동 속도 정하는 변수
    [SerializeField] private float rotSpeed = 10.0f;    // 회전 속도 정하는 변수

    private Vector3 direction = Vector3.zero;   // 방향 벡터 0,0,0 으로 설정

    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();  // Rigidbody 컴포넌트 가져오기
    }

    // 매 프레임 마다 입력 처리
    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (direction != Vector3.zero)  // 입력이 있을 때만 회전
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);  // 바라볼 방향 정하는 코드
            Quaternion newRot = Quaternion.Slerp(rigidbody.rotation, targetRot, rotSpeed * Time.fixedDeltaTime);    // targetRot 까지 얼마나 회전할지 정하는 코드
            rigidbody.MoveRotation(newRot); // 회전 적용
        }

        rigidbody.MovePosition(rigidbody.position + direction * speed * Time.fixedDeltaTime);   // 이동 적용
    }

    // 입력 처리 하는 함수
    private void HandleInput()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();  // 벡터 정규화 하는 코드
    }

}

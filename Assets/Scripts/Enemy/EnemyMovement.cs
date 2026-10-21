using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;  // 적 이동 속도
    [SerializeField] private bool isAlive = true;   // 적 생존 여부
    private Rigidbody target;   // 추적할 플레이어 Rigidbody
    private Rigidbody rigidbody;    // 이 스크립트가 붙은 오브젝트의 Rigidbody

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();  // 이 스크립트가 붙은 오브젝트의 Rigidbody 컴포넌트를 변수에 저장
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그 붙어있는 오브젝트 찾아서 변수에 저장
        if (player != null) target = player.GetComponent<Rigidbody>();  // 추적할 플레이어 Rigidbody 컴포넌트를 변수에 저장
    }

    private void FixedUpdate()
    {
        Move(); // 물리 프레임마다 이동 처리
    }

    // 플레이어 추적하는 함수
    private void Move()
    {
        if (!isAlive || target == null) return; // 죽었거나 추적 대상이 널이면 이동 안함

        Vector3 direction = target.position - rigidbody.position;   // 방향
        Vector3 moveAmount = direction.normalized * speed * Time.fixedDeltaTime;    // 물리 프레임마다 이동량

        rigidbody.MovePosition(rigidbody.position + moveAmount);    // 이동 적용
    }

}

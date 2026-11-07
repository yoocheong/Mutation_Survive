using UnityEngine;
using static Spawner;

public class EnemyMovement : MonoBehaviour
{
    private float speed;  // 적 이동 속도
    private float hp;
    [SerializeField] private bool isAlive = true;   // 적 생존 여부
    private Rigidbody target;   // 추적할 플레이어 Rigidbody
    private Rigidbody enemyRigidbody;    // 적 Rigidbody

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody>();  // 적 Rigidbody 컴포넌트를 변수에 저장
    }

    private void Start()
    {
       target = GameManager.instance.playerMovement.GetComponent<Rigidbody>();  // 추적할 플레이어 Rigidbody 컴포넌트를 변수에 저장
    }

    private void FixedUpdate()
    {
        ChasePlayer(); // 물리 프레임마다 이동 처리
    }

    // 플레이어 추적하는 메서드
    private void ChasePlayer()
    {
        if (!isAlive || target == null) return; // 죽었거나 추적 대상이 널이면 이동 안함

        Vector3 direction = target.position - enemyRigidbody.position;   // 방향
        direction.y = 0;
        Vector3 moveAmount = direction.normalized * speed * Time.fixedDeltaTime;    // 물리 프레임마다 이동량

        enemyRigidbody.MovePosition(enemyRigidbody.position + moveAmount);    // 이동 적용
    }

    // 스폰될 때 SpawnData의 값으로 적 이동속도, 체력 초기화
    public void Initialize(SpawnData spawnData)
    {
        speed = spawnData.speed;
        hp = spawnData.hp;
    }

    // 피격 처리
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Constants.Tags.Bullet)) return;   // 무기 아니면 무시

        hp -= other.GetComponent<Bullet>().damage;  // 무기 데미지 만큼 무시

        if (hp > 0)
        {
            // 살아있을 때 피격 처리 (피격 애니메이션, 넉백 등 추가 예정)
        }
        else
        {
            Dead(); // 체력이 0 이하면 Dead()함수 호출
        }
    }

    // 적 사망 메서드 (오브젝트 풀 재사용을 위해 파괴하지 않고 비활성화)
    private void Dead()
    {
        this.gameObject.SetActive(false);
    }

}
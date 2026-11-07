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

    public void Initialize(SpawnData spawnData)
    {
        speed = spawnData.speed;
        hp = spawnData.hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Constants.Tags.Bullet)) return;

        hp -= other.GetComponent<Bullet>().damage;

        if (hp > 0)
        {

        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        this.gameObject.SetActive(false);
    }

}

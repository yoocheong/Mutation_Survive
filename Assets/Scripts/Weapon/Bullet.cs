using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int pierceCount;       // 남은 관통 횟수 (int로 변경)
    public float lifeTime = 3f;   // 아무것도 안 맞으면 몇 초 뒤 사라질지

    private Quaternion originRotation;
    private Rigidbody rigid;
    private float lifeTimer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        originRotation = transform.rotation;    // 프리팹 원래 회전 저장
    }

    public void Initialize(float damage, int pierceCount, Vector3 dir)
    {
        this.damage = damage;
        this.pierceCount = pierceCount;
        transform.rotation = Quaternion.LookRotation(dir) * originRotation;
        rigid.linearVelocity = dir * 15f;
        lifeTimer = 0;
    }

    private void Update()
    {
        if (!rigid) return;

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
            Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") || !rigid) return;

        pierceCount--;                          // 관통 1 소모 (per → pierceCount)

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (!enemy) return;
        enemy.TakeDamage(damage);

        if (pierceCount < 0)
            Deactivate();
    }

    private void Deactivate()
    {
        rigid.linearVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
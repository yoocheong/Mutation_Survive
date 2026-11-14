using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage;
    public int pierceCount;       // 남은 관통 횟수 (int로 변경)

    public void Initialize(float damage, int pierceCount)
    {
        this.damage = damage;
        this.pierceCount = pierceCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        pierceCount--;                          // 관통 1 소모 (per → pierceCount)

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (!enemy) return;
        enemy.TakeDamage(damage);
    }

}

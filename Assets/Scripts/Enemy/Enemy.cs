using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMovement movement;
    private EnemyHealth health;
    private EnemyAttack attack;

    private void Awake()   // GetComponent는 처음 한 번만
    {
        movement = GetComponent<EnemyMovement>();
        health = GetComponent<EnemyHealth>();
        attack = GetComponent<EnemyAttack>();
    }

    public void Initialize(Spawner.SpawnData data)
    {
        movement.Initialize(data.speed);
        health.Initialize(data.hp);
        attack.Initialize(data.damage);
    }
}
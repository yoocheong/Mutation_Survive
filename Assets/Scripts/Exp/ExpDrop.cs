using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class ExpDrop : MonoBehaviour
{
    [SerializeField] private int gemPrefabId;    // PoolManager에서 보석 프리팹 번호
    [SerializeField] private int expAmount = 1;

    private EnemyHealth health;

    private void Awake() 
    {
        health = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    { 
        health.OnDead += Drop;
    }

    private void OnDisable()
    {
        health.OnDead -= Drop; 
    }

    private void Drop(EnemyHealth dead)
    {
        GameObject gem = GameManager.instance.poolManager.Get(gemPrefabId);
        gem.transform.position = transform.position;
        gem.GetComponent<ExpGem>().Initialize(expAmount);
    }
}
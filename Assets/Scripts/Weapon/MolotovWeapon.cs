using UnityEngine;

public class MolotovWeapon : WeaponBase
{
    public float scanRange = 8f;
    public LayerMask targetLayer;

    [Header("포물선")]
    public float flightTime = 0.8f;   // 날아가는 시간
    public float arcHeight = 3f;      // 최고 높이

    [Header("불장판")]
    public int fireZonePrefabId;      // PoolManager에서 불장판 번호
    public float fireDuration = 3f;
    public float fireRadius = 1.5f;

    private float timer;

    private void Reset()
    {
        speed = 2f;   // 던지는 간격(초)
        count = 1;    // 한 번에 몇 개 던질지
    }

    public override void Initialize() { }

    protected override void UpdateWeapon()
    {
        timer += Time.deltaTime;
        if (timer >= speed)
        {
            timer = 0f;
            Throw();
        }
    }

    private void Throw()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, scanRange, targetLayer);
        if (hits.Length == 0) return;

        for (int i = 0; i < count; i++)
        {
            // 범위 안 적 중 랜덤으로 하나 골라서 던지기
            Vector3 targetPos = hits[Random.Range(0, hits.Length)].transform.position;
            targetPos.y = transform.position.y;   // 땅 높이에 떨어지게

            Vector3 startPos = transform.position + Vector3.up * 1f;

            Transform bottle = GameManager.instance.poolManager.Get(prefabId).transform;
            bottle.GetComponent<MolotovProjectile>().Launch(startPos, targetPos, flightTime, arcHeight, this);
        }
    }

    // 병이 땅에 닿으면 호출됨
    public void SpawnFire(Vector3 position)
    {
        Transform fire = GameManager.instance.poolManager.Get(fireZonePrefabId).transform;
        fire.position = position;
        fire.GetComponent<FireZone>().Initialize(damage, fireDuration, fireRadius, targetLayer);
    }

    public override void LevelUp(float damage, int count)
    {
        base.LevelUp(damage, count);
        fireRadius += 0.3f;      // 불장판 범위 증가
        fireDuration += 0.5f;    // 지속 시간 증가
    }
}
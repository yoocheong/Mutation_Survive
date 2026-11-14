using UnityEngine;

public class RangedWeapon : WeaponBase
{
    public float scanRange = 10f;
    public LayerMask targetLayer;

    public int per = 0;              // 관통 횟수 (따로 분리)
    public float spreadAngle = 15f;  // 총알 사이 각도

    public Vector3 bulletRotationOffset = new Vector3(90f, 0f, 0f);

    private float timer;

    private void Reset()
    {
        speed = 0.5f;   // 발사 간격(초)
        count = 1;      // 처음엔 1발
    }

    public override void Initialize() { }

    public override void LevelUp(float damage, int count)
    {
        Debug.Log("d");
        base.LevelUp(damage, count);   // 부모 기능 실행 (데미지, 발사 수 증가)
        per += 1;                      // 원거리 무기만 관통 +1
    }

    protected override void UpdateWeapon()
    {
        timer += Time.deltaTime;

        if (timer >= speed)
        {
            timer = 0f;
            Fire();
        }
    }

    private void Fire()
    {
        Transform target = FindNearestTarget();
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        if (dir == Vector3.zero) return;
        dir.Normalize();

        for (int i = 0; i < count; i++)
        {
            // 가운데를 기준으로 좌우로 퍼지게
            float angle = spreadAngle * (i - (count - 1) / 2f);
            Vector3 shotDir = Quaternion.Euler(0, angle, 0) * dir;

            Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
            bullet.position = transform.position + Vector3.up * 1f;

            bullet.GetComponent<Bullet>().Initialize(damage, per, shotDir);
        }
    }

    private Transform FindNearestTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, scanRange, targetLayer);

        Transform nearest = null;
        float minDist = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }
}
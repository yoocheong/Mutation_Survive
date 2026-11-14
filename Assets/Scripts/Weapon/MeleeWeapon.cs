using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    public float radius = 1.7f;
    private float currentAngle;

    private void Reset()
    {
        speed = -150f;   // 인스펙터 기본값
    }

    public override void Initialize()
    {
        ArrangeWeapons();
    }

    protected override void UpdateWeapon()
    {
        currentAngle += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);
    }

    public override void LevelUp(float damage, int count)
    {
        base.LevelUp(damage, count);
        ArrangeWeapons();
    }

    private void ArrangeWeapons()
    {
        for (int index = 0; index < count; index++)
        {
            Transform sword;

            if (index < this.transform.childCount)
            {
                sword = this.transform.GetChild(index);
            }
            else
            {
                sword = GameManager.instance.poolManager.Get(prefabId).transform;
                sword.parent = this.transform;
            }

            float angle = 360f * index / count;
            Quaternion rot = Quaternion.Euler(0, angle, 0);   // Y축 기준 각도

            sword.localPosition = rot * Vector3.forward * radius + Vector3.up * 1f;
            sword.localRotation = rot * Quaternion.Euler(90, 0, 0);

            sword.GetComponent<Sword>().Initialize(damage, -1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (!enemy) return;
        enemy.TakeDamage(damage);
    }
}
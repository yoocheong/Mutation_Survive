using UnityEngine;

public class FireZone : MonoBehaviour
{
    public float tickInterval = 0.5f;   // 몇 초마다 데미지 줄지

    private float damage;
    private float duration;
    private float radius;
    private LayerMask targetLayer;

    private float lifeTimer;
    private float tickTimer;

    public void Initialize(float damage, float duration, float radius, LayerMask targetLayer)
    {
        this.damage = damage;
        this.duration = duration;
        this.radius = radius;
        this.targetLayer = targetLayer;

        lifeTimer = 0f;
        tickTimer = 0f;

        // 범위에 맞게 크기 조절 (기본 Cylinder 지름이 1이라 반지름 × 2)
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(radius * 2f, scale.y, radius * 2f);
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= duration)
        {
            gameObject.SetActive(false);
            return;
        }

        tickTimer += Time.deltaTime;
        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;
            DealDamage();
        }
    }

    private void DealDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, targetLayer);

        foreach (Collider hit in hits)
        {
            // 적 데미지 코드 따로 만들 예정
        }
    }
}
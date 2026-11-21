using UnityEngine;

// 플레이어 주위를 회전하는 근접 무기 (검)
// 검들을 원형으로 배치하고, 이 오브젝트를 회전시켜 검 전체가 함께 돌게 함
// 데미지는 각 검에 붙은 Sword 스크립트가 처리
public class MeleeWeapon : WeaponBase
{
    public float radius = 1.7f; // 플레이어로부터 검까지의 거리 (회전 반지름)
    private float currentAngle; // 현재 회전 각도

    // 컴포넌트를 처음 붙이거나 Reset 했을 때 인스펙터 기본값 설정
    private void Reset()
    {
        speed = -150f;  // 회전 속도 (음수면 시계 방향)
    }

    // 무기 초기화 (시작할 때 검 배치)
    public override void Initialize()
    {
        ArrangeWeapons();
    }

    // 매 프레임 회전 각도를 증가시켜 검들을 회전
    protected override void UpdateWeapon()
    {
        currentAngle += speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);  // Y축 기준으로 회전
    }

    // 레벨업 시 데미지/개수 증가 후 검 다시 배치
    public override void LevelUp(float damage, int count)
    {
        base.LevelUp(damage, count);    // 부모 기능 실행 (데미지, 개수 증가)
        ArrangeWeapons();   // 늘어난 개수에 맞게 검 재배치
    }

    // 검을 개수만큼 원형으로 균등하게 배치하는 메서드
    private void ArrangeWeapons()
    {
        for (int index = 0; index < count; index++)
        {
            Transform sword;    // 배치할 검

            // 이미 만들어진 검이 있으면 재사용, 없으면 풀에서 새로 꺼냄
            if (index < this.transform.childCount)
            {
                sword = this.transform.GetChild(index);
            }
            else
            {
                sword = GameManager.instance.poolManager.Get(prefabId).transform;
                sword.parent = this.transform;  // 무기 오브젝트의 자식으로 설정 (같이 회전하도록)
            }

            float angle = 360f * index / count; // 검 사이 간격을 균등하게 나눈 각도
            Quaternion rot = Quaternion.Euler(0, angle, 0); // Y축 기준 각도

            sword.localPosition = rot * Vector3.forward * radius + Vector3.up * 1f; // 각도 방향으로 radius만큼 떨어진 위치 (높이 1)
            sword.localRotation = rot * Quaternion.Euler(90, 0, 0); // 바깥쪽을 향하도록 회전 (모델 방향 보정 90도)

            sword.GetComponent<Sword>().Initialize(damage, -1); // 검 데미지 설정 (-1 : 무한 관통)
        }
    }
}
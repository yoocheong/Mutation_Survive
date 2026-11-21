using UnityEngine;

// 모든 무기의 공통 기능을 담은 추상 클래스
// 근접, 원거리, 화염병 등 각 무기는 이 클래스를 상속받아 만듦
public abstract class WeaponBase : MonoBehaviour
{
    public int id;          // 무기 고유 번호 (레벨업 강화에서 어떤 무기인지 찾을 때 사용)
    public int prefabId;    // PoolManager에서 꺼낼 투사체/오브젝트 번호
    public int count;       // 무기 개수 또는 발사 수
    public float damage;    // 무기 데미지
    public float speed;     // 무기 속도 (무기마다 회전 속도, 발사 간격 등 의미가 다름)

    // 시작할 때 각 무기의 초기화 실행
    protected virtual void Start()
    {
        Initialize();
    }

    // 매 프레임 각 무기의 동작 실행
    protected virtual void Update()
    {
        UpdateWeapon();
    }

    // 무기 레벨업 메서드 (공통: 데미지와 개수 증가)
    // 무기마다 추가 효과가 필요하면 자식 클래스에서 override 후 base.LevelUp() 호출
    public virtual void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;
    }

    // 자식 클래스가 반드시 구현해야 하는 메서드
    public abstract void Initialize();      // 무기 초기 설정
    protected abstract void UpdateWeapon(); // 매 프레임 무기 동작 (공격, 발사 등)
}
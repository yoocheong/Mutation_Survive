using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public int id;
    public int prefabId;
    public int count;
    public float damage;
    public float speed;

    protected virtual void Start()
    {
        Initialize();
    }

    protected virtual void Update()
    {
        // 테스트용 레벨업
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp(3f, 1);
        }

        UpdateWeapon();
    }

    public virtual void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;
    }

    // 자식이 반드시 만들어야 하는 부분
    public abstract void Initialize();
    protected abstract void UpdateWeapon();
}
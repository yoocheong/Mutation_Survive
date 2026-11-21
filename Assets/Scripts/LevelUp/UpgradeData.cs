using UnityEngine;

// 강화 종류
public enum UpgradeType
{
    Weapon,         // 무기 강화 (데미지, 개수 증가)
    MagnetRange,    // 자석 범위 증가
    Heal            // 체력 회복
}

// 강화 하나의 정보를 담는 데이터 에셋
[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    [Header("표시 정보")]
    public string upgradeName;      // 버튼에 표시할 이름
    [TextArea] public string description;   // 버튼에 표시할 설명
    public Sprite icon;             // 버튼에 표시할 아이콘

    [Header("레벨")]
    public int maxLevel = 5;        // 최대 레벨 (이 레벨에 도달하면 더 이상 후보에 안 나옴)

    [Header("효과")]
    public UpgradeType type;        // 강화 종류
    public int weaponId;            // type이 Weapon일 때 강화할 무기의 id (WeaponBase.id와 같은 값)
    public float damage;            // 무기 데미지 증가량
    public int count;               // 무기 개수(발사 수) 증가량
    public float value;             // 자석 범위 증가량 / 회복량
}

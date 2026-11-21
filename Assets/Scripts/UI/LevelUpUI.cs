using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;          // 레벨업 창 전체 (켜고 끄는 용도)
    [SerializeField] private UpgradeButton[] buttons;   // 선택 버튼 3개

    [Header("강화 목록")]
    [SerializeField] private UpgradeData[] allUpgrades; // 뽑을 수 있는 강화들 (회복은 넣지 않음)
    [SerializeField] private UpgradeData healUpgrade;   // 강화가 부족할 때 대신 나오는 체력 회복

    [Header("강화 대상")]
    [SerializeField] private WeaponBase[] weapons;      // 플레이어 무기들
    [SerializeField] private PlayerMagnet magnet;       // 플레이어 자석
    [SerializeField] private PlayerHealth playerHealth; // 플레이어 체력

    // 강화별 현재 레벨 (몇 번 골랐는지) 저장
    // ScriptableObject에 직접 저장하면 플레이를 끝내도 값이 남으므로 여기서 따로 관리
    private Dictionary<UpgradeData, int> upgradeLevels = new Dictionary<UpgradeData, int>();

    private void Start()
    {
        panel.SetActive(false); // 시작할 땐 창 숨김

        // 모든 강화의 레벨을 0으로 초기화
        foreach (UpgradeData upgrade in allUpgrades)
        {
            upgradeLevels[upgrade] = 0;
        }

        // GameManager.instance는 Awake에서 설정되므로 Start에서 구독
        GameManager.instance.OnLevelUp += Show;
    }

    private void OnDestroy()
    {
        // 구독 해제 (GameManager가 먼저 파괴됐을 수도 있으니 null 확인)
        if (GameManager.instance != null)
        {
            GameManager.instance.OnLevelUp -= Show;
        }
    }

    // 레벨업 창 표시 (OnLevelUp 이벤트로 호출됨)
    private void Show(int level)
    {
        Debug.Log($"[LevelUpUI] {name} / 버튼 수: {buttons.Length} / 후보 수: {allUpgrades.Length}");
        // 만렙이 아닌 강화만 후보로 모음
        List<UpgradeData> candidates = new List<UpgradeData>();
        foreach (UpgradeData upgrade in allUpgrades)
        {
            if (upgradeLevels[upgrade] < upgrade.maxLevel)
            {
                candidates.Add(upgrade);
            }
        }

        Shuffle(candidates);    // 후보를 무작위로 섞음 → 앞에서부터 쓰면 서로 겹치지 않음

        bool healShown = false; // 회복 버튼은 한 번만 보여주기 위한 표시

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < candidates.Count)
            {
                // 남은 강화가 있으면 그대로 표시
                buttons[i].gameObject.SetActive(true);
                buttons[i].Setup(candidates[i], upgradeLevels[candidates[i]], this);
            }
            else if (!healShown)
            {
                // 강화가 모자라면 그 자리를 체력 회복으로 바꿈 (한 번만)
                buttons[i].gameObject.SetActive(true);
                buttons[i].Setup(healUpgrade, 0, this);
                healShown = true;
            }
            else
            {
                // 회복도 이미 나왔으면 남는 버튼은 숨김
                buttons[i].gameObject.SetActive(false);
            }
        }

        panel.SetActive(true);
        Time.timeScale = 0f;    // 게임 일시정지 (UI 버튼은 계속 동작함)
    }

    // 버튼에서 강화를 선택했을 때 호출
    public void Select(UpgradeData data)
    {
        Apply(data);    // 강화 효과 적용

        // 회복이 아닌 강화면 레벨 1 증가
        if (upgradeLevels.ContainsKey(data))
        {
            upgradeLevels[data]++;
        }

        panel.SetActive(false); // 창 닫기
        Time.timeScale = 1f;    // 게임 재개
    }

    // 강화 종류에 따라 효과 적용
    private void Apply(UpgradeData data)
    {
        switch (data.type)
        {
            case UpgradeType.Weapon:
                // id가 같은 무기를 찾아서 레벨업
                foreach (WeaponBase weapon in weapons)
                {
                    if (weapon.id == data.weaponId)
                    {
                        weapon.LevelUp(data.damage, data.count);
                        break;
                    }
                }
                break;

            case UpgradeType.MagnetRange:
                magnet.SetRange(magnet.Range + data.value); // 현재 범위에 증가량을 더함
                break;

            case UpgradeType.Heal:
                playerHealth.Heal(data.value);
                break;
        }
    }

    // 리스트 순서를 무작위로 섞는 메서드 (피셔-예이츠 셔플)
    private void Shuffle(List<UpgradeData> list)
    {
        // 뒤에서부터 하나씩, 자기 자신 포함 앞쪽 아무 칸과 자리를 바꿈
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);    // 튜플로 두 값 교환
        }
    }
}

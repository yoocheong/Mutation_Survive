using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Image icon;            // 아이콘 이미지
    [SerializeField] private TMP_Text nameText;     // 이름 텍스트
    [SerializeField] private TMP_Text descText;     // 설명 텍스트
    [SerializeField] private TMP_Text levelText;    // 레벨 텍스트 (예: Lv.2 → Lv.3)

    private Button button;          // 이 오브젝트의 버튼 컴포넌트
    private UpgradeData data;       // 이 버튼에 표시 중인 강화 정보
    private LevelUpUI levelUpUI;    // 클릭했을 때 알려줄 레벨업 창

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);    // 버튼을 누르면 OnClick 실행
    }

    // 버튼에 표시할 강화 정보 설정 (LevelUpUI에서 호출)
    // currentLevel : 지금까지 이 강화를 몇 번 골랐는지
    public void Setup(UpgradeData data, int currentLevel, LevelUpUI levelUpUI)
    {
        Debug.Log($"[Setup] {name}에 {data.upgradeName} 설정");
        this.data = data;
        this.levelUpUI = levelUpUI;

        // 아이콘 이미지가 연결돼 있고, 강화 에셋에 아이콘이 있을 때만 표시
        if (icon != null)
        {
            icon.sprite = data.icon;
            icon.enabled = data.icon != null;
        }
        nameText.text = data.upgradeName;
        descText.text = data.description;

        // 레벨 표시 (회복은 레벨이 없으므로 비움)
        if (data.type == UpgradeType.Heal)
        {
            levelText.text = "";
        }
        else if (currentLevel == 0)
        {
            levelText.text = "NEW";     // 처음 고르는 강화
        }
        else
        {
            levelText.text = $"Lv.{currentLevel} → Lv.{currentLevel + 1}";
        }
    }

    // 버튼 클릭 시 레벨업 창에 선택한 강화를 알려줌
    private void OnClick()
    {
        levelUpUI.Select(data);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;       // 차오르는 게이지 이미지 (Image Type: Filled)
    [SerializeField] private TMP_Text levelText;    // 레벨 표시 텍스트
    [SerializeField] private float fillSpeed = 5f;  // 게이지가 차오르는 속도

    private float targetFill;   // 게이지가 도달해야 할 목표 비율 (0 ~ 1)

    private void Start()
    {
        // GameManager.instance는 Awake에서 설정되므로 Start에서 구독
        GameManager.instance.OnExpChanged += UpdateExp;
        GameManager.instance.OnLevelUp += UpdateLevel;

        // 시작할 때 현재 값으로 한 번 갱신
        UpdateExp(GameManager.instance.exp, GameManager.instance.NextExp);
        UpdateLevel(GameManager.instance.level);
        fillImage.fillAmount = targetFill;  // 처음엔 애니메이션 없이 바로 맞춤
    }

    private void OnDestroy()
    {
        // 구독 해제 (GameManager가 먼저 파괴됐을 수도 있으니 null 확인)
        if (GameManager.instance != null)
        {
            GameManager.instance.OnExpChanged -= UpdateExp;
            GameManager.instance.OnLevelUp -= UpdateLevel;
        }
    }

    private void Update()
    {
        // 현재 게이지를 목표 비율까지 부드럽게 이동
        // 레벨업 창이 떠서 timeScale이 0이어도 움직이도록 unscaledDeltaTime 사용
        fillImage.fillAmount = Mathf.MoveTowards(
            fillImage.fillAmount, targetFill, fillSpeed * Time.unscaledDeltaTime);
    }

    // 경험치가 바뀌었을 때 호출 → 목표 비율 계산
    private void UpdateExp(int exp, int nextExp)
    {
        targetFill = (float)exp / nextExp;  // int끼리 나누면 0이 되므로 float으로 변환
    }

    // 레벨업했을 때 호출 → 레벨 텍스트 갱신, 게이지는 0부터 다시 시작
    private void UpdateLevel(int level)
    {
        levelText.text = $"Lv.{level + 1}"; // level은 0부터 시작하므로 +1 해서 표시
        fillImage.fillAmount = 0f;
    }
}
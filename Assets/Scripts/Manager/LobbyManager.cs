using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;   // upgradePanel 인스펙터에서 참조
    [SerializeField] private GameObject settingsPanel;  // settingsPanel 인스펙터에서 참조
    [SerializeField] private string gameSceneName = "GameScene";    // Play 버튼 클릭시 로드할 씬 이름

    private void Awake()
    {
        // 만약 참조 안 돼있을 시 콘솔 창으로 알려주는 코드
        if (upgradePanel == null) Debug.Log("upgradePanel 참조가 비어있습니다.");
        if (settingsPanel == null) Debug.Log("settingsPanel 참조가 비어있습니다.");
    }


    private void Start()
    {
        ClosePanels();  // 시작할때 패널들 다 끄고 시작
    }


    // 함수 실행시 gameSceneName씬으로 이동하는 함수
    public void OnClickPlayButton() => SceneManager.LoadScene(gameSceneName);

    // 함수 실행시 upgradePanel 키는 함수
    public void OnClickUpgradeButton() => SetPanel(upgradePanel, true);

    // 함수 실행시 settingsPanel 키는 함수
    public void OnClickSettingsButton() => SetPanel(settingsPanel, true);
    
    // 함수 실행시 패널들 끄는 함수
    public void OnClickCloseButton() => ClosePanels();


    // panel 오브젝트를 active에 따라서 킬지 끌지 정하는 함수
    private void SetPanel(GameObject panel, bool active)
    {
        if (panel == null) return;

        panel.SetActive(active);
    }

    // 패널들 끄는 함수
    private void ClosePanels()
    {
        SetPanel(upgradePanel, false);
        SetPanel(settingsPanel, false);
    }

}

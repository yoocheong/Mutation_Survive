using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager instance { get; private set; }

    [Header("참조")]
    public PlayerMovement playerMovement;   // 플레이어 접근
    public PoolManager poolManager; // 적, 투사체 등등 오브젝트 풀

    [Header("게임 시간")]
    public float gameTime;  // 현재 진행 시간
    public float maxGameTime = 10 * 60; // 최대 게임 시간
     
    private void Awake()
    {
        // 씬에 GameManager가 중복으로 생기면 새로 생긴 객체 제거
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        // 최대 시간 넘기면 최대 시간으로 고정
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}

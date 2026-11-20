using System;
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

    [Header("레벨 / 경험치")]
    public int level;   // 현재 레벨 (0부터 시작)
    public int exp;     // 현재 경험치
    public int[] nextExp = { 3, 5, 10, 20, 40, 60, 100 };  // 레벨별 필요 경험치

    // 현재 레벨에서 필요한 경험치 (마지막 칸을 넘으면 마지막 값 사용)
    public int NextExp => nextExp[Mathf.Min(level, nextExp.Length - 1)];

    public event Action<int> OnLevelUp;         // 레벨업했을 때 알림 (새 레벨)
    public event Action<int, int> OnExpChanged; // 경험치가 바뀔 때 알림 (현재 경험치, 필요 경험치)

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

    public void GetExp(int amount)
    {
        exp += amount;

        // 필요 경험치를 넘으면 레벨업
        if (exp >= NextExp)
        {
            exp -= NextExp; // 넘친 경험치는 다음 레벨로 넘김 (level++ 전에 계산해야 함)
            level++;
            OnLevelUp?.Invoke(level);
        }

        OnExpChanged?.Invoke(exp, NextExp); // UI에 현재 경험치와 필요 경험치 전달
    }
}


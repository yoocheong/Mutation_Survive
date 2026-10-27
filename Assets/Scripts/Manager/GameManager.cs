using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public PlayerMovement playerMovement;

    private void Awake()
    {
        // 다른 게임 매니저가 존재한다면 자기자신 삭제
        if (instance != null || instance == this)   
        {
            Destroy(gameObject);
            return;
        }

        
        instance = this;
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public PlayerMovement playerMovement;
    public PoolManager poolManager;

    private void Awake()
    {
        instance = this;
    }
}

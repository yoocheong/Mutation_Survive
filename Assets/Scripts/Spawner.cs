using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform[] spawnPoint;
    private float timer;

    [SerializeField] private int level;
    private float[] spawnIntervals = { 1f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.3f, 0.1f, 0.05f };

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        SpawnTime();
    }

    private void SpawnTime()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f),
            spawnIntervals.Length -1);

        
        if (timer > spawnIntervals[level])
        {
            timer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(Random.Range(0, 1));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}

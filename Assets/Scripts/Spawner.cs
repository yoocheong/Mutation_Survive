using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform[] spawnPoint;
    private float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTime();
    }

    private void SpawnTime()
    {
        timer += Time.deltaTime;
        if (timer > 0.2f)
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

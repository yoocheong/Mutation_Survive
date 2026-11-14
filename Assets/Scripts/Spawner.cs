using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int spawnDataLevel; // 레벨 (1분마다 1씩 증가)
    [SerializeField] private int spawnEnemyLevel; // 레벨 (3분마다 1씩 증가)

    public Transform[] spawnPoint;  // 적이 생성될 위치 저장할 배열
    public SpawnData[] spawnData;   // 레벨에 따른 적 소환 정보

    private float timer;    // 다음 스폰까지 시간 측정용 

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();  // 자식 Transform을 스폰 위치로 저장
    }

    // 매 프레임 스폰 타이밍 확인
    void Update()
    {
        SpawnTime();
    }

    // 스폰 타이밍 확인 메서드
    private void SpawnTime()
    {
        timer += Time.deltaTime;
        spawnDataLevel = Mathf.FloorToInt(GameManager.instance.gameTime / 60f);  // 60초마다 레벨 1씩 증가
        spawnEnemyLevel = Mathf.FloorToInt(GameManager.instance.gameTime / (3 * 60f)); // 3분마다 레벨 1씩 증가

        // timer가 레벨에 따른 스폰시간 넘으면 timer 초기화 및 Spawn 메서드 호출
        if (timer > spawnData[spawnDataLevel].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    // 적 스폰 메서드
    private void Spawn()
    {
        // 풀 매니저에서 spawnEnemyLevel에 따른 몬스터 가져와서 지역변수 enemy에 저장
        GameObject enemy = GameManager.instance.poolManager.Get(spawnEnemyLevel); 
        // enemy 위치를 스폰포인트 위치 중에서 무작위로 설정
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        // spawnDataLevel에 따른 적 능력치 초기화
        enemy.GetComponent<Enemy>().Initialize(spawnData[spawnDataLevel]);
    }

    // 적 소환 정보
    [System.Serializable]
    public class SpawnData
    {
        public float spawnTime; // 적 스폰 시간
        public float speed; // 적 이동속도
        public float hp;  // 적 hp
        public float damage;  // 적 공격력
    }

}

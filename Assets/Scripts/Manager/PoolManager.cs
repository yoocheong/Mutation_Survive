using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("몬스터")]
    public GameObject[] enemyPrefabs;   // 몬스터 프리팹을 담는 배열
    private List<GameObject>[] enemyPools;  // 몬스터별로 생성된 오브젝트를 보관하는 리스트 배열

    [Header("무기 / 이펙트 / 아이템")]
    public GameObject[] prefabs;    // 몬스터 외의 프리팹을 담는 배열 (무기, 총알, 경험치 보석 등)
    private List<GameObject>[] pools;   // 프리팹별로 생성된 오브젝트를 보관하는 리스트 배열

    // 몬스터 종류 수를 반환하는 읽기 전용 프로퍼티 (Spawner에서 레벨 상한으로 사용)
    public int EnemyTypeCount => enemyPrefabs.Length;

    private void Awake()
    {
        enemyPools = CreatePools(enemyPrefabs.Length);  // 몬스터 프리팹 수만큼 풀 생성
        pools = CreatePools(prefabs.Length);    // 일반 프리팹 수만큼 풀 생성
    }

    // 개수만큼 리스트 배열을 만들고 초기화해서 반환하는 메서드
    private List<GameObject>[] CreatePools(int count)   // 매개변수로 만들 풀의 개수를 받는다.
    {
        List<GameObject>[] result = new List<GameObject>[count];    // count 길이만큼 리스트 담을 배열 생성

        // result 요소마다 초기화
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = new List<GameObject>();
        }

        return result;  // 초기화된 리스트 배열 반환
    }

    // 몬스터 오브젝트를 반환하는 메서드
    public GameObject GetEnemy(int index)   // 매개변수로 index를 받는다. (원하는 몬스터 풀 선택)
    {
        return GetFromPool(enemyPools, enemyPrefabs, index);    // 몬스터 풀과 몬스터 프리팹 배열을 넘겨서 꺼냄
    }

    // 몬스터 외의 오브젝트를 반환하는 메서드 (무기, 총알, 보석 등)
    public GameObject Get(int index)    // 매개변수로 index를 받는다. (원하는 풀 선택)
    {
        return GetFromPool(pools, prefabs, index);  // 일반 풀과 일반 프리팹 배열을 넘겨서 꺼냄
    }

    // 실제로 풀에서 오브젝트를 꺼내는 메서드 (몬스터, 일반 오브젝트 공용)
    // poolArray : 꺼낼 풀 배열 / prefabArray : 새로 만들 때 쓸 프리팹 배열 / index : 풀 번호
    private GameObject GetFromPool(List<GameObject>[] poolArray, GameObject[] prefabArray, int index)
    {
        GameObject select = null;   // 오브젝트를 저장할 지역변수

        // 선택한 풀에서 비활성화된 오브젝트를 찾아 재사용
        foreach (GameObject item in poolArray[index])
        {
            if (!item.activeSelf)
            {
                select = item;  // 찾은 오브젝트 저장
                select.SetActive(true); // 활성화해서 다시 사용
                break;  // 하나 찾았으면 반복 종료
            }
        }

        // 풀에서 못 찾았으면 새로 생성 후 풀에 추가
        if (!select)
        {
            select = Instantiate(prefabArray[index], transform);    // PoolManager의 자식으로 생성
            poolArray[index].Add(select);   // 다음에 재사용할 수 있게 풀에 추가
        }

        return select;  // 오브젝트 반환
    }
}
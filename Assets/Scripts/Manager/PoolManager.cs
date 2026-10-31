using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;    // 프리팹을 담는 배열
    public List<GameObject>[] pools;    // 프리팹별롤 생성된 오브젝트를 보관하는 리스트 배열

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];   // prefabs 길이만큼 리스트 담을 배열 생성

        // pools 요소마다 초기화
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    // 오브젝트를 반환하는 메서드
    public GameObject Get(int index)    // 매개변수로 index를 받는다. (원하는 풀 선택)
    {
        GameObject select = null;   // 오브젝트를 저장할 지역변수 

        // 선택한 풀에서 비활성화된 오브젝트를 찾아 재사용
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;    // 찾은 오브젝트 저장
                select.SetActive(true);
                break;
            }
        }

        // 풀에서 못 찾았으면 새로 생성 후 풀에 추가
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;  // 오브젝트 반환
    }
}

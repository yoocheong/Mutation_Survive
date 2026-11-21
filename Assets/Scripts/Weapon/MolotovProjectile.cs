using UnityEngine;

// 화염병 투사체 (화염병 프리팹에 붙임)
// 시작 위치에서 목표 위치까지 포물선을 그리며 날아가고, 도착하면 불장판을 생성
public class MolotovProjectile : MonoBehaviour
{
    public float spinSpeed = 720f;  // 병 회전 속도 (초당 회전 각도)

    private Vector3 start;      // 출발 위치
    private Vector3 end;        // 도착 위치 (불장판이 생길 곳)
    private float flightTime;   // 도착까지 걸리는 시간
    private float arcHeight;    // 포물선 최고 높이
    private float elapsed;      // 발사 후 지난 시간
    private MolotovWeapon owner;    // 이 화염병을 던진 무기 (도착 시 불장판 생성 요청용)

    // 화염병 발사 메서드 (MolotovWeapon에서 던질 때 호출)
    // 풀에서 재사용되므로 던질 때마다 값을 새로 설정
    public void Launch(Vector3 start, Vector3 end, float flightTime, float arcHeight, MolotovWeapon owner)
    {
        this.start = start;
        this.end = end;
        this.flightTime = flightTime;
        this.arcHeight = arcHeight;
        this.owner = owner;
        elapsed = 0f;   // 풀 재사용 대비 리셋

        transform.position = start; // 출발 위치로 이동
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / flightTime);  // 비행 진행도 (0 : 출발 ~ 1 : 도착)

        // 포물선 위치 계산
        Vector3 pos = Vector3.Lerp(start, end, t);  // 출발~도착 사이를 직선으로 이동한 위치
        pos.y += arcHeight * 4f * t * (1f - t);     // 높이 추가 (t = 0.5일 때 arcHeight로 가장 높고, 양 끝에서 0)
        transform.position = pos;

        // 빙글빙글 돌면서 날아가는 연출
        transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);

        // 도착하면 불장판 생성 후 사라짐
        if (t >= 1f)
        {
            owner.SpawnFire(end);   // 던진 무기에게 도착 위치에 불장판 생성 요청
            gameObject.SetActive(false);    // 풀로 반환
        }
    }
}
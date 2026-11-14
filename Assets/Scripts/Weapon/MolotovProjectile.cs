using UnityEngine;

public class MolotovProjectile : MonoBehaviour
{
    public float spinSpeed = 720f;   // 병 회전 속도

    private Vector3 start;
    private Vector3 end;
    private float flightTime;
    private float arcHeight;
    private float elapsed;
    private MolotovWeapon owner;

    public void Launch(Vector3 start, Vector3 end, float flightTime, float arcHeight, MolotovWeapon owner)
    {
        this.start = start;
        this.end = end;
        this.flightTime = flightTime;
        this.arcHeight = arcHeight;
        this.owner = owner;
        elapsed = 0f;                 // 풀 재사용 대비 리셋

        transform.position = start;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / flightTime);

        // 포물선 위치
        Vector3 pos = Vector3.Lerp(start, end, t);
        pos.y += arcHeight * 4f * t * (1f - t);
        transform.position = pos;

        // 빙글빙글 돌면서 날아가는 연출
        transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);

        // 도착하면 불장판 생성 후 사라짐
        if (t >= 1f)
        {
            owner.SpawnFire(end);
            gameObject.SetActive(false);
        }
    }
}
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public int count;
    public float damage;
    public float speed;

    public float radius = 1.5f;
    private float currentAngle;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                currentAngle += speed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, currentAngle, 0);
                //transform.Rotate(Vector3.up * speed * Time.deltaTime);
                break;
            default:

                break;
        }
    }

    public void Initialize()
    {
        switch (id)
        {
            case 0:
                speed = -150f;
                ArrangeWeapons();
                break;
            default:

                break;
        }
    }

    private void ArrangeWeapons()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet = GameManager.instance.poolManager.Get(prefabId).transform;
            bullet.parent = this.transform;

            float angle = 360f * index / count;
            Quaternion rot = Quaternion.Euler(0, angle, 0);   // Y축 기준 각도

            bullet.localPosition = rot * Vector3.forward * radius + Vector3.up * 1f;      // 원 위의 위치
            bullet.localRotation = rot * Quaternion.Euler(90, 0, 0);    // 눕힌 채로 바깥을 향하게

            /*
            Vector3 rotVec = Vector3.up * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.forward * 1.5f, Space.World);
            */

            bullet.GetComponent<Bullet>().Initialize(damage, -1);
        }
    }
}

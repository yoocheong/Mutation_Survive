using UnityEngine;
/*
public class Weapon : MonoBehaviour
{
    public int id;  // 무기 
    public int prefabId;
    public int count;
    public float damage;
    public float speed = -150f;

    public float radius = 1.5f;
    private float currentAngle;

    float timer;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp(5f, 1);
        }

        switch (id)
        {
            case 0:
                currentAngle += speed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, currentAngle, 0);
                break;
            default:
                timer += Time.deltaTime;
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count += count;

        if (id == 0)
        {
            ArrangeWeapons();
        }
    }

    public void Initialize()
    {
        switch (id)
        {
            case 0:
                //speed = -150f;
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
            Transform bullet;

            if (index < this.transform.childCount)
            {
                bullet = this.transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.poolManager.Get(prefabId).transform;
                bullet.parent = this.transform;
            }

            float angle = 360f * index / count;
            Quaternion rot = Quaternion.Euler(0, angle, 0);   // Y축 기준 각도

            bullet.localPosition = rot * Vector3.forward * radius + Vector3.up * 1f;
            bullet.localRotation = rot * Quaternion.Euler(90, 0, 0);

            bullet.GetComponent<Bullet>().Initialize(damage, -1);
        }
    }
}
*/
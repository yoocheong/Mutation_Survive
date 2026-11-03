using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float pierceCount;

    public void Initialize(float damage, float pierceCount)
    {
        this.damage = damage;
        this.pierceCount = pierceCount;
    }

}

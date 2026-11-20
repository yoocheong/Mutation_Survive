using UnityEngine;

public class ExpGem : MonoBehaviour
{
    private int exp;

    public void Initialize(int exp)
    {
        this.exp = exp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.instance.GetExp(exp);
        gameObject.SetActive(false);   // 풀로 반환
    }
}
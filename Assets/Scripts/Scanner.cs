using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public Collider[] targets;
    public Transform nearestTarget;

    public void FixedUpdate()
    {
        targets = Physics.OverlapSphere(this.transform.position, scanRange, targetLayer);
        nearestTarget = GetNearest();
    }

    private Transform GetNearest()
    {
        Transform result = null;
        float minDistance = 100f;

        foreach (Collider target in targets)
        {
            Vector3 myPosition = this.transform.position;
            Vector3 targetPosition = target.transform.position;

            float distance = Vector3.Distance(myPosition, targetPosition);

            if (distance < minDistance)
            {
                minDistance = distance;
                result = target.transform;
            }
        }

        return result;
    }
}

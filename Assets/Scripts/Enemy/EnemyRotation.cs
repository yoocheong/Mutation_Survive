using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameManager.instance.playerMovement.transform;
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 direction = playerTransform.position - this.transform.position;
        direction.y = 0f;

        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

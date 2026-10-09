using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 25f;

    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        transform.position += transform.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
    }

    void Rotate()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }

    public void AddMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public void AddRotationSpeed(float amount)
    {
        rotationSpeed += amount;
    }
}
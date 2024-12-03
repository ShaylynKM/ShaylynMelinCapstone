using UnityEngine;

public class OriginalSpiralMovement : MonoBehaviour
{
    [SerializeField]private float speed = 100f; // Speed of rotation
    [SerializeField]private float radius = 0f; // Radius size when the object is spawned
    [SerializeField]private float radiusGrowSpeed = .5f; // How fast the radius increases
    [SerializeField]private float angle;
    [SerializeField] private float survivalTime = 1f;
    private Vector3 centerPoint; // Where the object starts -- the center of the spiral

    private void Start()
    {
        centerPoint = transform.position; // Set the center point to the location where this object is spawned
        Invoke("EventualDeath", survivalTime);
    }

    void Update()
    {
        angle += Time.deltaTime * speed; // Change the angle over time

        radius += radiusGrowSpeed * Time.deltaTime; // Increase the radius over time

        Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;

        transform.position = centerPoint + direction * radius;
    }

    private void EventualDeath()
    {
        Destroy(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject); // Destroy the object when it's out of view of the camera
    }
}

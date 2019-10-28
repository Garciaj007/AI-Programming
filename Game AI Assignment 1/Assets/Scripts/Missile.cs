using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Missile : MonoBehaviour
{
    [SerializeField] private float detectionAngle = 30.0f;
    [SerializeField] private float raycastLength = 5.0f;
    [SerializeField] private float maxSpeed = 2.0f;
    [SerializeField] private float maxAngularSpeed = 90.0f;
    [SerializeField] private float disableHitDuration = 0.1f;

    private RaycastHit2D leftWallHit, rightWallHit;
    private Rigidbody2D rigid = null;
    private bool hasHitLeft, hasHitRight, isRayDisabled;
    private float disableHitTestingTime = 0.0f;

    public Player Owner { get; set; } = null;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var desired = (Enemy.Instance.transform.position - transform.position).normalized * maxSpeed;
        var steering = desired - new Vector3(rigid.velocity.x, rigid.velocity.y);
        steering = AvoidWalls(steering);
        steering = Vector3.ClampMagnitude(steering, maxAngularSpeed);
        Debug.DrawRay(transform.position, steering, Color.magenta);
        rigid.AddForce(steering);
        UpdateOrientation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall") Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Owner.HasMissile = true;
    }

    private void UpdateOrientation()
    {
        transform.eulerAngles = new Vector3(0, 0,
            -Mathf.Atan2(rigid.velocity.x, rigid.velocity.y) * Mathf.Rad2Deg);
    }

    private Vector3 AvoidWalls(Vector3 direction)
    {
        if (Time.time > disableHitTestingTime + disableHitDuration)
            isRayDisabled = false;

        hasHitLeft = hasHitRight = false;

        var leftRayDirection = Quaternion.AngleAxis(-detectionAngle, Vector3.forward) * rigid.velocity.normalized;
        var rightRayDirection = Quaternion.AngleAxis(detectionAngle, Vector3.forward) * rigid.velocity.normalized;

        Debug.DrawRay(transform.position, leftRayDirection * raycastLength, Color.green);
        Debug.DrawRay(transform.position, rightRayDirection * raycastLength, Color.green);

        var origin = new Vector2(transform.position.x, transform.position.y);
        var leftRay = new Vector2(leftRayDirection.x, leftRayDirection.y);
        var rightRay = new Vector2(rightRayDirection.x, rightRayDirection.y);

        leftWallHit = Physics2D.Raycast(origin, leftRay, raycastLength, LayerMask.GetMask("Wall"));
        rightWallHit = Physics2D.Raycast(origin, rightRay, raycastLength, LayerMask.GetMask("Wall"));

        hasHitLeft = leftWallHit.collider != null;
        hasHitRight = rightWallHit.collider != null && !isRayDisabled;

        if (!hasHitLeft && !hasHitRight) return direction;

        if (hasHitLeft && hasHitRight)
        {
            isRayDisabled = true;
            disableHitTestingTime = Time.time;
        }

        if (hasHitLeft)
            direction = leftWallHit.normal * maxSpeed;

        if (hasHitRight)
            direction = rightWallHit.normal * maxSpeed;

        return direction;
    }
}

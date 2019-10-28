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
    private bool hasHitLeft, hasHitRight, isRayDisabled;
    private float disableHitTestingTime = 0.0f;

    private Vector2 heading = Vector2.up;

    public Player Owner { get; set; } = null;

    private void Update()
    {
        var desired = (Enemy.Instance.transform.position - transform.position).normalized;
        PreviewRay(desired, Color.blue);
        desired = AvoidWalls(desired);
        PreviewRay(desired, Color.yellow);
        var anglebetween = Mathf.Acos(Vector3.Dot(desired, heading)) * Mathf.Rad2Deg;
        var maxAngle = maxAngularSpeed * Time.deltaTime * Mathf.Deg2Rad;

        if (anglebetween <= maxAngle)
        {
            heading = desired.normalized;
        }
        else
        {
            var normal = Vector2.Dot(new Vector2(-heading.y, heading.x), desired);
            if (normal < 0) maxAngle *= -1;

            heading =
                new Vector3(
                    heading.x * Mathf.Cos(maxAngle) - heading.y * Mathf.Sin(maxAngle),
                    heading.x * Mathf.Sin(maxAngle) + heading.y * Mathf.Cos(maxAngle))
                    .normalized;
        }

        UpdateOrientation();

        transform.position += new Vector3(heading.x, heading.y) * maxSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            AudioManager.Instance.PlaySound("WallHit");
            Destroy(gameObject);
        }
    }

    private void OnDestroy() => Owner.HasMissile = true;

    private void UpdateOrientation() =>
        transform.eulerAngles = new Vector3(0, 0,
            -Mathf.Atan2(heading.x, heading.y) * Mathf.Rad2Deg);

    private void PreviewRay(Vector2 dir, Color c) => 
        Debug.DrawRay(transform.position, dir * maxSpeed, c);

    private Vector3 AvoidWalls(Vector3 direction)
    {
        if (Time.time > disableHitTestingTime + disableHitDuration && isRayDisabled)
        {
            isRayDisabled = false;
            Debug.Log("Enabling Ray...");
        }

        hasHitLeft = hasHitRight = false;

        var leftRayDirection = Quaternion.AngleAxis(-detectionAngle, Vector3.forward) * heading;
        var rightRayDirection = Quaternion.AngleAxis(detectionAngle, Vector3.forward) * heading;

        Debug.DrawRay(transform.position, leftRayDirection * raycastLength, Color.green);
        Debug.DrawRay(transform.position, rightRayDirection * raycastLength, Color.green);

        var origin = new Vector2(transform.position.x, transform.position.y);
        var leftRay = new Vector2(leftRayDirection.x, leftRayDirection.y);
        var rightRay = new Vector2(rightRayDirection.x, rightRayDirection.y);

        leftWallHit = Physics2D.Raycast(origin, leftRay, raycastLength, LayerMask.GetMask("Wall"));
        rightWallHit = Physics2D.Raycast(origin, rightRay, raycastLength, LayerMask.GetMask("Wall"));

        hasHitLeft = leftWallHit.collider != null;
        hasHitRight = rightWallHit.collider != null & !isRayDisabled;

        if (!hasHitLeft && !hasHitRight) return direction;

        if (hasHitLeft && hasHitRight)
        {
            Debug.Log("Disabling Ray...");
            isRayDisabled = true;
            disableHitTestingTime = Time.time;
        }

        if (hasHitLeft)
            direction = (leftWallHit.normal + heading).normalized;

        if (hasHitRight)
            direction = (rightWallHit.normal + heading).normalized;

        return direction;
    }
}

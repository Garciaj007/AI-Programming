using UnityEngine;

public class Intercepter : MonoBehaviour {
    public GameObject target;
    public float speed;
    public float maxAngularSpeed;
    Vector3 orientation = Vector3.up;
    Vector3 velocity = Vector3.zero;

    Meteor meteor = null;

    private void Start()
    {
        meteor = target.GetComponent<Meteor>();
    }

    // Update is called once per frame
    void Update () {
        // To do: Complete Update() to intercept the target
        // * recommended steps: 
        // 1. implement the chasing that you did in Lab3.1
        // 2. (Optional) implement the rotation limit that you did in Lab3.3
        // 3. calculate the predicted moving position and chase the predicted position instead of the actual position of the target.
        var Vr = meteor.velocity - velocity;
        var Sr = meteor.transform.position - transform.position;
        var t = Sr.magnitude / Vr.magnitude;
        var predictedPosition = meteor.transform.position + meteor.velocity * t;

        var desired = (predictedPosition - transform.position);

        var anglebetween = Mathf.Acos(Vector3.Dot(desired, orientation) / (desired.magnitude * orientation.magnitude)) * Mathf.Rad2Deg;
        var maxAngle = maxAngularSpeed * Time.deltaTime * Mathf.Deg2Rad;

        if (anglebetween <= maxAngle)
        {
            orientation = desired.normalized;
        }
        else
        {
            var normal = Vector2.Dot(new Vector2(-orientation.y, orientation.x), desired);
            if (normal < 0) maxAngle *= -1;

            orientation =
                new Vector3(
                    orientation.x * Mathf.Cos(maxAngle) - orientation.y * Mathf.Sin(maxAngle),
                    orientation.x * Mathf.Sin(maxAngle) + orientation.y * Mathf.Cos(maxAngle))
                    .normalized;
        }

        UpdateOrientation(orientation.x, orientation.y);
        transform.position += orientation * speed * Time.deltaTime;
    }

    void UpdateOrientation(float x, float y)
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(x, y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}

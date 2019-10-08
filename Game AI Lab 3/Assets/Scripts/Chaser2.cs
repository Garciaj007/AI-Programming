using UnityEngine;

public class Chaser2 : MonoBehaviour {
    public GameObject target = null;
    public float maxSpeed = 10f;
    public float maxAngularSpeed = 90f;
    public float arrivalDistance = 10f;

    private Rigidbody2D rigid = null;
    private Vector3 orientation = Vector3.up;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    //I Dont know if my math is wrong but I was using some different ways to achieve the same goal, pls note where I went
    //wrong in the other Method, Also which method is better/less taxing...
    void Update()
    {
        //Reynolds Method 
        var desired = (target.transform.position - transform.position).normalized * maxSpeed;

        //Arrival Behaviour
        desired = desired.magnitude < arrivalDistance ? 
            desired * Utils.Mathf.Map(desired.magnitude, 0, arrivalDistance, 0, maxSpeed) : 
            desired * maxSpeed;

        var steering = desired - new Vector3(rigid.velocity.x, rigid.velocity.y);
        steering = Vector3.ClampMagnitude(steering, maxAngularSpeed);
        rigid.AddForce(steering);
        UpdateOrientation(rigid.velocity.x, rigid.velocity.y);

        //In Class Way
        //var desired = (target.transform.position - transform.position);
        //var anglebetween = Mathf.Acos(Vector3.Dot(desired, orientation) / (desired.magnitude * orientation.magnitude)) * Mathf.Rad2Deg;
        //var maxAngle = maxAngularSpeed * Time.deltaTime * Mathf.Rad2Deg;
        //
        //Debug.Log(maxAngle);
        //
        //if (anglebetween < maxAngle)
        //{
        //    orientation = desired.normalized;
        //}
        //else
        //{
        //    orientation =
        //        new Vector3(
        //            orientation.x * Mathf.Cos(maxAngle) - orientation.y * Mathf.Sin(maxAngle),
        //            orientation.x * Mathf.Sin(maxAngle) + orientation.y * Mathf.Cos(maxAngle));
        //}
        //
        //UpdateOrientation(orientation.x, orientation.y);
        //transform.position += orientation * maxSpeed * Time.deltaTime;
    }

    void UpdateOrientation(float x, float y)
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(x, y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}

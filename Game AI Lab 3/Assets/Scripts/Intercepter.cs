using UnityEngine;

public class Intercepter : MonoBehaviour {
    public GameObject target;
    public float speed;
    public float maxAngularSpeed;
    Vector3 orientation = Vector3.up;

    Rigidbody2D rigid = null;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        // To do: Complete Update() to intercept the target
        // * recommended steps: 
        // 1. implement the chasing that you did in Lab3.1
        // 2. (Optional) implement the rotation limit that you did in Lab3.3
        // 3. calculate the predicted moving position and chase the predicted position instead of the actual position of the target.
        var relativeDistance = (target.transform.position - transform.position);
        var relativeVelocity = target.GetComponent<Meteor>().velocity - (Vector3) rigid.velocity;
        var relativeTime = relativeDistance.magnitude / relativeVelocity.magnitude;
        var steering = target.transform.position + target.GetComponent<Meteor>().velocity * relativeTime;
        rigid.AddForce(steering);
        UpdateOrientation(rigid.velocity.x, rigid.velocity.y);
    }

    void UpdateOrientation(float x, float y)
    {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(x, y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}

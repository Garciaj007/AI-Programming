using UnityEngine;

public class ApplyCameraBounds : MonoBehaviour
{
    [SerializeField] private float width = 0.0f;

    private void Update()
    {
        Camera.main.orthographicSize = width * Screen.height / Screen.width * 0.5f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(-width / 2.0f, 0.0f), new Vector3(width / 2.0f, 0.0f));
    }

}

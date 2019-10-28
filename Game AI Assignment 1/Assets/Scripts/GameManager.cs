using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnPlayerMoveDelegate(float axis);
    public delegate void OnLaunchMissileDelegate();
    public event OnPlayerMoveDelegate PlayerMoveDelegate;
    public event OnLaunchMissileDelegate LaunchMissileDelegate;

    public static GameManager Instance { get; set; }
    public GameObject CurrentMissileInScene { get; set; }

    private void Awake () {
		if(Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;
    }
    
	private void Update ()
    {
        PlayerMoveDelegate?.Invoke(Input.anyKey ? Input.GetAxis("Horizontal") : 0.0f);
        if (Input.GetKeyDown(KeyCode.Space)) LaunchMissileDelegate?.Invoke();
    }
}

using System;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public delegate void OnEnemyDeathDelegate();
    public event OnEnemyDeathDelegate DeathDelegate;

    [SerializeField] private float path = 0.0f;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float respawnTime = 0.5f;

    private SpriteRenderer sp = null;
    private float timeOfDeath = 0f;
    private bool isDead = false;

    public static Enemy Instance { get; set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        SetRandomDirection();
    }

    private void Update ()
    {
        if (isDead)
        {
            if (Time.time > timeOfDeath + respawnTime)
                Respawn();
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x > path || transform.position.x < -path)
                SetSpeed(-speed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-path, transform.position.y), new Vector3(path, transform.position.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Missile") return;
        Destroy(other.gameObject);
        isDead = true;
        timeOfDeath = Time.time;
        AudioManager.Instance.PlaySound("Explosion");
        DeathDelegate?.Invoke();
    }

    private void Respawn()
    {
        SetRandomDirection();
        isDead = false;
    }

    private void SetRandomDirection()
    {
        SetSpeed(Random.Range(0, 2) == 0 ? speed : -1 * speed);
    }

    private void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        sp.flipY = speed < 0;
    }
}

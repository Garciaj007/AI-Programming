using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject missileObj = null;
    [SerializeField] private float maxSpeed = 4.0f;

    private Rigidbody2D rigid;
    public bool HasMissile { private get; set; } = true;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        GameManager.Instance.PlayerMoveDelegate += PlayerMove;
        GameManager.Instance.LaunchMissileDelegate += LaunchMissile;
    }

    private void PlayerMove(float axis)
    {
        rigid.AddForce(new Vector2(axis, 0.0f), ForceMode2D.Impulse);
        rigid.velocity = rigid.velocity.magnitude > maxSpeed ? rigid.velocity.normalized * maxSpeed : rigid.velocity;
    }

    private void LaunchMissile()
    {
        if (!HasMissile) return;
        var missile = Instantiate(missileObj, transform.position, Quaternion.identity);
        missile.GetComponent<Missile>().Owner = this;
        AudioManager.Instance.PlaySound("Firing");
        GameManager.Instance.CurrentMissileInScene = missile;
        HasMissile = false;
    }
}

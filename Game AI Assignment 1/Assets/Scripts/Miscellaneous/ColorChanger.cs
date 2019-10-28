using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private float colorChangeSpeed = 0.001f;

    private SpriteRenderer sp;
    private float hue;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        hue = Random.Range(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        hue += colorChangeSpeed;
        hue = hue > 1.0f ? 0.0f : hue;
        sp.color = Color.HSVToRGB(hue, 1.0f, 1.0f);
    }
}

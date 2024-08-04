using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWave : MonoBehaviour
{
    float sinCenterX;
    public float amplitude = 2;
    public float frequency = 0.5f;

    public float verticalSpeed = 3.0f;
    public bool inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        sinCenterX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        // Menggerakkan musuh ke bawah
        pos.y -= verticalSpeed * Time.fixedDeltaTime;

        // Menghitung gerakan horizontal berdasarkan gelombang sinusoidal
        float sin = Mathf.Sin(pos.y * frequency) * amplitude;
        if (inverted)
        {
            sin *= -1;
        }
        pos.x = sinCenterX + sin;

        transform.position = pos;
    }
}
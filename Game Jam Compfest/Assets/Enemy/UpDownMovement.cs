using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool isBoss = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (isBoss)
        {
            if (pos.y > 13.6f)
            {
                pos.y -= moveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                pos.y = 13.6f;
            }
        }
        else
        {
            pos.y -= moveSpeed * Time.fixedDeltaTime;
        }

        transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public float dir = -1f;
    public SpriteRenderer sr;
    public float speed = 3f;
    protected Vector3 lastPosition;
    protected float maxStoppedTime = 0.75f;
    protected float stoppedTimer = 0.75f;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        if (Vector3.Distance(transform.position, lastPosition) > 0.1f)
        {
            lastPosition = transform.position;
            stoppedTimer = maxStoppedTime;
        }
        else
        {
            stoppedTimer -= Time.deltaTime;
            if (stoppedTimer < 0.0f)
            {
                dir *= -1f;
                stoppedTimer = maxStoppedTime;
            }
        }
        sr.flipX = dir > 0f;
    }
}

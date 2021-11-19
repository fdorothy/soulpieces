using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    public Vector3 dir = Vector3.left;
    public float speed = 1.0f;
    public bool alive = false;
    public LayerMask isGround;
    public LayerMask isPlayer;
    protected SpriteRenderer sr;
    protected float life = 5.0f;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void Begin(Vector3 position, Vector3 dir, float speed)
    {
        this.dir = dir;
        this.speed = speed;
        alive = true;
        life = 5.0f;
    }

    public void Stop()
    {
        alive = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit something");
        if ((1<<collision.gameObject.layer & isGround.value) != 0)
            Stop();
        if ((1 << collision.gameObject.layer & isPlayer.value) != 0)
        {
            collision.gameObject.SendMessage("hit");
            Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            transform.position += dir * speed * Time.deltaTime;

            if (life < 0.0f)
                Stop();
        }
        if (!sr.isVisible)
            Stop();
    }
}

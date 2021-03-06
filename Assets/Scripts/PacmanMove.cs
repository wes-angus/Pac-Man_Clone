﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1;
    Vector2 vel;
    CircleCollider2D cc;
    public float leftBound, rightBound;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        vel = Vector2.right * speed;
        cc = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                moveDir(Vector2.right);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                moveDir(Vector2.left);
            }
        }
        else if (Input.GetButton("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                moveDir(Vector2.up);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                moveDir(Vector2.down);
            }
        }
	}

    void moveDir(Vector2 dir)
    {
        if (vel.normalized != dir.normalized)
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, dir, cc.radius + 0.2f,
                                            1 << LayerMask.NameToLayer("Background"));
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag.Equals("Maze"))
                {
                    return;
                }
            }
            vel = dir * speed;
            rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + vel * Time.deltaTime);

        // loop
        if(rb.position.x > rightBound)
        {
            rb.position = new Vector2(leftBound + 0.01f, rb.position.y);
        }
        else if (rb.position.x < leftBound)
        {
            rb.position = new Vector2(rightBound - 0.01f, rb.position.y);
        }
    }
}

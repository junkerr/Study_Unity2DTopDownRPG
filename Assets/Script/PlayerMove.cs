using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float speed;

    float h;
    float v;

    Rigidbody2D rigid;

    void Awake()
    {
        speed = 2f;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v * speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    Vector3 dirVector;
    GameObject scanObject;

    float speed;
    float h;
    float v;
    bool isHorizonMove;

    void Awake()
    {
        speed = 2f;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // Check Button Down & Up
        bool inputH = Input.GetButton("Horizontal");
        bool inputV = Input.GetButton("Vertical");

        // Check Horizontal Move
        if (inputH)
        {
            isHorizonMove = true;
            anim.SetBool("isHorizonMove", true);
        }
        else if (inputV)
        {
            isHorizonMove = false;
            anim.SetBool("isHorizonMove", false);
        }

        // Animation
        if (anim.GetInteger("hAxisRaw") != (int)h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != (int)v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        // Direction
        if (inputV && v == 1)
        {
            dirVector = Vector3.up;
        }
        else if (inputV && v == -1)
        {
            dirVector = Vector3.down;
        }
        else if (inputH && h == 1)
        {
            dirVector = Vector3.right;
        }
        else if (inputH && h == -1)
        {
            dirVector = Vector3.left;
        }

        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            Debug.Log("scanObject : " + scanObject.name);
        }

    }

    private void FixedUpdate()
    {
        // Move
        Vector2 moveVector = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVector * speed;

        // Ray
        Debug.DrawRay(rigid.position, dirVector * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVector, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}

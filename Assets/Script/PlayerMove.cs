using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MobileKey
{
    public int CurrentValue { get; set; }

    public bool isKeyDown { get; set; }

    public MobileKey()
    {
        CurrentValue = 0;
        isKeyDown = false;
    }
}

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;

    Rigidbody2D rigid;
    Animator anim;
    Vector3 dirVector;
    GameObject scanObject;

    float speed;
    float h;
    float v;
    bool isHorizonMove;

    MobileKey downKey;
    MobileKey upKey;
    MobileKey leftKey;
    MobileKey rightKey;

    void Awake()
    {
        speed = 5f;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        downKey = new MobileKey();
        upKey = new MobileKey(); ;
        leftKey = new MobileKey(); ;
        rightKey = new MobileKey(); ;
    }

    void Update()
    {
        // Move Value
        // PC
        h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + rightKey.CurrentValue + leftKey.CurrentValue;
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical") + upKey.CurrentValue + downKey.CurrentValue;

        // Mobile
        //h = gameManager.isAction ? 0 : rightKey.CurrentValue + leftKey.CurrentValue;
        //v = gameManager.isAction ? 0 : upKey.CurrentValue + downKey.CurrentValue;

        // Check Button Down & Up
        // PC
        bool inputH = gameManager.isAction ? false : Input.GetButton("Horizontal") || rightKey.isKeyDown || leftKey.isKeyDown;
        bool inputV = gameManager.isAction ? false : Input.GetButton("Vertical") || upKey.isKeyDown || downKey.isKeyDown;

        // Mobile
        //bool inputH = gameManager.isAction ? false : rightKey.isKeyDown || leftKey.isKeyDown;
        //bool inputV = gameManager.isAction ? false : upKey.isKeyDown || downKey.isKeyDown;


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
            //Debug.Log("scanObject : " + scanObject.name);
            gameManager.Action(scanObject);
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

    public void ButtonDown(string button)
    {
        switch (button)
        {
            case "U":
                upKey.CurrentValue = 1;
                upKey.isKeyDown = true;
                break;

            case "D":
                downKey.CurrentValue = -1;
                downKey.isKeyDown = true;
                break;

            case "L":
                leftKey.CurrentValue = -1;
                leftKey.isKeyDown = true;
                break;

            case "R":
                rightKey.CurrentValue = 1;
                rightKey.isKeyDown = true;
                break;

            case "A":
                // Scan Object
                if (scanObject != null)
                {
                    gameManager.Action(scanObject);
                }
                break;

            case "C":
                gameManager.SubMenuActive();
                break;
            default:
                break;
        }
    }

    public void ButtonUp(string button)
    {
        switch (button)
        {
            case "U":
                upKey.CurrentValue = 0;
                upKey.isKeyDown = false;
                break;

            case "D":
                downKey.CurrentValue = 0;
                downKey.isKeyDown = false;
                break;

            case "L":
                leftKey.CurrentValue = 0;
                leftKey.isKeyDown = false;
                break;

            case "R":
                rightKey.CurrentValue = 0;
                rightKey.isKeyDown = false;
                break;

            default:
                break;
        }
    }
}

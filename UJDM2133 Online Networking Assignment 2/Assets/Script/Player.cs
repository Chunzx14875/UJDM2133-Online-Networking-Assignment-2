using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Photon.MonoBehaviour
{
    public PhotonView photonView;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public SpriteRenderer sr;
    public Text PlayerNameText;

    public bool isGrounded = false;
    //public Transform playerPos;
    //public float positionRadius;
    //public LayerMask ground;

    //private float airTimeCount;
    //public float airTime;
    //private bool inAir;

    public float MoveSpeed;
    public float JumpForce;

    private void Awake()
    {
        if(photonView.isMine)
        {
            PlayerCamera.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
           CheckInput();
        }
    }

    private void CheckInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * MoveSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            sr.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            sr.flipX = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }
}

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

    private Vector2 moveInput;

    public bool isGrounded = false;
    //public Transform playerPos;
    //public float positionRadius;
    //public LayerMask ground;

    //private float airTimeCount;
    //public float airTime;
    //private bool inAir;

    public float MoveSpeed;

    public GameObject BulletObject;
    public Transform FirePos;

    public bool DisableInput = false;

    private void Awake()
    {
        if(photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            PlayerNameText.text = photonView.owner.name;
            PlayerNameText.color = Color.cyan;
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine && !DisableInput)
        {
           CheckInput();
        }
    }

    private void CheckInput()
    {
        //var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        //transform.position += move * MoveSpeed * Time.deltaTime;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * MoveSpeed;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
        }

        if(moveInput.x != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void Shoot()
    {
        if (sr.flipX == false)
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletObject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Quaternion.identity, 0);
        }

        if (sr.flipX == true)
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletObject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChangeDir_left", PhotonTargets.AllBuffered);
        }  
    }


    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }

    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }
}

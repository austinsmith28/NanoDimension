using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    public CharacterController controller;

    public float speed = 13f;
    public float sprintSpeed =20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public  LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    bool isShiftDown;

    PhotonView PV;


    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    PlayerManager playerManager;

    void Awake() {
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    void Start() {

        if(!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
           
            Destroy(controller);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine)
            return;
        //Debug.Log($"isGrounded = {isGrounded}");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isShiftDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded && isShiftDown)
        {
            speed = sprintSpeed;
        }
        else{
            speed = 13f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

      


        if (!isShiftDown)
        { 
            controller.Move(move.normalized * speed * Time.deltaTime);
        }
         if (isShiftDown)
        { 
            controller.Move(move.normalized * sprintSpeed * Time.deltaTime);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime); 
    }


    public void TakeDamage(float damage){


        Debug.Log("took damage "+ damage);

        PV.RPC("RPC_TakeDamage", RpcTarget.All, damage);
        
   }

   [PunRPC]
   void RPC_TakeDamage(float damage)
   {
        if(!PV.IsMine){return;}
            

        currentHealth -= damage;

        if(currentHealth <= 0){
            playerManager.Die();
        }
   }

   
}

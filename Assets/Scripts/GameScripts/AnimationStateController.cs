using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimationStateController : MonoBehaviour
{
   Animator animator;
   int isWalkingHash;

   int isWalkingBackHash;
   int isJumpHash;

    PhotonView PV;

    void Awake() {
        PV = GetComponent<PhotonView>();
    }



   

    void Start()
    {
   

        animator = GetComponent<Animator>();



        isWalkingHash = Animator.StringToHash("isWalking");

        isWalkingBackHash = Animator.StringToHash("walkingBack");

        isJumpHash = Animator.StringToHash("isJump");
        

     
    }

    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine)
            return;

        ////////////////////////if player presses w////////////////////////////////////////////
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPress = Input.GetKey("w") ;

      
        if (!isWalking && forwardPress)
        {

            animator.SetBool(isWalkingHash, true);

        }
       if (isWalking && !forwardPress)
        {

            animator.SetBool(isWalkingHash, false);

        }
        ////////////////////////////////////////////////////////







        /////////////////if player presses s to walk backwards//////////////////////
        bool isWalkingBack = animator.GetBool(isWalkingBackHash); 
        bool backwardPress = Input.GetKey("s");

         if (!isWalkingBack && backwardPress)
        {

            animator.SetBool(isWalkingBackHash, true);

        }
       if (isWalkingBack && !backwardPress)
        {

            animator.SetBool(isWalkingBackHash, false);

        }

       //////////////////////////////////////////////////





        /////////////////if player presses space bar to jump//////////////////////
        bool isJump = animator.GetBool(isJumpHash);
        bool jumpPress = Input.GetButtonDown("Jump");

        
        if (!isJump && jumpPress)
        {

            animator.SetBool(isJumpHash, true);

        }
       if (isJump && !jumpPress)
        {

            animator.SetBool(isJumpHash, false);

        }
        //////////////////////////////////////////////////////////////////



    }
}

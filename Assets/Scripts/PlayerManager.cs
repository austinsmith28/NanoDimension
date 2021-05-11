using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
public class PlayerManager : MonoBehaviour
{
    public PhotonView PV;
    public int myTeam;

    public GameObject myPlayer;

    
   

    void Awake() {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if(PV.IsMine)
        {
            //CreateController();
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }
    }

    void Update() {

        if(myPlayer == null && myTeam != 0)
        {
            if (myTeam == 1)
            {

                if(PV.IsMine)
                {
                    Vector3 spawnpoint = new Vector3 (Random.Range(-60f,-34f), 4.65f, Random.Range(-6f,6)); 
                    myPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), spawnpoint, Quaternion.identity,  0, new object[] {PV.ViewID});
                }

            }




            if (myTeam == 2)
            {

                if(PV.IsMine)
                {
                    Vector3 spawnpoint = new Vector3 (Random.Range(-62f,-32f), 4.65f, Random.Range(-220f,-209f));  
                    myPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player 1"), spawnpoint, Quaternion.identity,  0, new object[] {PV.ViewID});
                }

            }
        }
        
    }
 
    void CreateController()
    {
       // controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity, 0, new object[] {PV.ViewID});
        
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = RoomManager.Instance.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;



    }

    public void Die() {
        PhotonNetwork.Destroy(myPlayer);
        Debug.Log("hello im here and alive");
    }
}

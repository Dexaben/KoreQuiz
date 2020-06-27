using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Laucher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject progressLabel;
    [SerializeField]
    private byte maxPlayersPerRoom = 2;
    bool isConnecting;
    static Laucher laucher = null;
	void Awake () {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        if(laucher == null)
        {
            laucher = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
        Debug.Log("photon nickname:" + PhotonNetwork.NickName);
    }
    public void Connect () {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        if (PhotonNetwork.NickName == null)
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
        }
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {
            
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

            PhotonNetwork.ConnectUsingSettings();
            isConnecting = true;
        }
        
    }
    #region MonoBehaviourPunCallbacks Callbacks
    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }
    #endregion
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("KquizRoom" + PhotonNetwork.NickName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(4);
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameObject.FindWithTag("GameManager").GetComponent<PhotonView>().RPC("oyuncu_kacti", RpcTarget.All, null);
    }
    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public override void OnLeftLobby()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }
    public void QuickCancel()
    {

        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
       
    }
}

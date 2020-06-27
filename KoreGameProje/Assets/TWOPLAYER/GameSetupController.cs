    using Photon.Pun;
	using Photon.Realtime;
    using System.IO;
    using UnityEngine;
    using UnityEngine.UI;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    public PhotonView pw;
    int oyuncu1_skor = 0;
    int oyuncu2_skor = 0;



    [SerializeField] Text oyuncu1_Name;
    [SerializeField] Text oyuncu2_Name;
    [SerializeField] Text oyuncu1_SkorText;
    [SerializeField] Text oyuncu2_SkorText;
    void Start()
    {
        CreatePlayer();
        pw = GetComponent<PhotonView>();
    }
    private void CreatePlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("multiprefabs", "Oyuncu"), Vector3.zero, Quaternion.identity, 0);
        player.GetComponent<PhotonView>().Owner.NickName = PhotonNetwork.LocalPlayer.NickName;

    }

    [PunRPC]
    public void basla()
    {
        oyuncu1_Name.text = PhotonNetwork.PlayerList[0].NickName;
        oyuncu2_Name.text = PhotonNetwork.PlayerList[1].NickName;
        oyuncu1_SkorText.text = PhotonNetwork.PlayerList[0].NickName + " Skor : " + oyuncu1_skor;
        oyuncu2_SkorText.text = PhotonNetwork.PlayerList[1].NickName + " Skor : " + oyuncu2_skor;
    }
}
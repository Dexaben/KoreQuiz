using UnityEngine;

public class EnerjiYokScript : MonoBehaviour
{

    [SerializeField] private System.Collections.Generic.List<GameObject> energys;
    void Start()
    {
        energys.AddRange(GameObject.FindGameObjectsWithTag("enerji"));
    }
    public void EnerjiYenile()
    {
        if (PlayerPrefs.GetInt("Elmas") >= 1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (PlayerPrefs.GetString("lastbuttonclick" + i) != "0")
                {
                    PlayerPrefs.SetString("lastbuttonclick" + i, "0");
                    energys[i].GetComponent<EnerjiTimer>().Tazele();
                    int temp = PlayerPrefs.GetInt("Elmas");
                    PlayerPrefs.SetInt("Elmas", temp -= 1);
                    return;
                }
            }
            close();
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Zaten tüm enerjileriniz dolu!";
            Destroy(Instantiate(dogrucevap), 2.3f);
        }
        else
        {
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Yeterli elmasınız yok!";
            Destroy(Instantiate(dogrucevap), 2.3f);
        }
    }
    public void close()
    {
        Destroy(gameObject);
    }
}

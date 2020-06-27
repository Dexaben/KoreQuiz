using UnityEngine;
using UnityEngine.UI;
public class EnerjiTimer : MonoBehaviour
{
    public ulong lastButtonClick;
    [SerializeField] private levelselectscript lvlslct;
    [SerializeField] enerjiUI enUI;
    public bool hazir;
    public int kalanDakika;
    private void Start()
    {
        hazir = false;
        enUI.energys[int.Parse(gameObject.name)] = this.gameObject.GetComponent<EnerjiTimer>();
        if (PlayerPrefs.HasKey("lastbuttonclick" + gameObject.name) == false)
        {
            PlayerPrefs.SetString("lastbuttonclick"+gameObject.name, "0");
            lastButtonClick = 0;
        }
        else
        {
            lastButtonClick = ulong.Parse(PlayerPrefs.GetString("lastbuttonclick" + gameObject.name));
        }
        if (EnerjiHazirMi())
        {
            PlayerPrefs.SetString("lastbuttonclick" + gameObject.name,"0");

        }
      
    }
   
    public void Tazele()
    {
        lvlslct.HideBanner();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        //Zamanlayıcıyı ayarla
        LASTBUTTONCLICK_HESAPLA(lastButtonClick);
    }
    void LASTBUTTONCLICK_HESAPLA(ulong lastbuttonclick)
    {
        ulong diff = ((ulong)System.DateTime.Now.Ticks - lastbuttonclick);
        ulong m = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(3600000f - m) / 1000.0f;
        if ((secondsLeft) <= 0 && !lvlslct.loading)
        {
            if(PlayerPrefs.GetString("lastbuttonclick"+gameObject.name,"0") != "0")
            {
                PlayerPrefs.SetString("lastbuttonclick" + gameObject.name, "0");
                Debug.Log(gameObject.name + " sıfırlandı");
                Tazele();
            }
        }
        kalanDakika = (int)secondsLeft / 60;
        if (kalanDakika < 0)
        {
            kalanDakika = 0;
        }
        if (!hazir)
        {
            Debug.Log(gameObject.name + " hazır!" + "  kalan dakika = " + kalanDakika);
            hazir = true;
        }

    }
    private bool EnerjiHazirMi()
    {
        ulong diff = ((ulong)System.DateTime.Now.Ticks - lastButtonClick);
        ulong m = diff / System.TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(3600000f - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            return true;
        }
        return false;
    }
}

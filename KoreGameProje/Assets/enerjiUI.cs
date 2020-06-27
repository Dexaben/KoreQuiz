using UnityEngine;
using UnityEngine.UI;


public class enerjiUI : MonoBehaviour {

   
    [SerializeField] Text energyText;
    [SerializeField] Text sonrakiEnerjiText;
    public System.Collections.Generic.List<EnerjiTimer> energys = new System.Collections.Generic.List<EnerjiTimer>(3);
    public ulong[] lastbuttonClick = new ulong[3];
    public int[] kalanDakika = new int[3];
    int enerji_;

    public ulong tempLastButtonClick;
    public int tempKalanDakika;


    ulong lastbuttonclikc_;

  
    void Update()
    {
        if(energys[0].hazir == true && energys[1].hazir == true && energys[2].hazir == true)
        {
            TextsControl();
            for(int i = 0;i<3;i++)
            {
                lastbuttonClick[i] = energys[i].lastButtonClick;
                Debug.Log("enerji class alındı");
                kalanDakika[i] = energys[i].kalanDakika;
            }
            Debug.Log("text ve enerji control");
            ulong diff = ((ulong)System.DateTime.Now.Ticks - EnerjiControl());
            ulong m = diff / System.TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(3600000f - m) / 1000.0f;

            string r = "";
            //Saatler
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //Dakikalar
            r += ((int)secondsLeft / 60).ToString("00") + "d ";
            //Saniyeler
            r += (secondsLeft % 60).ToString("00") + "sn";
            sonrakiEnerjiText.text = r;
        }
    }
    void TextsControl()
    {
        enerji_ = 0;
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetString("lastbuttonclick" + i) == "0")
            {
             enerji_ += 1;
            }
        }
        if (enerji_ >= 3)
        {
            sonrakiEnerjiText.gameObject.SetActive(false);
        }
        else
        {
            sonrakiEnerjiText.gameObject.SetActive(true);
        }
        energyText.text = enerji_ + "/3";
        if(PlayerPrefs.HasKey("UnliminatedEnergy"))
        {
            energyText.text = "Sınırsız\nEnerji";
        }

    }
    private ulong EnerjiControl()
    {
        for (int i = 0; i < 3; i++)
        {
            lastbuttonClick[i] = energys[i].lastButtonClick;
            Debug.Log("enerji class alındı");
            kalanDakika[i] = energys[i].kalanDakika;
        }
        for (int i = 0; i < energys.Count - 1; i++)
        {
            for (int j = i; j < energys.Count; j++)
            {
                // >(büyük) işareti <(küçük ) olarak değiştirilirse büyükten küçüğe sıralanır
                if (kalanDakika[i] > kalanDakika[j])
                {
                    tempLastButtonClick = lastbuttonClick[j];
                    tempKalanDakika = kalanDakika[j];
                    kalanDakika[j] = kalanDakika[i];
                    lastbuttonClick[j] = lastbuttonClick[i];
                    lastbuttonClick[i] = tempLastButtonClick;
                    kalanDakika[i] = tempKalanDakika;
                }
            }

        }
        for (int i = 0;i < energys.Count;i++)
        {
            if(kalanDakika[i] > 0)
            {
               lastbuttonclikc_ =  lastbuttonClick[i];
                break;
            }
        }
        return lastbuttonclikc_;
    }
}

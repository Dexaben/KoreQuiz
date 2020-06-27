
using UnityEngine;
public class levelsonuBilgiler : MonoBehaviour {
    [SerializeField] System.Collections.Generic.List<Sprite> RUTBE_IMAGES ;
    [SerializeField] UnityEngine.UI.Image profilePicture;
    [SerializeField] Sprite[] PpImages = new Sprite[16];
    [SerializeField] UnityEngine.UI.Image RUTBE;
    [SerializeField] UnityEngine.UI.Text RUTBE_TEXT;
    [SerializeField] UnityEngine.UI.Text SKOR;
    [SerializeField] UnityEngine.UI.Text PARA;


    void OnEnable () {
        profilePicture = this.gameObject.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>();
        profilePicture.sprite = PpImages[PlayerPrefs.GetInt("ProfilePicture")];
        if(RUTBE_IMAGES.Count <3)
        {
            RUTBE_IMAGES.AddRange(Resources.LoadAll<Sprite>("rutbeResimleri"));
        }
   
        RUTBE = this.gameObject.transform.GetChild(4).gameObject.GetComponent<UnityEngine.UI.Image>();
        RUTBE_TEXT = this.gameObject.transform.GetChild(5).gameObject.GetComponent<UnityEngine.UI.Text>();
        SKOR = this.gameObject.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>();
        PARA = this.gameObject.transform.GetChild(3).gameObject.GetComponent<UnityEngine.UI.Text>();
        UnityEngine.UI.Text USERNAME = this.gameObject.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>();
        USERNAME.text = PlayerPrefs.GetString("Username");
        if (PlayerPrefs.GetInt("AnaSkor") < 500)
        {
            Destroy(RUTBE);
            RUTBE_TEXT.text = "";
        }
    }
    void Update()
    {
        SKOR.text ="ANA SKOR: "+ PlayerPrefs.GetInt("AnaSkor");
        PARA.text = "PARA: " + PlayerPrefs.GetInt("Para");
        rutbeSync();
    }
    public void rutbeSync()
    {
        if (PlayerPrefs.GetInt("AnaSkor") < 500)
        {
            Destroy(RUTBE);
            RUTBE_TEXT.text = "";
        }
        if (PlayerPrefs.GetInt("AnaSkor") >= 500 && PlayerPrefs.GetInt("AnaSkor") < 10000)
        {
            if (!PlayerPrefs.HasKey("rutbe_acemi"))
            {
                GameObject rutbeAL = Resources.Load("GameObject/rutbeAL") as GameObject;
                rutbeAL.GetComponent<rutbeUyari>().currentRutbe = "acemi";
                Destroy(Instantiate(rutbeAL), 3f);
                PlayerPrefs.SetInt("rutbe_acemi", 1);
            }
            for (int i = 0; i < RUTBE_IMAGES.Count; i++)
            {
                if (RUTBE_IMAGES[i].name == "acemi")
                {
                    RUTBE.sprite = RUTBE_IMAGES[i];
                    break;
                }
            }
            RUTBE_TEXT.text = "ACEMİ";
        }
        if (PlayerPrefs.GetInt("AnaSkor") >= 10000 && PlayerPrefs.GetInt("AnaSkor") < 35000)
        {
            if (!PlayerPrefs.HasKey("rutbe_tecrubeli"))
            {
                GameObject rutbeAL = Resources.Load("GameObject/rutbeAL") as GameObject;
                rutbeAL.GetComponent<rutbeUyari>().currentRutbe = "tecrubeli";
                Destroy(Instantiate(rutbeAL), 3f);
                PlayerPrefs.SetInt("rutbe_tecrubeli", 1);
            }
            for (int i = 0; i < RUTBE_IMAGES.Count; i++)
            {
                if (RUTBE_IMAGES[i].name == "tecrubeli")
                {
                    RUTBE.sprite = RUTBE_IMAGES[i];
                    break;
                }
            }
            RUTBE_TEXT.text = "TECRÜBELİ";
        }
        if (PlayerPrefs.GetInt("AnaSkor") >= 35000)
        {
            if (!PlayerPrefs.HasKey("rutbe_efsanevi"))
            {
                GameObject rutbeAL = Resources.Load("GameObject/rutbeAL") as GameObject;
                rutbeAL.GetComponent<rutbeUyari>().currentRutbe = "efsanevi";
                Destroy(Instantiate(rutbeAL), 3f);
                PlayerPrefs.SetInt("rutbe_efsanevi", 1);
            }
            for (int i = 0; i < RUTBE_IMAGES.Count; i++)
            {
                if (RUTBE_IMAGES[i].name == "efsanevi")
                {
                    RUTBE.sprite = RUTBE_IMAGES[i];
                    break;
                }
            }
            RUTBE_TEXT.text = "EFSANEVİ";
        }
    }
}

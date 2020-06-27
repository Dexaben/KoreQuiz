using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public class levelselectscript : MonoBehaviour {
    [Header ("Prefabs")]
    public Transform image_transform;
    [SerializeField] Text ToplamSkor_Text;
    [SerializeField] Text Para_Text;
    [SerializeField] Text Elmas_Text;
    [SerializeField] GameObject locationpin;
    [SerializeField] GameObject alphabet;
    [SerializeField] RectTransform cam;
    [SerializeField] GameObject loadingscreen;
    [SerializeField] Text loadingscreen_hint;
    [SerializeField] public GameObject enerjidoldur;
    [SerializeField] System.Collections.Generic.List<Button> level_buttons;
    [SerializeField] private System.Collections.Generic.List<string> hintList = new System.Collections.Generic.List<string>(10);
    [SerializeField] private System.Collections.Generic.List<Sprite> PpImages;
    [SerializeField] private Image Pp;
    [SerializeField] private Image RUTBE;
    [SerializeField] private Text RUTBE_TEXT;
    [SerializeField] System.Collections.Generic.List<Sprite> RUTBE_IMAGES = new System.Collections.Generic.List<Sprite>(3);
    [SerializeField] private Text USERNAME;

    public GameObject Go;

    [Header ("Level_Camera_Locations")]
    public levelcamera_transform[] level_transform;

    public bool loading = false;

    private BannerView bannerAD;
    AsyncOperation async;
    private GameManager gameManager;


    void OnEnable()
    {
        loading = false;
        RUTBE_IMAGES.AddRange(Resources.LoadAll<Sprite>("rutbeResimleri"));
        for (int i = 0; i < 10; i++)
        {
            hintList.Add("");
        }
        hintList[0] = "Oyunumuzu Beğendin mi?\nHemen oyla!";
        hintList[1] = "Görevleri yaparak para kazanabileceğini biliyormuydun?";
        hintList[2] = "En yüksek skorları yapmak istiyorsan en kısa zamanda, en az yanlış miktarıyla bölümü bitirmelisin!";
        hintList[3] = "Elmaslar çok nadir ve çok değerlidir.\nElmaslar sayesinde bölüme devam edebilir, enerjilerini doldurabilirsin!";
        hintList[4] = "Oyun sonunda sandıkların tamamını açmak için video izleyebilirsin.\nSandıklarda seni sürpriz hediyeler bekliyor!";
        hintList[5] = "Elmaslar çok değerlidir! Bölümleri tamamlayarak ve sandıklardan elmas kazanabilirsin!\nHer beş bölümde bir elmas kazanırsın!";
        hintList[6] = "Oyunda arkadaşlarından yüksek skorlar elde et, onlara ne kadar iyi olduğunu göster!";
        hintList[7] = "Beklemekten sıkıldın mı?\nHemen markete git, sınırsız enerji satın al!";
        hintList[8] = "Bilemediğin bir soruyla karşılaştın?\nSorun değil! 1500KP karşılığında istediğin soruyu geçebilirsin!";
        hintList[9] = "Oyun ile alakalı görüşlerini bildir!\nBu bize daha iyi bir oyun geliştirmek için yardımcı olacaktır!";
        if(PlayerPrefs.HasKey("level_available46"))
        {
            GameObject rutbeAL = Resources.Load("GameObject/OyunSonuEkrani") as GameObject;
            Instantiate(rutbeAL);
        }
        //Bolumdeki prefabların cekilmesi.
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        USERNAME.text = PlayerPrefs.GetString("Username");
        Pp.sprite = PpImages[PlayerPrefs.GetInt("ProfilePicture")];
        if(PlayerPrefs.GetInt("AnaSkor") < 500)
        {
            Destroy(RUTBE);
            RUTBE_TEXT.text = "";
        }
        if(PlayerPrefs.GetInt("AnaSkor") >= 500 && PlayerPrefs.GetInt("AnaSkor") < 10000)
        {
            if(!PlayerPrefs.HasKey("rutbe_acemi"))
            {
                GameObject rutbeAL = Resources.Load("GameObject/rutbeAL") as GameObject;
                rutbeAL.GetComponent<rutbeUyari>().currentRutbe = "acemi";
                Destroy(Instantiate(rutbeAL), 3f);
                PlayerPrefs.SetInt("rutbe_acemi",1);
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
        EnergyTimes();
        if(PlayerPrefs.GetInt("current_level") <45)
        {
            cam.anchoredPosition = new Vector2(level_transform[PlayerPrefs.GetInt("current_level")].x, level_transform[PlayerPrefs.GetInt("current_level")].y);
            
        }
        else
        {
            cam.anchoredPosition = new Vector2(level_transform[44].x, level_transform[44].y);
        }
        for (int i = 0; i < Go.transform.childCount - 1; i++)
            {
            level_buttons.Add(Go.transform.GetChild(i).gameObject.GetComponent<Button>());
            }
       
        //aktif olabilecek prefablarin kapatilmasi.
        loadingscreen.SetActive(false);
        enerjidoldur.SetActive(false);
        for(int i = 0;i<45;i++)
        {
            if (PlayerPrefs.HasKey("level_available"+(i+1)))
            {
                level_buttons[i].interactable = true;
                level_buttons[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.green;
                locationpin.transform.SetParent(level_buttons[i].gameObject.transform,false);
                if(PlayerPrefs.HasKey("alphabe"+i))
                {
                    alphabet.GetComponent<alphabetscript>().a = PlayerPrefs.GetString("alphabe" + i);
                    if (PlayerPrefs.GetString("alphabe" + i) == "threeStar")
                    {
                        alphabet.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                    }
                    else { 
                        alphabet.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                    }
                    Instantiate(alphabet, level_buttons[i-1].transform,false);
                }
              
            }
            else
            {
                level_buttons[i].interactable = false;
                level_buttons[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
               
            }
        }
        if(PlayerPrefs.HasKey("level_available46"))
        {
             level_buttons[44].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.gray;
             locationpin.SetActive(false);
              if(PlayerPrefs.HasKey("alphabe"+45))
                {
                    alphabet.GetComponent<alphabetscript>().a = PlayerPrefs.GetString("alphabe" + 45);
                    if (PlayerPrefs.GetString("alphabe" + 45) == "threeStar")
                    {
                        alphabet.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                    }
                    else { 
                        alphabet.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                    }
                    Instantiate(alphabet, level_buttons[44].transform,false);
                }
        }
        if(PlayerPrefs.GetInt("current_level") == 0)
        {
            locationpin.transform.SetParent(level_buttons[0].gameObject.transform,false);
            if (PlayerPrefs.HasKey("alphabe" + 0))
            {
                alphabet.GetComponent<alphabetscript>().a = PlayerPrefs.GetString("alphabe" + 0);
                alphabet.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                Instantiate(alphabet, level_buttons[0].transform,false);
            }
        }
        level_buttons[0].interactable = true;
        level_buttons[0].transform.GetChild(0).GetComponent<Image>().color = Color.green;
        //AnaSkorun cekilmesi
        if (ToplamSkor_Text.gameObject && PlayerPrefs.HasKey("AnaSkor"))
        {
            ToplamSkor_Text.text = "Skor : " + PlayerPrefs.GetInt("AnaSkor");
        }
        else ToplamSkor_Text.text = "Skor verisi alınamadı.";

        //paranin cekilmesi
        if (Para_Text.gameObject && PlayerPrefs.HasKey("Para"))
        {
            Para_Text.text = "KP : " + PlayerPrefs.GetInt("Para");
         
        }
        else Para_Text.text = "Para verisi alınamadı.";
        //elmasin cekilmesi
        if (Elmas_Text.gameObject && PlayerPrefs.HasKey("Elmas"))
        {
            Elmas_Text.text = "ELMAS : " + PlayerPrefs.GetInt("Elmas");
        }
        else Elmas_Text.text = "Elmas verisi alınamadı.";

        //banner gosterimi.

            HideBanner();

            RequestBanner();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideBanner();
            Debug.Log("banner kaldırıldı");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            EnergyTimes();
        }
        enerjidoldur.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetString("lastbuttonclick" + i) != "0")
            {
                enerjidoldur.SetActive(true);
            }

        }
    }
    public void HideBanner()
    {
        try
        {
            bannerAD.Destroy();
        }
        catch
        {

        }
        
    }

    public void RequestBanner()
    {
#if UNITY_ANDROID
        string banner_ID = "ca-app-pub-6647722756286983/8239076778";
        bannerAD = new BannerView(banner_ID, AdSize.SmartBanner, AdPosition.Bottom);
       
        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerAD.LoadAd(adRequest);
#endif
    }
    public void SceneChangee(string name)
    {
        loading = true;
        if(PlayerPrefs.HasKey("UnliminatedEnergy"))
        {
            StartCoroutine(LoadingScreen(name));
            return;
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (PlayerPrefs.GetString("lastbuttonclick" + i) == "0")
                {
                    ulong lastButtonClick = (ulong)System.DateTime.Now.Ticks;
                    PlayerPrefs.SetString("lastbuttonclick" + i, lastButtonClick.ToString());
               
                    StartCoroutine(LoadingScreen(name));
                    return;
                }
            }
            Instantiate(Resources.Load("GameObject/EnerjiYok") as GameObject);
        }
    }
    void EnergyTimes()
    {
        Unity.Notifications.Android.AndroidNotificationCenter.CancelAllNotifications();
        int temp = 0;
        for (int i = 0;i<3;i++)
        {
            if(PlayerPrefs.GetString("lastbuttonclick" + i) != "0")
            {
                ulong diff = ((ulong)System.DateTime.Now.Ticks - ulong.Parse(PlayerPrefs.GetString("lastbuttonclick" + i)));
                ulong m = diff / System.TimeSpan.TicksPerMillisecond;
                float secondsLeft = (float)(3600000f - m) / 1000.0f;
                if(((int)secondsLeft / 60) > temp)
                {
                    temp = ((int)secondsLeft / 60);
                }
            }
        }
        if(temp <=0)
        {
            return;
        }
        else
        {
            gameManager.PushNotification(temp+1);
        }
          
       
    }
    public void SceneIndex(int SceneIndex) //sahneye girerken sahnenin numarasini gm'ye aktarir.
    {
        gameManager.currentLevel = SceneIndex;
    }
    System.Collections.IEnumerator LoadingScreen(string index) //loading screen.
    {
        loadingscreen.SetActive(true);
        if (!loadingscreen_hint)
            loadingscreen_hint = loadingscreen.transform.GetChild(2).gameObject.GetComponent<Text>();
        int rand = UnityEngine.Random.Range(0, hintList.Count);
        loadingscreen_hint.text = hintList[rand];
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
        Slider slider = loadingscreen.transform.GetChild(0).gameObject.GetComponent<Slider>();
        HideBanner();
        Debug.Log("banner kaldırıldı");
        EnergyTimes();
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
        
    }
}

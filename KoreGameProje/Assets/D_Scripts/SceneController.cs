using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
public class SceneController : MonoBehaviour {
    [Header("Timer")]
        private float Timer=120;
        private float timer_start;
        private bool sureDevam = true;
        private bool doOnce = false;
        [SerializeField] private Text Time_Text;
        [SerializeField] private Image Time_Slider;

    [Header("Objeler")]
    [SerializeField] private Image[] Can_Images;
    [SerializeField] private GameObject LevelOver_Canvas;
    [SerializeField] private Text SoruPuani_Text;
    [SerializeField] private Text SurePuani_Text;
    [SerializeField] private Text CanPuani_Text;
    [SerializeField] private Text Altin_Text;
    [SerializeField] private GameObject[] sorular;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject DevamEtMenu;
    [SerializeField] private Text KalanSoru_Text;
    [SerializeField] private GameObject sandikAcma;
    [SerializeField] private GameObject dogruCanvas;
    [SerializeField] private GameObject yanlisCanvas;
    [SerializeField] private Image[] barlar;
    [SerializeField] private GameObject hazirolObject;
    [SerializeField] private Button x2button;
    [SerializeField] private Text Skor_Text;
    [SerializeField] private skorslider skslider;
    [SerializeField] private Sprite[] offon_music;
    public string levelalphabe;

    [Header("Sistem")]
    public int Can = 3;
    public int Soru = 14;
    public int Dogru_Soru = 0;
    public int Yanlis_Soru = 0;
  
    int a,b;

    int karteslestirme = 0;

    private RewardBasedVideoAd rewardBasedVideo;

    void OnEnable()
    {
        
        Destroy(Instantiate(hazirolObject), 3);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        LevelOver_Canvas.SetActive(false);
        DevamEtMenu.SetActive(false);
        karteslestirme = 0;
        Invoke("bekle", 3);
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
       this.rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
       this.rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        this.RequestRewardBasedVideo();
    }
    void OnDisable()
    {
        this.rewardBasedVideo.OnAdFailedToLoad -= HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
    }
  
    void bekle()
    {
        SonrakiSoru();
        skslider.skorUpdate(0);
        timer_start = Timer;
        sureDevam = true;
        doOnce = false;
    }
  
    public void music(Image musicImage)
    {
        if (PlayerPrefs.HasKey("music"))
        {
            if (PlayerPrefs.GetInt("music") == 0)
            {
                musicImage.sprite = offon_music[0];
                PlayerPrefs.SetInt("music", 1);
                gameManager.GetComponent<AudioSource>().mute = false;
            }
            else
            {
                musicImage.sprite = offon_music[1];
                PlayerPrefs.SetInt("music", 0);
                gameManager.GetComponent<AudioSource>().mute = true;
            }
        }
        else
        {
            musicImage.sprite = offon_music[1];
            PlayerPrefs.SetInt("music", 0);
            gameManager.GetComponent<AudioSource>().mute = true;
        }
    }
    public void devamET() //can hakkı bittikten sonra video izle ve doldur butonuna tıkladıktan sonra çalıştırılan fonksiyon
    {
        string HtmlText = gameManager.GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "İnternet bağlantısı yok.";
            Destroy(Instantiate(dogrucevap), 2.3f);
        }
        else if (!HtmlText.Contains("schema.org/WebPage"))
        {
            //Redirecting since the beginning of googles html contains that 
            //phrase and it was not found
        }
        else
        {
            if (this.rewardBasedVideo.IsLoaded())
            {
                this.rewardBasedVideo.Show();
            }
            else
            {
                GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
                dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Video izlemeye hazır değil. Tekrar deneyin.";
                Destroy(Instantiate(dogrucevap), 2.3f);
                if(gameManager.Interstitial_Control())
                {
                    gameManager.Display_Interstitial();
                    sureDevam = true;
                    doOnce = false;
                    Can = 3;
                    Timer = Timer + 40;
                    if (Timer >= 120)
                    {
                        Timer = 120;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        Can_Images[i].enabled = true;
                    }
                    DevamEtMenu.SetActive(false);
                    SonrakiSoru();
                }
            }
        }
    }
    private void RequestRewardBasedVideo()
    {
        string adUnitId = "ca-app-pub-6647722756286983/3513718924";

        AdRequest request = new AdRequest.Builder().Build();
    
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }


    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        sureDevam = true;
        doOnce = false;
        Can = 3;
        Timer = Timer+40;
        if (Timer >= 120)
        {
            Timer = 120;
        }
        for (int i = 0; i < 3; i++)
        {
            Can_Images[i].enabled = true;
        }
        DevamEtMenu.SetActive(false);
        SonrakiSoru();
    }
    public void PAUSE(GameObject go)
    {
        if(!go.gameObject.activeInHierarchy)
        {
            go.gameObject.SetActive(true);
            go.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Skor : " + (Dogru_Soru * 5)+"\nKalan Soru Sayısı : "+(15-(Dogru_Soru + Yanlis_Soru));
          

            if (PlayerPrefs.HasKey("music"))
            {
                if (PlayerPrefs.GetInt("music") == 0)
                {
                    go.transform.GetChild(4).gameObject.GetComponent<Image>().sprite = offon_music[1];
                }
                else
                {
                    go.transform.GetChild(4).gameObject.GetComponent<Image>().sprite = offon_music[0];
                }
            }
        }
        else
        {
            go.gameObject.SetActive(false);
        }
    }
    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        this.RequestRewardBasedVideo();
    }
    public void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        this.RequestRewardBasedVideo();
    }
    private void GameOver() //can veya süre bitince çalışan fonksiyon
    {
        if (GameObject.FindGameObjectWithTag("soru"))
        {
            Destroy(GameObject.FindGameObjectWithTag("soru"));
        }
        sureDevam = false;
        KalanSoru_Text.text= "KALAN SORU (15/" + (15-(Dogru_Soru + Yanlis_Soru)) + ")";
        DevamEtMenu.SetActive(true);
    }
    public void x2skor()
    {
        string HtmlText = gameManager.GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "İnternet bağlantısı yok.";
            Destroy(Instantiate(dogrucevap), 2.3f);
        }
        else if (!HtmlText.Contains("schema.org/WebPage"))
        {
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "İnternet bağlantısı yok.";
            Destroy(Instantiate(dogrucevap), 2.3f);
        }
        else
        {
            if (gameManager.Interstitial_Control())
            {
                gameManager.Display_Interstitial();
                if (x2button.interactable != false)
                {
                    ParaArttir((Dogru_Soru * 80) + ((int)Timer * 10));
                    int temp = PlayerPrefs.GetInt("Para");
                    PlayerPrefs.SetInt("Para", temp += ((Dogru_Soru * 80) + ((int)Timer * 10)));
                    Altin_Text.text = "(x2) +KP : " + ((Dogru_Soru * 80) + ((int)Timer * 10)) * 2;
                    x2button.interactable = false;
                    LevelOver_Canvas.GetComponent<Animator>().enabled = false;
                }
            }
            else
            {
                GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
                dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Video izlemeye hazır değil. Tekrar deneyin.";
                Destroy(Instantiate(dogrucevap), 2.3f);
            }
        }
    }
    
    private void LevelOver() //level bitince calısan fonksiyon
    {
        Instantiate(sandikAcma); //sandık acma objesi canlandırır
        if (GameObject.FindGameObjectWithTag("soru"))
        {
            Destroy(GameObject.FindGameObjectWithTag("soru"));
        }
        sureDevam = false;
        LevelOver_Canvas.SetActive(true);
        SoruPuani_Text.text = "SORU PUANI (15/" + Dogru_Soru + ") : " + Dogru_Soru * 5;
        SurePuani_Text.text = "SÜRE PUANI ("+(int)Timer+" sn) : "+ (int)Timer*8;
        CanPuani_Text.text = "CAN PUANI (3/" + Can + ") : " + Can * 20;
        Skor_Text.text = "+SKOR : " +(Dogru_Soru * 5 + (int)Timer * 8 + Can * 20);
        Altin_Text.text = "+KP : " + ((Dogru_Soru *80) + ((int)Timer *10));
        skslider.skorUpdate(Dogru_Soru * 5);
        gameManager.LevelComplated((Dogru_Soru * 5 + (int)Timer * 8 + Can * 20),levelalphabe);
       
        x2button.interactable = true;
        ParaArttir((Dogru_Soru *80) + ((int)Timer *10));
        if (Dogru_Soru >= 15) //15 soruda 15 dogru yapınca calıstırılacak fonksiyon
        {
            if (!PlayerPrefs.HasKey("HicYanlisYapmadan"))
            {
                PlayerPrefs.SetInt("HicYanlisYapmadan", 1);
                GameObject dogrucevap = Resources.Load("GameObject/BasarimUyari") as GameObject;
                dogrucevap.GetComponent<basarimUyari>().currentBasarim = "basarim7";
                dogrucevap.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler bölümdeki bütün soruları doğru cevaplayarak başarım kazandınız!";
                Destroy(Instantiate(dogrucevap), 2.3f);
            }   
        }
        if(Yanlis_Soru >= 10)
        {
            if(!PlayerPrefs.HasKey("HepYanlisYaparak"))
            {
                PlayerPrefs.SetInt("HepYanlisYaparak", 1);
                GameObject dogrucevap = Resources.Load("GameObject/BasarimUyari") as GameObject;
                dogrucevap.GetComponent<basarimUyari>().currentBasarim = "basarim8";
                dogrucevap.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler bölümdeki sorulardan en az 10 tanesini yanlış yaparak başarım kazandınız!";
                Destroy(Instantiate(dogrucevap), 2.3f);
            }
        }
        if(PlayerPrefs.HasKey("Para")==true) //dogru sorunun 200 katı kadar parayı para degıskenıne kaydeder.
        {
            int temp = PlayerPrefs.GetInt("Para");
            PlayerPrefs.SetInt("Para", temp += ((Dogru_Soru * 80) + ((int)Timer * 10)));
        }
        else
        {
            int temp = 0;
            PlayerPrefs.SetInt("Para", temp += ((Dogru_Soru * 80) + ((int)Timer * 10)));
        }   
        if(PlayerPrefs.GetInt("AnaSkor") >= 500 && !PlayerPrefs.HasKey("AnaSkor500"))
        {

            GameObject dogrucevap = Resources.Load("GameObject/BasarimUyari") as GameObject;
            dogrucevap.GetComponent<basarimUyari>().currentBasarim = "basarim2";
            dogrucevap.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler 500 skor elde ederek başarım kazandınız!";
            Destroy(Instantiate(dogrucevap), 2.3f);
            PlayerPrefs.SetInt("AnaSkor500", 1);
        }
        if (PlayerPrefs.GetInt("AnaSkor") >= 3000 && !PlayerPrefs.HasKey("AnaSkor3000"))
        {
            GameObject dogrucevap = Resources.Load("GameObject/BasarimUyari") as GameObject;
            dogrucevap.GetComponent<basarimUyari>().currentBasarim = "basarim3";
            dogrucevap.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler 3000 skor elde ederek başarım kazandınız!";
            Destroy(Instantiate(dogrucevap), 2.3f);
            PlayerPrefs.SetInt("AnaSkor3000", 1);
        }
        if (PlayerPrefs.GetInt("AnaSkor") >= 10000 && !PlayerPrefs.HasKey("AnaSkor10000"))
        {
            GameObject dogrucevap = Resources.Load("GameObject/BasarimUyari") as GameObject;
            dogrucevap.GetComponent<basarimUyari>().currentBasarim = "basarim4";
            dogrucevap.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler 10000 skor elde ederek başarım kazandınız!";
            Destroy(Instantiate(dogrucevap), 2.3f);
            PlayerPrefs.SetInt("AnaSkor10000", 1);
        }
        if (PlayerPrefs.GetInt("AnaSkor") >= 35000 && !PlayerPrefs.HasKey("AnaSkor35000"))
        {
            GameObject dogrucevap = Resources.Load("GameObject/BasarimUyari") as GameObject;
            dogrucevap.GetComponent<basarimUyari>().currentBasarim = "basarim5";
            dogrucevap.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler 35000 skor elde ederek başarım kazandınız!";
            Destroy(Instantiate(dogrucevap), 2.3f);
            PlayerPrefs.SetInt("AnaSkor35000", 1);
        }
        
    }
    public void SonrakiSoru() //rastgele sonraki soruyu cagıran fonksiyon
    {
        a = Random.Range(0, 7);
        while(a == b)
        {
            a = Random.Range(0, 7);
        }
        b = a;
        if(a == 6)
        {
            karteslestirme += 1;
            if (karteslestirme > 1)
            {
                if (GameObject.FindGameObjectWithTag("soru"))
                {
                    Destroy(GameObject.FindGameObjectWithTag("soru"));
                }
                SonrakiSoru();
                
                return;
            }
        }
        if (a == 5)
        {
            gameManager.GetComponent<AudioSource>().mute = true;
        }
        else
        {
            if(PlayerPrefs.GetInt("music")==1)
            {
                gameManager.GetComponent<AudioSource>().mute = false;
            }
  
        }
        if(GameObject.FindGameObjectWithTag("soru"))
        {
            Destroy(GameObject.FindGameObjectWithTag("soru"));
            Destroy(GameObject.FindGameObjectWithTag("soru"));
        }

        Instantiate(sorular[a]);
    }
    public void bar_dogru(int soruDeger) //soru dogru cevaplanınca calıstırılır
    {
        Dogru_Soru += 1;
        barlar[soruDeger].color = Color.green; //bardaki sorunun sirasindaki textureyi yesil yapar.
        if (Soru > 0)
        {
           
            Destroy(Instantiate(dogruCanvas), 2f); //dogru cevap canvasını 0.6 saniyeligine ınstant eder.
            Invoke("SonrakiSoru", 2);
        }
        else
        {
            Destroy(Instantiate(dogruCanvas), 2f);
            Invoke("LevelOver", 2f);
        }
        skslider.skorUpdate(Dogru_Soru * 5);
    }
     public void SceneChange(string scenename) //sahne degıstırme fonksıyonu
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scenename);
    }
    public void bar_yanlis(int soruDeger)
    {
        Yanlis_Soru += 1;
        barlar[soruDeger].color = Color.red; //bardaki sorunun sirasindaki textureyi kırmızı yapar.
        Can -= 1;
        if (Soru > 0)
        {
         
            Destroy(Instantiate(yanlisCanvas), 2f); //yanlış cevap canvasını 0.6 saniyeligine ınstant eder.
            for (int i = 2;i >= Can ; i--) //can imagelerini can degiskenine gore kapatır.
            {
                Can_Images[i].enabled = false;
            }
            if(Can <=0)
            {
                    Invoke("GameOver", 2f);
            }
            if(Can > 0)
            {
                Invoke("SonrakiSoru", 2);
            }
          
        }
        else
        {
            Destroy(Instantiate(yanlisCanvas), 2f);
            Invoke("LevelOver", 2f);
        }

    }
    void Update () { //süreyi fps boyunca düsürür ve canı kontrol eder .

		if(Timer >= 0.0f && sureDevam)
        {
            Timer -= Time.deltaTime;
            Time_Text.text = ((int)Timer).ToString();
            Time_Slider.fillAmount = Timer/timer_start;
        }
        else if(Timer <= 0.0f && !doOnce)
        {
            sureDevam = false;
            doOnce = true;
            Time_Text.text = "0";
            Time_Slider.fillAmount = 0;
            Invoke("GameOver", 0.3f);
        }
	}
    void ParaArttir(int mny)
    {
        GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
        dogrucevap.GetComponent<parascript>().incPara = mny;
        Destroy(Instantiate(dogrucevap), 3.1f);
    }
   
}

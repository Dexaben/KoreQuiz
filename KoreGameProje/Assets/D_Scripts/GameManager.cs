using Unity.Notifications.Android;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Net;
public class GameManager : MonoBehaviour {
    [Header("Değişkenler")]
    public int currentLevel; //save sistemi icin gerekli oynanılan bölümü ceken degisken
    public int anaSkor; //skor tutan degısken
    private static GameObject instance;

    //public string APP_ID = "ca-app-pub-6647722756286983~4945671350";
    private InterstitialAd interstitialAd;
   

    void Start()
    {
        Application.targetFrameRate = 60;
        MobileAds.Initialize("ca-app-pub-6647722756286983~4945671350"); 

        RequestInterstitial();
        CreateNotification();
        if (!PlayerPrefs.HasKey("Elmas"))
            PlayerPrefs.SetInt("Elmas",5);
        if (!PlayerPrefs.HasKey("music"));
            PlayerPrefs.SetInt("music", 1);
        if (!PlayerPrefs.HasKey("current_level"))
            PlayerPrefs.SetInt("currentLevel", 0);
        if (!PlayerPrefs.HasKey("level_available"))
            PlayerPrefs.SetInt("level_available", 1);
        if (!PlayerPrefs.HasKey("AnaSkor"))
            PlayerPrefs.SetInt("AnaSkor", 0);
        if (!PlayerPrefs.HasKey("Para"))
            PlayerPrefs.SetInt("Para", 0);
        if (!PlayerPrefs.HasKey("lastbuttonclick0"))
            PlayerPrefs.SetString("lastbuttonclick0", "0");
        if (!PlayerPrefs.HasKey("lastbuttonclick1"))
            PlayerPrefs.SetString("lastbuttonclick1","0");
        if (!PlayerPrefs.HasKey("lastbuttonclick2"))
            PlayerPrefs.SetString("lastbuttonclick2", "0");

        DontDestroyOnLoad(gameObject);  //GameManager dont destroy komutları
        if (instance == null)
        {
            instance = gameObject;
        }
        else
            Destroy(gameObject);
        anaSkor = PlayerPrefs.GetInt("AnaSkor"); //gamemanager değişkenini savelenmiş anaskora eşitler
    }


    //======================================BANNER===========================================================================================
    
    //=================================================================================================================================================



    //======================================INTERSTITIAL===========================================================================================
    void RequestInterstitial()
    {
#if UNITY_ANDROID
        string interstitialID = "ca-app-pub-6647722756286983/5875609647";
        interstitialAd = new InterstitialAd(interstitialID);
        
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
#endif
    }
    public void Display_Interstitial()
    {
        interstitialAd.Show();
        RequestInterstitial();
    }
    public bool Interstitial_Control()
    {
        if (interstitialAd.IsLoaded())
        {
            return true;
        }
        else return false;
    }
    //=================================================================================================================================================







    //========================================NOTIFICATION==============================================================================================
    public void CreateNotification()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
    }
    public void PushNotification(int time)
    {
        var notification = new AndroidNotification();
        notification.SmallIcon = "icon_0";
        notification.Style = NotificationStyle.BigTextStyle;
        notification.LargeIcon = "icon_1";
        notification.Title = "ENERJİLERİN DOLDU!";
        notification.Text = "Tüm enerjilerin doldu oynamaya hazırsın!";
        notification.Color = Color.cyan;
        notification.FireTime = System.DateTime.Now.AddMinutes(time);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
    //========================================================================================================================================================
    public string GetHtmlFromUri(string resource) //internet kontrol
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }







    public void LevelComplated(int skor,string a) //level bitince çalışan fonksiyon 
    {
        PlayerPrefs.SetInt("level"+currentLevel, true ? 1 : 0); //current level true olur.
        PlayerPrefs.SetInt("level_available" + (currentLevel + 1), true ? 1 : 0); //bir sonraki levelin aktif olması için bu levelin indexine 1 ekleyip level_Available degiskeni  kaydedilir.
        if(PlayerPrefs.HasKey("alphabe"+currentLevel))
        {
            int a_temp= 0;
            if(a == "oneStar")
            {
                a_temp = 1;
            }
            if(a == "twoStar")
            {
                a_temp = 2;
            }
            if(a == "threeStar")
            {
                a_temp = 3;
            }

            int levela_temp = 0;
            if(PlayerPrefs.GetString("alphabe"+currentLevel) == "oneStar")
            {
                levela_temp = 1;
            }
            if(PlayerPrefs.GetString("alphabe"+currentLevel) == "twoStar")
            {
                levela_temp = 2;
            }
             if(PlayerPrefs.GetString("alphabe"+currentLevel) == "threeStar")
            {
                levela_temp = 3;
            }
            

            if(levela_temp >= a_temp)
            {
                if(levela_temp == 3)  
                PlayerPrefs.SetString("alphabe"+currentLevel,"threeStar");
                  if(levela_temp == 2)  
                PlayerPrefs.SetString("alphabe"+currentLevel,"twoStar");
                   if(levela_temp == 1)  
                PlayerPrefs.SetString("alphabe"+currentLevel,"oneStar");
            }
            else
            {
                PlayerPrefs.SetString("alphabe"+currentLevel,a);
            }


              if(a =="noStar")
            {
                PlayerPrefs.SetString("alphabe"+currentLevel,"noStar");
            }
        }
        else
        {
            PlayerPrefs.SetString("alphabe"+currentLevel, a);
        }
    
        if (!PlayerPrefs.HasKey("current_level" + currentLevel))
        {
            PlayerPrefs.SetInt("current_level", currentLevel);
            
        }        
        if(currentLevel % 5 == 0)
        {
            int temp = PlayerPrefs.GetInt("Elmas");
            PlayerPrefs.SetInt("Elmas", temp += 1);
        }
       
        int tempskor = PlayerPrefs.GetInt("AnaSkor");
        SkorArttir(skor);
        PlayerPrefs.SetInt("AnaSkor", tempskor+=skor); //toplanan skor anaskora aktarılır ve kaydedilir.
        anaSkor = PlayerPrefs.GetInt("AnaSkor");

        if (PlayerPrefs.HasKey("level_available" + 46) && !PlayerPrefs.HasKey("levelavaible46")) 
        {
            GameObject dogrucevap = Resources.Load("GameObject/BasarimUyari") as GameObject;
            dogrucevap.GetComponent<basarimUyari>().currentBasarim = "basarim6";
            dogrucevap.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler bölümleri başarıyla tamamladınız ve başarım kazandınız!";
            Destroy(Instantiate(dogrucevap), 2.3f);
            PlayerPrefs.SetInt("levelavaible46", 1);
           Invoke("OyunSonuEKRANI",2.4F);
        }
        if (PlayerPrefs.HasKey("level_available"+11) && !PlayerPrefs.HasKey("levelavaible11"))
        {
            GameObject dogrucevap = Resources.Load("GameObject/BasarimUyari") as GameObject;
            dogrucevap.GetComponent<basarimUyari>().currentBasarim = "basarim9";
            dogrucevap.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler 10 bölümü başarıyla tamamlayarak başarım kazandınız!";
            Destroy(Instantiate(dogrucevap), 2.3f);
            PlayerPrefs.SetInt("levelavaible11", 1);
        }
    }
    void OyunSonuEKRANI()
    {
        GameObject oyunsonu = Resources.Load("GameObject/OyunSonuEkrani") as GameObject;
        Instantiate(oyunsonu);
    }
    void SkorArttir(int skr)
    {
        GameObject dogrucevap = Resources.Load("GameObject/Skor") as GameObject;
        dogrucevap.GetComponent<skorscript>().incSkor = skr;
        Destroy(Instantiate(dogrucevap), 3.1f);
    }
}

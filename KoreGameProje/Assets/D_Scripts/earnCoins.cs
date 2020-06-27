using UnityEngine;
using UnityEngine.UI;
public class earnCoins : MonoBehaviour {
    [SerializeField] private System.Collections.Generic.List<GameObject> gorevObjects; //gorev objeleri
    [SerializeField] private GameObject gO; //gorev  objelerinin contenti

    [SerializeField] private Button videobtn;
    GameManager gm;
    private bool isFocus = false;
    private bool isProcessing = false;

    void OnEnable() {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


        if (PlayerPrefs.GetInt("watchvideotick") > 4)
        {
            videobtn.interactable = false;
            videoizlemehazirmi();
        }
        else
        {
            videobtn.interactable = true;
        }

        
        for (int i = 0; i < gO.transform.childCount; i++) //basarım child objelerini listeye çekme
        {
            gorevObjects.Add(gO.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < gorevObjects.Count; i++) //basarımları kontrol etme (yapılan ve ödülleri alınanları
        {
            if (PlayerPrefs.HasKey("gorev" + i) == true)
            {
                gorevObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
                gorevObjects[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
    public void share()
    {
        if (!isProcessing)
        {
            StartCoroutine(ShareTextInAnroid());
        }
    }
    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public System.Collections.IEnumerator ShareTextInAnroid()
    {
        string gameURL = "https://play.google.com/store/apps/details?id=" + Application.identifier;
        var shareSubject = "Kore ile alakalı harika bir oyun buldum!";
        var shareMessage = "Hey! Kore ile alakalı harika bir oyun buldum! \nEğer sende bu oyunu denemek istiyorsan hemen indir. " +
                           " \nKOREGAME\n\n" +
                           gameURL;
                         

        isProcessing = true;

        if (!Application.isEditor)
        {
            //Create intent for action send
            AndroidJavaClass intentClass =
                new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject =
                new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>
            ("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);


            //call createChooser method of activity class
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity =
                unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser =
                intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your high score");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }
    public void gorev_URL(string url)
    {
        string HtmlText = gm.GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
        }
        else if (!HtmlText.Contains("schema.org/WebPage"))
        {
            //Redirecting since the beginning of googles html contains that 
            //phrase and it was not found
        }
        else
        {
            Application.OpenURL(url);
        }

    }
    
    public void gorevAl(int index)
    {
        string HtmlText = gm.GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<Text>().text = "İnternet bağlantısı yok.";
            Destroy(Instantiate(dogrucevap), 2.3f);
        }
        else if (!HtmlText.Contains("schema.org/WebPage"))
        {
            //Redirecting since the beginning of googles html contains that 
            //phrase and it was not found
        }
        else
        {
            PlayerPrefs.SetInt("gorev" + index, 1);
            for (int i = 0; i < gorevObjects.Count; i++)
            {
                if (PlayerPrefs.GetInt("gorev" + i) == 1)
                {
                    gorevObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
                    gorevObjects[i].transform.GetChild(3).gameObject.SetActive(true);
                }
            }
        }
    }
    public void basarimOdul(int moneyValue) //basarım butonuna tıklanınca calısır ve basarım parasını para degıskenıne aktarır.
    {
        string HtmlText = gm.GetHtmlFromUri("http://google.com");
        if (HtmlText == "")
        {
        }
        else if (!HtmlText.Contains("schema.org/WebPage"))
        {
            //Redirecting since the beginning of googles html contains that 
            //phrase and it was not found
        }
        else
        {
            StartCoroutine(para_al(moneyValue));
        }
    }
    System.Collections.IEnumerator para_al(int mny)
    {
        yield return new WaitForSeconds(3f);
        GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
        dogrucevap.GetComponent<parascript>().incPara = mny;
        Destroy(Instantiate(dogrucevap), 3.1f);
        int temp = PlayerPrefs.GetInt("Para");
        PlayerPrefs.SetInt("Para", temp += mny);
    }
    public void videoIzle(Button btn)
    {
        int temp = 0;
        if(PlayerPrefs.HasKey("watchvideotick"))
        {
            temp = PlayerPrefs.GetInt("watchvideotick");
        }
        else
        {
            temp = 0;
            PlayerPrefs.SetInt("watchvideotick", 0);
        }


        if (PlayerPrefs.GetInt("watchvideotick") > 4)
        {
           
            if(!PlayerPrefs.HasKey("lastwatchvideo"))
            {
                ulong lastButtonClick = (ulong)System.DateTime.Now.Ticks;
                PlayerPrefs.SetString("lastwatchvideo", lastButtonClick.ToString());
                btn.interactable = false;
            }
            else
            {
                GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
                dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<Text>().text = "Bugünlük video kotanı doldurdun. Yarın tekrardan video izleyip para kazanabilirsin!";
                Destroy(Instantiate(dogrucevap), 3f);
                btn.interactable = false;
            }
        }
        else
        {

            string HtmlText = gm.GetHtmlFromUri("http://google.com");
            if (HtmlText == "")
            {
                GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
                dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<Text>().text = "İnternet bağlantısı yok.";
                Destroy(Instantiate(dogrucevap), 2.3f);
            }
            else if (!HtmlText.Contains("schema.org/WebPage"))
            {
                //Redirecting since the beginning of googles html contains that 
                //phrase and it was not found
            }
            else
            {
                if (gm.Interstitial_Control())
                {
                    gm.Display_Interstitial();
                    PlayerPrefs.SetInt("watchvideotick", temp += 1);
                    if (PlayerPrefs.GetInt("watchvideotick") > 4)
                    {
                        GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
                        dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<Text>().text = "Bugünlük video kotanı doldurdun. Yarın tekrardan video izleyip para kazanabilirsin!";
                        Destroy(Instantiate(dogrucevap), 3f);
                        btn.interactable = false;
                    }
                        basarimOdul(800);
                }
                else
                {
                    GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
                    dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<Text>().text = "Video izlemeye hazır değil. Tekrar deneyin.";
                    Destroy(Instantiate(dogrucevap), 2.3f);
                }
            }
           
         }
    }
   
   
    void videoizlemehazirmi()
    {
        if(PlayerPrefs.HasKey("lastwatchvideo"))
        {
            ulong lastButtonClick = ulong.Parse(PlayerPrefs.GetString("lastwatchvideo"));
            ulong diff = ((ulong)System.DateTime.Now.Ticks - lastButtonClick);
            ulong m = diff / System.TimeSpan.TicksPerMillisecond;

            float secondsLeft = (float)(14400000f - m) / 1000.0f;
            Debug.Log("kalan süre"+secondsLeft);
            if (secondsLeft < 10)
            {
                Debug.Log("süre bitmiş");
                PlayerPrefs.SetInt("watchvideotick", 0);
                PlayerPrefs.DeleteKey("lastwatchvideo");
                videobtn.interactable = true;
            }
        }
        else
        {
            Debug.Log("lastwatchvideo yok");
            videobtn.interactable = true;
        }

    }
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }


    }
}

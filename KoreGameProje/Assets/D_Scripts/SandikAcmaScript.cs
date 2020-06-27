using UnityEngine;
using UnityEngine.UI;
public class SandikAcmaScript : MonoBehaviour {
    [Header("Sandık Açılması İçin Gerekli Olanlar")]
    [SerializeField] private string[] paralar;
    [SerializeField] private string[] ekstralar;
    [Header("Objects")]
    [SerializeField] private Sprite openChest_Image;
    [SerializeField] private GameObject sandiktanCikan;
    [SerializeField] private GameObject sandiklarınhepsiacilsinmi;
    private GameManager gm;
    [SerializeField] private System.Collections.Generic.List<string> names;
    bool sandik_hepsi = false;
    int sandiksayisi;


    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sandiksayisi = 0;
        sandik_hepsi = false;
        name_control();
    }
    public void AllChestOpen()
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
                sandik_hepsi = true;
                sandiklarınhepsiacilsinmi.SetActive(false);

            }
            else
            {
                GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
                dogrucevap.transform.GetChild(0).GetChild(0).transform.transform.GetChild(1).GetComponent<Text>().text = "Video izlemeye hazır değil. Tekrar deneyin.";
                Destroy(Instantiate(dogrucevap), 1f);
            }
        }

    }

    public void ChestOpenEnd()
    {
        gameObject.SetActive(false);
    }

    void name_control()
    {
        if (names.Count == 0)
        {
            names.Clear();
            names.Add("leeminho");
            names.Add("leehyeri");
            names.Add("kimhyunjoong");
            names.Add("kimseokjin");
            names.Add("leedongwook");
            names.Add("suzy");
            names.Add("parkminyoung");
            names.Add("parkseojong");
            names.Add("leejongsuk");
            names.Add("parkhyungsik");
            names.Add("parkboyoung");
            names.Add("jichangwook");
            names.Add("kimnamjoon");
            names.Add("parkshinhye");
            names.Add("junjihyun");
            //prefab initalize

        }
    }

 
    public void OpenChest(GameObject chest_image) //sandik acma scripti herhangibir sandıga basıldıgında
    {
        sandiksayisi += 1;
        chest_image.GetComponent<Image>().sprite = openChest_Image; 
        int a = UnityEngine.Random.Range(1, 101);

        chest_image.GetComponent<Button>().interactable = false;
        if(a <=10)
        {
                 name_control();
                System.Collections.Generic.List<string> puzzletemp = new System.Collections.Generic.List<string>();
                puzzletemp.Clear();
                while (puzzletemp.Count < 60)
                {
                    int rand = UnityEngine.Random.Range(0, 15);
                    int indexrand = UnityEngine.Random.Range(1, 5);
                    string puzzletemps = names[rand] + indexrand;
                    if (!PlayerPrefs.HasKey(names[rand] + indexrand))
                    {
                        PlayerPrefs.SetInt(names[rand] + indexrand, 1);
                        sandiktanCikan.GetComponent<SandiktanCikanEsya>().puzzle(names[rand] + indexrand);
                        Instantiate(sandiktanCikan);
                        SandikKontrol();
                        return;
                    }
                    else
                    {
                        if (puzzletemp.IndexOf(puzzletemps) == -1)
                        {
                            puzzletemp.Add(puzzletemps);
                        }
                    }
                }
                int bb = UnityEngine.Random.Range(0, paralar.Length);
                int paraa = System.Convert.ToInt32(paralar[bb]);
                ParaArttir(paraa);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(paraa);
                Instantiate(sandiktanCikan);
        }
        if(a > 10 && a <=50)
        {
                int b = UnityEngine.Random.Range(0, paralar.Length);
                int para = System.Convert.ToInt32(paralar[b]);
                ParaArttir(para);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(para);
                Instantiate(sandiktanCikan);  
        }
         if(a > 50)
        {
                int y = UnityEngine.Random.Range(0, ekstralar.Length);
                string ekstra = ekstralar[y];
                if(ekstra  != "Enerji Yenile")
                {
                    sandiktanCikan.GetComponent<SandiktanCikanEsya>().skor(System.Convert.ToInt32(ekstra));
                    Instantiate(sandiktanCikan);
                    SkorArttir(System.Convert.ToInt32(ekstra));
                }
                else
                {
                    int r = UnityEngine.Random.Range(0, 100);

                    if(r <= 70)
                    {
                        sandiktanCikan.GetComponent<SandiktanCikanEsya>().enerji();
                        Instantiate(sandiktanCikan);
                    }
                    else
                    {
                        ParaArttir(350);
                        sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(350);
                        Instantiate(sandiktanCikan);
                    }
                }
        }
        SandikKontrol();
      
    }
    void SandikKontrol()
    {
        if (sandik_hepsi == false)
        {
            sandiklarınhepsiacilsinmi.SetActive(true);
        }
        if (sandiksayisi >= 3)
        {
            ChestOpenEnd();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
    
    void ParaArttir(int m)
    {

        GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
        dogrucevap.GetComponent<parascript>().incPara = m;
        Destroy(Instantiate(dogrucevap), 3.1f);
        int temp= PlayerPrefs.GetInt("Para");
        PlayerPrefs.SetInt("Para",temp +=m);
    }
    void SkorArttir(int m)
    {
       

        GameObject dogrucevap = Resources.Load("GameObject/Skor") as GameObject;
        dogrucevap.GetComponent<skorscript>().incSkor = m;
        Destroy(Instantiate(dogrucevap), 3.1f);
        int temp= PlayerPrefs.GetInt("AnaSkor");
        PlayerPrefs.SetInt("AnaSkor",temp +=m);
    }
}

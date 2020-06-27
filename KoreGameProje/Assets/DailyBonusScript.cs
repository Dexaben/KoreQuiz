using UnityEngine;
public class DailyBonusScript : MonoBehaviour {
    [SerializeField] private System.Collections.Generic.List<GameObject> bonusObjects; //gorev objeleri
    [SerializeField] private GameObject gO; //gorev  objelerinin contenti
    [SerializeField] UnityEngine.UI.Text gb_time_text;
    [SerializeField] GameObject sandiktanCikan;
    [SerializeField] GameObject bonuscanvas;
    [SerializeField] private System.Collections.Generic.List<string> names;
    void Awake ()
    {
        name_control();
        for (int i = 0; i < gO.transform.childCount; i++) //basarım child objelerini listeye çekme
        {
            bonusObjects.Add(gO.transform.GetChild(i).gameObject);
        }
        streakTest();
        if(Sure())
        {
            bonuscanvas.SetActive(true);
        }
        if(PlayerPrefs.HasKey("bonus12"))
        {
            Destroy(this.gameObject);
        }
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
    public void tikla()
    {
        if(bonuscanvas.activeInHierarchy == false)
        {
            bonuscanvas.SetActive(true);
        }
        else
        {
            bonuscanvas.SetActive(false);
        }
    }
    void streakTest() //streak indexini bulur ve ona göre butonları ayarlar.
    {
        for (int i = 0; i < bonusObjects.Count; i++)
        {
            if (!PlayerPrefs.HasKey("bonus" + i))
            {
                for (int x = 0;x < bonusObjects.Count;x++)
                {
                    bonusObjects[x].GetComponent<UnityEngine.UI.Button>().interactable = false;
                }
                if(Sure())
                {
                    bonusObjects[i].GetComponent<UnityEngine.UI.Button>().interactable = true;
                }

            
                for (int x = 0; x < i; x++)
                {
                    bonusObjects[x].transform.GetChild(3).gameObject.SetActive(true);
                }
                return;
            }
        }
    }
    public void Altin(int altindeger)
    {
        ParaArttir(altindeger);
        int temp = PlayerPrefs.GetInt("Para");
            PlayerPrefs.SetInt("Para", temp += altindeger);

    }
    public void puzzle(int tane)
    {
        System.Collections.Generic.List<string> puzzletemp = new System.Collections.Generic.List<string>();
        puzzletemp.Clear();
   
        name_control();
        for (int c = 0; c < tane; c++)
        {
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
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Puzzle'nin tüm parçalarını zaten tamamladınız.";
            Destroy(Instantiate(dogrucevap), 2.3f);
           
        }
       
    }
    public void gorevAl(int index) //bonus butonuna tıklandıgında
    {
            PlayerPrefs.SetInt("bonus" + index, 1);
            PlayerPrefs.DeleteKey("bonustime");
            ulong lastButtonClick = (ulong)System.DateTime.Now.Ticks;
            PlayerPrefs.SetString("bonustime", lastButtonClick.ToString());
            streakTest();
            Invoke("Kapat", 3f);

    }
    void Kapat()
    {
        bonuscanvas.SetActive(false);
    }
    private bool Sure() //önceki buton tıklanmasından sonra geçen süreyi hesaplar.
    {
        if (PlayerPrefs.HasKey("bonustime"))
        {
            ulong lastButtonClick = ulong.Parse(PlayerPrefs.GetString("bonustime"));
            ulong diff = ((ulong)System.DateTime.Now.Ticks - lastButtonClick);
            ulong m = diff / System.TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(86400000f - m) / 1000.0f;
            Debug.Log("kalan süre" + secondsLeft);
            if (secondsLeft < 0)
            {
                Debug.Log("süre bitmiş");

                return true;
            }
            return false;
        }
        else
        {
            return true;
        }

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bonuscanvas.SetActive(false);
        }
        if(PlayerPrefs.HasKey("bonustime"))
        { 
            ulong lastButtonClick = ulong.Parse(PlayerPrefs.GetString("bonustime"));
            ulong diff = ((ulong)System.DateTime.Now.Ticks - lastButtonClick);
            ulong m = diff / System.TimeSpan.TicksPerMillisecond;
            float secondsLeft = (float)(86400000f - m) / 1000.0f;

            string r = "";
            //Saatler
            r += "("+((int)secondsLeft / 3600).ToString() + "s ";
            secondsLeft -= ((int)secondsLeft / 3600) * 3600;
            //Dakikalar
            r += ((int)secondsLeft / 60).ToString("00") + "d ";
            //Saniyeler
            r += (secondsLeft % 60).ToString("00") + "sn)";
            gb_time_text.text = r;
            if(secondsLeft<0)
            {
                bonuscanvas.SetActive(true);
                PlayerPrefs.DeleteKey("bonustime");
                streakTest();
            }
        }
        else
        {
            gb_time_text.text = "Bonus Alınabilir!";

        }
    }
    void ParaArttir(int mny)
    {
        StartCoroutine("PARAART", mny);

    }
    System.Collections.IEnumerator PARAART(int m)
    {
        yield return new WaitForSeconds(1);
        GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
        dogrucevap.GetComponent<parascript>().incPara = m;
        Destroy(Instantiate(dogrucevap), 3.1f);
    }
}

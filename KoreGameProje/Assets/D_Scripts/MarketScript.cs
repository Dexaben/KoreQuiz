using UnityEngine;
using UnityEngine.UI;
public class MarketScript : MonoBehaviour {

    [SerializeField] private GameObject sandiktanCikan;  //sandiktancikan canvası (instalize object)
    [SerializeField] private System.Collections.Generic.List<string> names;
    [SerializeField] private Text para_text;
    [SerializeField] private Button SinirsizEnerji;

    void OnEnable()
    {
        if(PlayerPrefs.HasKey("UnliminatedEnergy"))
        {
            SinirsizEnerji.interactable = false;
        }
        para_text.text = PlayerPrefs.GetInt("Para") + " KP";
    }
   
    public void Money_ChestOpen() //para sandığı açılırsa calıstırılacak fonksiyon
    {
        if (PlayerPrefs.GetInt("Para") >= 1000) //eger para 1500 den yüksekse calıs
        {
            int a = Random.Range(0, 11);
            int temp = PlayerPrefs.GetInt("Para");
            
            switch (a) //object instalize ve objenin degerlerini degistiren fonksiyona gitme
            {
                case 0:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 100, 1000);
                    PlayerPrefs.SetInt("Para", temp += 100);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(100);
                Instantiate(sandiktanCikan);
                    break;

                case 1:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 10, 1000);
                    PlayerPrefs.SetInt("Para", temp += 10);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(10);
                Instantiate(sandiktanCikan); 
                    break;

                case 2:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 2000, 1000);
                    PlayerPrefs.SetInt("Para", temp += 2000);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(2000);
                Instantiate(sandiktanCikan);
                    break;

                case 3:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 2100, 1000);
                    PlayerPrefs.SetInt("Para", temp += 2100);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(2100);
                Instantiate(sandiktanCikan);
                    break;

                case 4:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 1600, 1000);
                    PlayerPrefs.SetInt("Para", temp += 1600);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(1600);
                Instantiate(sandiktanCikan);
                    break;

                case 5:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 5, 1000);
                    PlayerPrefs.SetInt("Para", temp += 5);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(5);
                Instantiate(sandiktanCikan);
                     break;

                case 6:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 1, 1000);
                    PlayerPrefs.SetInt("Para", temp += 1);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(1);
                Instantiate(sandiktanCikan);
                     break;

                case 7:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 1340, 1000);
                    PlayerPrefs.SetInt("Para", temp += 1340);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(1340);
                Instantiate(sandiktanCikan);
                    break;

                case 8:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 500, 1000);
                    PlayerPrefs.SetInt("Para", temp += 500);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(500);
                Instantiate(sandiktanCikan);
                    break;

                case 9:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 3250, 1000);
                    PlayerPrefs.SetInt("Para", temp += 3250);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(3250);
                Instantiate(sandiktanCikan);
                    break;

                case 10:
                    ParaArttir(PlayerPrefs.GetInt("Para"), 1300, 1000);
                    PlayerPrefs.SetInt("Para", temp += 1300);
                sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(1300);
                Instantiate(sandiktanCikan);

                    break;
            }
            PlayerPrefs.SetInt("Para", temp -= 1500);

        }
        else
        {
            Instantiate(Resources.Load("GameObject/ParaYok") as GameObject);
        }

    }
    private float mevcutpara;
    private float incPara;
    private int finishPara;
    private bool start = false;

    void ParaArttir(int mevcutpara_, float incPara_ , int odeme_)
    {
        start = false;
        mevcutpara = mevcutpara_;
        incPara = incPara_ - odeme_ ;
      

        finishPara = mevcutpara_ + ((int)incPara_ - odeme_);
        Debug.Log(finishPara);
        if (finishPara == mevcutpara_)
        {
            return;
        }
        else
        {
            incPara = incPara / 150f;
            Invoke("START", 0.5F);
        }
        //-----------------------odemedus---------------------------


    }
    void START()
    {
        start = true;
    }
    void Update()
    {
        if (PlayerPrefs.HasKey("UnliminatedEnergy"))
        {
            SinirsizEnerji.interactable = false;
        }
        if (start)
        {
            if(incPara > 0)
            {
                mevcutpara += (float)incPara * Time.deltaTime * 100;
                para_text.text = (int)mevcutpara + " KP";
                para_text.color = Color.green;
                if ((int)mevcutpara >= finishPara)
                {
                    para_text.text = finishPara + " KP";
                    para_text.color = Color.white;
                    start = false;
                }
            }
            else if(incPara < 0)
            {
                mevcutpara += (float)incPara * Time.deltaTime * 100;
                para_text.text = (int)mevcutpara + " KP";
                para_text.color = Color.red;
                if ((int)mevcutpara <= finishPara)
                {
                    para_text.text = finishPara + " KP";
                    para_text.color = Color.white;
                    start = false;
                }
            }
          
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
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
    public void Puzzle_ChestOpen() //puzzle sandığı açılınca çalışacak komutlar
    {
        System.Collections.Generic.List<string> puzzletemp = new System.Collections.Generic.List<string>();
        puzzletemp.Clear();
        if (PlayerPrefs.GetInt("Para") >= 2000) //eger para 6000 den yüksekse calıs
        {
            int temp = PlayerPrefs.GetInt("Para");
           
         
            name_control();
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
                    ParaArttir(PlayerPrefs.GetInt("Para"), 0, 2000);
                    PlayerPrefs.SetInt("Para", temp -= 2000);
                    temp = PlayerPrefs.GetInt("Para");
                   
                    return;
                }
                else
                {
                    if(puzzletemp.IndexOf(puzzletemps)==-1)
                    {
                        puzzletemp.Add(puzzletemps);
                    }
                }
            }
            ParaArttir(PlayerPrefs.GetInt("Para"), 2000, 2000);
            PlayerPrefs.SetInt("Para", temp += 2000);
            PlayerPrefs.SetInt("Para", temp -= 2000);
            GameObject dogrucevap = Resources.Load("GameObject/Uyari") as GameObject;
            dogrucevap.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Puzzle'nin tüm parçalarını zaten tamamladınız.";
            Destroy(Instantiate(dogrucevap), 2.3f);
            return;
        }
        else
        {
            Instantiate(Resources.Load("GameObject/ParaYok") as GameObject);
        }

    }
    public void Sans_ChestOpen() //Sans sandığı acılırken calıstırılacak fonksiyon
    {
        if (PlayerPrefs.GetInt("Para") >= 1500) //eger para 4000 den yüksekse calıs
        {

            int temp = PlayerPrefs.GetInt("Para");
            int x = Random.Range(0, 3);
            switch(x)
            {
                case 0: //puzzle çıkarsa
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
                            ParaArttir(PlayerPrefs.GetInt("Para"), 0, 1500);
                            PlayerPrefs.SetInt("Para", temp -= 1500);
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
                    ParaArttir(PlayerPrefs.GetInt("Para"),1450, 1500);
                    int tempp = PlayerPrefs.GetInt("Para");
                    PlayerPrefs.SetInt("Para", tempp -= 1450);
                    sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(1450);
                    Instantiate(sandiktanCikan);
        
                    break;
                case 1: //para cıkarsa
                    int v = Random.Range(0, 11);
                    switch (v)
                    {
                        case 0:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 100, 1500);
                            PlayerPrefs.SetInt("Para", temp += 100);

                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(100);
                            Instantiate(sandiktanCikan);
                  
                            break;

                        case 1:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 25, 1500);
                            PlayerPrefs.SetInt("Para", temp += 25);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(25);
                            Instantiate(sandiktanCikan);
                     
                            break;

                        case 2:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 2000, 1500);
                            PlayerPrefs.SetInt("Para", temp += 2000);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(2000);
                            Instantiate(sandiktanCikan);
                       
                            break;

                        case 3:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 2500, 1500);
                            PlayerPrefs.SetInt("Para", temp += 2500);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(2500);
                            Instantiate(sandiktanCikan);
                       
                            break;

                        case 4:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 1600, 1500);
                            PlayerPrefs.SetInt("Para", temp += 1600);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(1600);
                            Instantiate(sandiktanCikan);
      
                            break;

                        case 5:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 50, 1500);
                            PlayerPrefs.SetInt("Para", temp += 50);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(50);
                            Instantiate(sandiktanCikan);
                    
                            break;

                        case 6:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 10, 1500);
                            PlayerPrefs.SetInt("Para", temp += 10);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(10);
                            Instantiate(sandiktanCikan);
              
                            break;

                        case 7:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 1230, 1500);
                            PlayerPrefs.SetInt("Para", temp += 1230);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(1230);
                            Instantiate(sandiktanCikan);
                            break;

                        case 8:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 3000, 1500);
                            PlayerPrefs.SetInt("Para", temp += 3000);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(3000);
                            Instantiate(sandiktanCikan);
                           
                            break;

                        case 9:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 350, 1500);
                            PlayerPrefs.SetInt("Para", temp += 350);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(350);
                            Instantiate(sandiktanCikan);
                          
                            break;

                        case 10:
                            ParaArttir(PlayerPrefs.GetInt("Para"), 4500, 1500);
                            PlayerPrefs.SetInt("Para", temp += 4500);
                            sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(4500);
                            Instantiate(sandiktanCikan);
                            
                            break;
                    }
                    PlayerPrefs.SetInt("Para", temp -= 1500);
                    break;
                case 2: //enerji yenileme cıkarsa

                    int r = UnityEngine.Random.Range(0, 2);

                    if (r == 0)
                    {
                        sandiktanCikan.GetComponent<SandiktanCikanEsya>().enerji();
                        Instantiate(sandiktanCikan);
                    }
                    else
                    {
                        ParaArttir(PlayerPrefs.GetInt("Para"), 525, 1500);
                        PlayerPrefs.SetInt("Para", temp += 525);

                        sandiktanCikan.GetComponent<SandiktanCikanEsya>().para(525);
                        
                        Instantiate(sandiktanCikan);
                    }
                 
                    break;
            }
        }
        else
        {
            Instantiate(Resources.Load("GameObject/ParaYok") as GameObject);
        }
     
    }
    
}

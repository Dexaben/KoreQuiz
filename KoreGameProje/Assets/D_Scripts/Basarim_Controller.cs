using UnityEngine;
using UnityEngine.UI;
public class Basarim_Controller : MonoBehaviour {
    [SerializeField] private System.Collections.Generic.List<GameObject> basarimObjects; //basarım objeleri
    [SerializeField] private GameObject gO; //basarım  objelerinin contenti
  
    [SerializeField] private System.Collections.Generic.List<string> names;


    void OnEnable()
    {
        name_control();
        for (int i = 0; i < gO.transform.childCount; i++) //basarım child objelerini listeye çekme
        {
            basarimObjects.Add(gO.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < basarimObjects.Count; i++) //basarımları kontrol etme (yapılan ve ödülleri alınanları
        {
            if (PlayerPrefs.GetInt("basarim" + i) == 1)
            {
                basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = false;
                basarimObjects[i].transform.GetChild(3).gameObject.SetActive(true);
            }
            basarimKontrol(i);
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void basarimAl(int index) //basarım butonuna basılınca calısır ve kullanımı saveler
    {
        PlayerPrefs.SetInt("basarim" + index, 1);
        basarimObjects[index].transform.GetChild(0).GetComponent<Button>().interactable = false;
        basarimObjects[index].transform.GetChild(3).gameObject.SetActive(true);
    }
    void ParaArttir(int mny)
    {
        GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
        dogrucevap.GetComponent<parascript>().incPara = mny;
        Destroy(Instantiate(dogrucevap), 3.1f);
    }
    public void basarimOdul(int moneyValue) //basarım butonuna tıklanınca calısır ve basarım parasını para degıskenıne aktarır.
    {
        ParaArttir(moneyValue);
        int temp = PlayerPrefs.GetInt("Para");
        PlayerPrefs.SetInt("Para",   temp += moneyValue);
    }
    void basarimKontrol(int i)
    {
        switch (i)
        {
            case 0: //Bölümlerden en az bir tanesini tamamla!
                if (PlayerPrefs.GetInt("level_available2") == 1 && PlayerPrefs.HasKey("basarim1") == false)
                {
                    basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 1: //500 skora ulaş!
                if (PlayerPrefs.GetInt("AnaSkor") >= 500 && PlayerPrefs.HasKey("basarim2") == false)
                {
                    basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 2: //3000 skora ulaş!
                if (PlayerPrefs.GetInt("AnaSkor") >= 3000 && PlayerPrefs.HasKey("basarim3") == false)
                {
                    basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 3: //10000 skora ulaş!
                if (PlayerPrefs.GetInt("AnaSkor") >= 10000 && PlayerPrefs.HasKey("basarim4") == false)
                {
                    basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 4: //120000 skora ulaş!
                if (PlayerPrefs.GetInt("AnaSkor") >= 35000 && PlayerPrefs.HasKey("basarim5") == false)
                {
                    basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 5: //Bölümlerin hepsini tamamlayın!
                if (PlayerPrefs.HasKey("level_available" + 51) == true && PlayerPrefs.HasKey("basarim6") == false)
                {
                        basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 6: //Bölümü hiç yanlış yapmadan tamamlayın!
                if (PlayerPrefs.HasKey("HicYanlisYapmadan") == true && PlayerPrefs.HasKey("basarim7") == false)
                {
                        basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 7: //Bir bölümün tüm sorularını yanlış cevaplayarak geçin.
                if (PlayerPrefs.HasKey("HepYanlisYaparak") == true && PlayerPrefs.HasKey("basarim8") == false)
                {
                        basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 8: //10 bölüm tamamlayın!
                if (PlayerPrefs.HasKey("level_available" + 11) == true && PlayerPrefs.HasKey("basarim9") == false)
                {
                        basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                }
                break;
            case 9: //En az bir puzzle'nin tüm parçalarını toplayın.
                {
        
                    name_control();

                    for (int x = 0; x < names.Count; x++)
                    {
                        int tamamlanmis = 0;
                        for (int c = 1; c < 5; c++)
                        {
                            if (PlayerPrefs.HasKey(names[x] + c))
                            {
                                tamamlanmis += 1;
                            }
                            if (tamamlanmis >= 4)
                            {
                                if (PlayerPrefs.HasKey("basarim9") == false)
                                {
                                    basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                                    return;
                                }

                            }
                        }


                    }

                }
                break;
            case 10: //Tüm puzzle'ların parçalarını tamamlayın.
                {
                    for (int x = 0; x < names.Count; x++)
                    {
                        for (int c = 1; c < 5; c++)
                        {
                            if (!PlayerPrefs.HasKey(names[x] + c))
                            {
                                return;
                            }
                        }
                    }
                    if (PlayerPrefs.HasKey("basarim10") == false)
                    {
                        basarimObjects[i].transform.GetChild(0).GetComponent<Button>().interactable = true;
                    }
                }
                break;
            
        }
    }
}

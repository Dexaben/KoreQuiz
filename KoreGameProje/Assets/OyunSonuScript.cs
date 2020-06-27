
using UnityEngine;
using UnityEngine.UI;
public class OyunSonuScript : MonoBehaviour {
    public System.Collections.Generic.List<Sprite> rutbeImages = new System.Collections.Generic.List<Sprite>(3);
    [SerializeField]private Text RUTBE_TEXT;
    [SerializeField] private Image RUTBE_IMAGES;
    [SerializeField] private GameObject goLevel;
    public System.Collections.Generic.List<Image> level_Images;
    [SerializeField] Sprite oneSTAR;
    [SerializeField] Sprite twoSTAR;
    [SerializeField] Sprite threeSTAR;
    [SerializeField] private Text anaskorText;

    [SerializeField] private System.Collections.Generic.List<Image> puzzleImages;
    [SerializeField] private System.Collections.Generic.List<string> names;
    [SerializeField] private GameObject goPuzzle;

    [SerializeField] private System.Collections.Generic.List<Image> basarimImages;
    [SerializeField] private GameObject goBasarim;

    [SerializeField] private Text tebriklerText;


    void Start () {
        name_control();
        tebriklerText.text = "Tebrikler " + PlayerPrefs.GetString("Username") + " !\nBütün bölümleri başarı ile tamamladınız, işte başardıklarınız!";
        anaskorText.text = "Ana Skor\n" + PlayerPrefs.GetInt("AnaSkor");
        for(int i =0;i<goBasarim.transform.childCount;i++)
        {
            basarimImages.Add( goBasarim.transform.GetChild(i).GetComponent<Image>());
            basarimImages[i].sprite = Resources.Load<Sprite>("basarimResimleri/basarim" + (i+1));
            Color32 temp = new Color32(87, 87, 87, 180);
            basarimImages[i].color = temp;
            basarimKontrol(i);
        }
        if (puzzleImages.Count == 0)
        {
            puzzleImages.Clear();
            for (int i = 0; i < goPuzzle.transform.childCount; i++)
            {
               
                for (int y = 0; y < 4; y++)
                {

                    puzzleImages.Add(goPuzzle.transform.GetChild(i).gameObject.transform.GetChild(y).gameObject.GetComponent<Image>());
               
                }
             
            }
        }
        for (int i = 0; i < goPuzzle.transform.childCount; i++)
        {

            for (int y = 0; y < 4; y++)
            {

                puzzleImages[i * 4 + y].sprite = Resources.Load<Sprite>("Puzzles/" + i + "/" + names[i] + (y + 1));
                Color32 temp = new Color32(87, 87, 87, 180);
                puzzleImages[i * 4 + y].color = temp;


            }

        }
        for (int i = 0; i < 15; i++)
        {
            tamamlanmaControl(names[i], i);
        }
        for (int i = 0;i<goLevel.transform.childCount;i++)
        {
            level_Images.Add(goLevel.transform.GetChild(i).GetComponent<Image>());
            switch(PlayerPrefs.GetString("alphabe" + (i + 1)))
            {
                case "oneStar":
                    level_Images[i].sprite = oneSTAR;
                    break;
                case "twoStar":
                    level_Images[i].sprite = twoSTAR;
                    break;
                case "threeStar":
                    level_Images[i].sprite = threeSTAR;
                    break;
                default:
                    level_Images[i].gameObject.SetActive(false);
                    break;
            }
        }
        
        rutbeImages.AddRange(Resources.LoadAll<Sprite>("rutbeResimleri"));
        if (PlayerPrefs.GetInt("AnaSkor") < 500)
        {
            Destroy(RUTBE_IMAGES);
            RUTBE_TEXT.text = "SEVİYE YOK";
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
            for (int i = 0; i < rutbeImages.Count; i++)
            {
                if (rutbeImages[i].name == "acemi")
                {
                    RUTBE_IMAGES.sprite = rutbeImages[i];
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
            for (int i = 0; i < rutbeImages.Count; i++)
            {
                if (rutbeImages[i].name == "tecrubeli")
                {
                    RUTBE_IMAGES.sprite = rutbeImages[i];
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
            for (int i = 0; i < rutbeImages.Count; i++)
            {
                if (rutbeImages[i].name == "efsanevi")
                {
                    RUTBE_IMAGES.sprite = rutbeImages[i];
                    break;
                }
            }
            RUTBE_TEXT.text = "EFSANEVİ";
        }
    }
    void tamamlanmaControl(string name, int index)
    {
       
        for (int i = 1; i < 5; i++)
        {
            if (PlayerPrefs.HasKey(name + i))
            {
                Color32 temp = new Color32(255, 255, 255, 255);
              
              puzzleImages[index * 4 + i -1].color = temp;
            }
        }
        
    }
    void basarimKontrol(int i)
    {
        switch (i)
        {
            case 0: //Bölümlerden en az bir tanesini tamamla!
                if (PlayerPrefs.GetInt("level_available2") == 1)
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
                }
                break;
            case 1: //500 skora ulaş!
                if (PlayerPrefs.GetInt("AnaSkor") >= 500)
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
                }
                break;
            case 2: //3000 skora ulaş!
                if (PlayerPrefs.GetInt("AnaSkor") >= 3000)
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
                }
                break;
            case 3: //10000 skora ulaş!
                if (PlayerPrefs.GetInt("AnaSkor") >= 10000)
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
                }
                break;
            case 4: //120000 skora ulaş!
                if (PlayerPrefs.GetInt("AnaSkor") >= 35000)
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
                }
                break;
            case 5: //Bölümlerin hepsini tamamlayın!
                if (PlayerPrefs.HasKey("level_available" + 46) == true)
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
                }
                break;
            case 6: //Bölümü hiç yanlış yapmadan tamamlayın!
                if (PlayerPrefs.HasKey("HicYanlisYapmadan") == true)
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
                }
                break;
            case 7: //Bir bölümün tüm sorularını yanlış cevaplayarak geçin.
                if (PlayerPrefs.HasKey("HepYanlisYaparak") == true )
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
                }
                break;
            case 8: //10 bölüm tamamlayın!
                if (PlayerPrefs.HasKey("level_available" + 11) == true)
                {
                    Color32 temp = new Color32(255, 255, 255, 255);
                    basarimImages[i].color = temp;
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
                               
                                    Color32 temp = new Color32(255, 255, 255, 255);
                                    basarimImages[i].color = temp;
                                    return;
                                

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
                   
                        Color32 temp = new Color32(255, 255, 255, 255);
                        basarimImages[i].color = temp;
                  
                }
                break;

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
  
}

using UnityEngine;
using UnityEngine.UI;
public class PuzzleScript : MonoBehaviour {
    [SerializeField] private System.Collections.Generic.List<Image> puzzleImages;
    [SerializeField] private Slider yuzdeSlider;
    [SerializeField] private Text yuzdeText;
    [SerializeField] private System.Collections.Generic.List<GameObject>  puzzle;
    [SerializeField] private GameObject puzzleacbutton;
    [SerializeField] private System.Collections.Generic.List<string> names;
    public GameObject gO;
 
    void OnEnable()
    {
        name_control();
        if(puzzleImages.Count == 0 || puzzle.Count == 0)
        {
            puzzleImages.Clear();
            puzzle.Clear();
            for (int i = 0; i < gO.transform.childCount; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    puzzleImages.Add(gO.transform.GetChild(i).gameObject.transform.GetChild(y).gameObject.GetComponent<Image>());
                    if (y == 3)
                    {
                        puzzle.Add(gO.transform.GetChild(i).gameObject.transform.GetChild(y).gameObject.transform.GetChild(0).gameObject);
                    }
                }
            }
        }
        PuzzleYenile();
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

    void tamamlanmaControl(string name, int index)
    {
        int compPuzzleCount = 0;
        puzzle[index].SetActive(false);
        for (int i = 1; i < 5; i++)
        {
            if (PlayerPrefs.HasKey(name + i))
            {
                compPuzzleCount += 1;
            }
        }
        if (compPuzzleCount >= 4)
        {
            puzzle[index].SetActive(true);
            if (PlayerPrefs.HasKey(name + "_tamamlandi"))
            {
                puzzle[index].transform.GetChild(0).gameObject.SetActive(true);
                puzzle[index].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                puzzle[index].transform.GetChild(0).gameObject.SetActive(false);
                puzzle[index].transform.GetChild(1).gameObject.SetActive(true);
            }
        } 
    }

    public void PuzzleYenile()
    {
       
        for(int i = 0;i<15;i++)
        {
            for (int x = 1; x <= 4; x++)
            {
                if (PlayerPrefs.HasKey(names[i] + x) == true)
                {
                    var tempcolor = puzzleImages[i*4+(x-1)].color;
                    tempcolor.a = 1f;
                    puzzleImages[i * 4 + (x - 1)].color = tempcolor;
                    puzzleImages[i * 4 + (x - 1)].color = Color.white;
                }
            }
            tamamlanmaControl(names[i], i);
        }
        for (int i = 0; i < 15; i++)
        {
            if(PlayerPrefs.HasKey(names[i]+"_tamamlandi"))
            {
                puzzleacbutton.SetActive(false);
            }
            else
            {
                puzzleacbutton.SetActive(true);
                break;
            }
        }
       
        yuzdeHesapla();

    }
    void yuzdeHesapla()
    {
        int yuzde = 0;
        for (int i = 0; i < puzzleImages.Count; i++)
        {
            if (puzzleImages[i].color.a == 1)
            {
                yuzde += 1;
            }
        }
        yuzdeSlider.value = (float)yuzde*0.016f;
        yuzdeText.text = "%"+((int)(1.66*yuzde)).ToString();
        if((int)(1.66*yuzde) >= 99)
        {
            yuzdeText.text = "%100";
            yuzdeSlider.value = 1;
        }
    }
    public void puzzle_kullan(string tamamlandistring)
    {
        if (!PlayerPrefs.HasKey(tamamlandistring))
        {
            int tempp = PlayerPrefs.GetInt("Elmas");
            PlayerPrefs.SetInt("Elmas", tempp += 1);
            PlayerPrefs.SetInt(tamamlandistring, 0);
        }
    }
    public void Destroyed(GameObject gm)
    {
        gm.SetActive(false);
        gm.transform.parent.Find("Text").gameObject.SetActive(true);
    }
   
}

using UnityEngine;
using UnityEngine.UI;
public class SandiktanCikanEsya : MonoBehaviour {
    [SerializeField] private Sprite moneyImage;
    [SerializeField] private Sprite SkorImage;
    [SerializeField] private Sprite EnerjiImage;
    [SerializeField] private System.Collections.Generic.List<Sprite> puzzleImagesi;
    [SerializeField] private Image image_;
    [SerializeField] private Text text_;
    [SerializeField] private System.Collections.Generic.List<string> names;
    bool destroy_open = false;
	void Awake () {
        name_control();
        image_ = gameObject.transform.GetChild(1).GetComponent<Image>();
        text_ = gameObject.transform.GetChild(2).GetComponent<Text>();
        destroy_open = false;
        StartCoroutine("bekle");
        puzzleImagesi.Clear();
        for (int y = 0; y < 15; y++)
        {
            
            Sprite[] puzzleResimTemp = Resources.LoadAll<Sprite>("Puzzles/" + y);
            puzzleImagesi.AddRange(puzzleResimTemp);
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(destroy_open)
            Cikis();
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
    System.Collections.IEnumerator bekle()
    {
        yield return new WaitForSeconds(2f);
        destroy_open = true;
    }
	public void para(int paramiktari)
    {
        image_.sprite = moneyImage;
        text_.text = "Sandıktan Çıkan Eşya " + paramiktari+" KP!";
    }
    public void skor(int skormiktari)
    {
        image_.sprite = SkorImage;
        text_.text = "Sandıktan Çıkan Eşya " + skormiktari + " SKOR!";
    }
    public void puzzle(string puzzlestring)
    {
        puzzleImagesi.Clear();
        for (int y = 0; y < 15; y++)
        {
            Sprite[] puzzleResimTemp = Resources.LoadAll<Sprite>("Puzzles/" + y);
            puzzleImagesi.AddRange(puzzleResimTemp);
        }
        for (int i = 0; i<puzzleImagesi.Count;i++)
        {
            if (puzzleImagesi[i].name == puzzlestring)
            {
                image_.sprite = puzzleImagesi[i];
                text_.text = "Sandıktan Çıkan Eşya " + puzzlestring + " PUZZLE PARÇASI!";
                if (PlayerPrefs.HasKey("basarim9") == false)
                {
                    int tamamlanmis_ = 0;
                    for (int y = 0; y < names.Count; y++)
                    {
                        int tamamlanmis = 0;
                        for (int x = 1; x< 5; x++)
                        {
                            if (PlayerPrefs.HasKey(names[y] + x))
                            {
                                tamamlanmis += 1;
                                tamamlanmis_ += 1;
                            }
                            if (tamamlanmis >= 4)
                            {
                                GameObject dg = Resources.Load("GameObject/BasarimUyari") as GameObject;
                                dg.GetComponent<basarimUyari>().currentBasarim = "basarim9";
                                dg.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler bir puzzle'nin bütün parçalarını tamamlayarak başarım kazandınız!";
                                Destroy(Instantiate(dg), 2.3f);
                            }

                        }
                    }
                    if (tamamlanmis_ >= 60)
                    {
                        GameObject dg = Resources.Load("GameObject/BasarimUyari") as GameObject;
                        dg.GetComponent<basarimUyari>().currentBasarim = "basarim10";
                        dg.GetComponent<basarimUyari>().currentBasarimText = "Tebrikler tüm puzzle parçalarını tamamlayarak başarım kazandınız!";
                        Destroy(Instantiate(dg), 2.3f);
                    }
                }
                return;
            }
        }
        
    }
    public void enerji()
    {
        image_.sprite = EnerjiImage;
        int temp = PlayerPrefs.GetInt("Elmas");
        PlayerPrefs.SetInt("Elmas", temp += 1);
        text_.text = "Sandıktan Çıkan Eşya 1 Elmas!";
    }
    public void Cikis()
    {
        if(destroy_open)
        Destroy(gameObject);
    }
}

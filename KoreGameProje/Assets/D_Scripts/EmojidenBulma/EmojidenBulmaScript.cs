using UnityEngine;
using UnityEngine.UI;
public class EmojidenBulmaScript : MonoBehaviour {
    public SceneController scenecontroller;
    public Question_emojidenBulma[] question;
    private Question_emojidenBulma currentquestion;
    public Image emojiImagee;
    public Text ipucu_Text;
    int rand;
    public GameObject[] answ = new GameObject[4];
    GameObject btn;

    void Start()
    {
        scenecontroller = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
  
        RandomSoruCagir();
    }
    public void RandomSoruCagir()  //RANDOM OLARAK SORULARI ÇAĞIRIR BÖLÜM BASINDA VE İSTENİRSE FONKSİYON ÇAĞIRILDIĞINDA
    {
        SetCurrentQuestion();
    }
    void SetCurrentQuestion()
    {
       
            System.Collections.Generic.List<string> puzzletemp = new System.Collections.Generic.List<string>();
            puzzletemp.Clear();
            while (puzzletemp.Count < question.Length)
            {
                rand = Random.Range(0, question.Length);
                string puzzletemps = "emoji" + rand;
                if (!PlayerPrefs.HasKey("emoji" + rand))
                {
                    currentquestion = question[rand];
                    emojiImagee.sprite = currentquestion.emojiImage;
                    ipucu_Text.text = currentquestion.ipucu;
                    answ[0].transform.GetChild(0).GetComponent<Text>().text = currentquestion.answ1;
                    answ[1].transform.GetChild(0).GetComponent<Text>().text = currentquestion.answ2;
                    answ[2].transform.GetChild(0).GetComponent<Text>().text = currentquestion.answ3;
                    answ[3].transform.GetChild(0).GetComponent<Text>().text = currentquestion.answ4;
                    PlayerPrefs.SetInt("emoji" + rand, 1);
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
            for (int i = 0; i < puzzletemp.Count; i++)
            {
                PlayerPrefs.DeleteKey(puzzletemp[i]);
            }
            SetCurrentQuestion();
      
        
    }
    public void sorugec()
    {
        if (PlayerPrefs.HasKey("Para"))
        {
            if (PlayerPrefs.GetInt("Para") >= 1500)
            {
                int temp = PlayerPrefs.GetInt("Para");
                GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
                dogrucevap.GetComponent<parascript>().incPara = -1500;
                 Destroy(Instantiate(dogrucevap), 3.1f);
                PlayerPrefs.SetInt("Para", temp -= 1500);

                if (currentquestion.answ1 == currentquestion.correctansw)
                {
                    answ[0].GetComponent<Image>().color = Color.green;
                    scenecontroller.Soru -= 1;
                    scenecontroller.bar_dogru(scenecontroller.Soru);
                }
                if (currentquestion.answ2 == currentquestion.correctansw)
                {
                    answ[1].GetComponent<Image>().color = Color.green;
                    scenecontroller.Soru -= 1;
                    scenecontroller.bar_dogru(scenecontroller.Soru);
                }
                if (currentquestion.answ3 == currentquestion.correctansw)
                {
                    answ[2].GetComponent<Image>().color = Color.green;
                    scenecontroller.Soru -= 1;
                    scenecontroller.bar_dogru(scenecontroller.Soru);
                }
                if (currentquestion.answ4 == currentquestion.correctansw)
                {
                    answ[3].GetComponent<Image>().color = Color.green;
                    scenecontroller.Soru -= 1;
                    scenecontroller.bar_dogru(scenecontroller.Soru);
                }
            }
            else
            {
                Instantiate(Resources.Load("GameObject/ParaYok") as GameObject);
            }
        }

    }
    
    public void AnswerTheQuestion() //SORU CEVAPLANIRSA
    {
        
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.red;
            btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.gameObject; //BUTONUN 1.COCUK OBJESINI YANİ TEXT İNİ ÇAĞIRIR.
            if (btn.transform.GetChild(0).GetComponent<Text>().text == currentquestion.correctansw) //GİRİLEN CEVABI KONTROL EDER BUTTONUN TEXTİNE GÖRE EGER TEXT İLE CORRECTANSWER EŞLEŞİRSE CALISIR
            {
                btn.GetComponent<Image>().color = Color.green;
                scenecontroller.Soru -= 1;
                scenecontroller.bar_dogru(scenecontroller.Soru);
            }
            else //YANLIŞ CEVAPLANIRSA BUTTONU SİLER
            {

                scenecontroller.Soru -= 1;
                scenecontroller.bar_yanlis(scenecontroller.Soru);
                for (int i = 0; i < 4; i++)
                {
                    if (answ[i].transform.GetChild(0).GetComponent<Text>().text == currentquestion.correctansw)
                    {
                        answ[i].GetComponent<Image>().color = Color.green;
                        return;
                    }
                }
            }
     
        
    }
}

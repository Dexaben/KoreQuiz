using UnityEngine;
using UnityEngine.UI;

public class BoslukDoldurma : MonoBehaviour {
    public SceneController scenecontroller;
    public Question_boslukdoldurma[] question;
    private Question_boslukdoldurma currentquestion;
    public Text answer_Text;
    public Text soru_Text;
    public Text ipucu_Text;
    [SerializeField] GameObject dogrucevap;
    int rand;

    private void Start()
    {
        scenecontroller = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
      
        SetCurrentQuestion();
    }
   
    void SetCurrentQuestion()
    {
        
            System.Collections.Generic.List<string> puzzletemp = new System.Collections.Generic.List<string>();
            puzzletemp.Clear();
            while (puzzletemp.Count < question.Length)
            {
                rand = Random.Range(0, question.Length);
                string puzzletemps = "boslukdoldurma" + rand;
                if (!PlayerPrefs.HasKey("boslukdoldurma" + rand))
                {
                    currentquestion = question[rand];
                    soru_Text.text = currentquestion.soru;
                    ipucu_Text.text = currentquestion.ipucu;
                    PlayerPrefs.SetInt("boslukdoldurma" + rand, 1);
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

                scenecontroller.Soru -= 1;
                scenecontroller.bar_dogru(scenecontroller.Soru);
                dogrucevap = Resources.Load("GameObject/EmojiDogru") as GameObject;
                dogrucevap.transform.GetChild(1).GetComponent<Text>().text = currentquestion.correctansw;
                Destroy(Instantiate(dogrucevap), 2);
            }
            else
            {
                Instantiate(Resources.Load("GameObject/ParaYok") as GameObject);
            }
        }

    }
    public void AnswerTheQuestion() //SORU CEVAPLANIRSA
    {
      
            string correctanswer = answer_Text.text;   //GİRİLEN CEVABIN BOŞLUKLARI SİLİNİR "İ" HARFİ "I" YA DÖNÜŞTÜRÜLÜR
            correctanswer = correctanswer.ToUpper();
            char[] a = correctanswer.ToCharArray();
            correctanswer = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != ' ' && a[i] != 'İ')
                {
                    correctanswer += a[i];
                }
                if (a[i] == 'İ')
                {
                    correctanswer += 'I';
                }
            }

            string correctanswer_question = currentquestion.correctansw;
            char[] x = correctanswer_question.ToCharArray();
            correctanswer_question = "";
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != ' ' && x[i] != 'İ')
                {
                    correctanswer_question += x[i];
                }
                if (x[i] == 'İ')
                {
                    correctanswer_question += 'I';
                }
            }
            correctanswer_question = correctanswer_question.ToUpper();

            if (correctanswer == correctanswer_question) //DOGRU CEVAPLANIRSA
            {
                scenecontroller.Soru -= 1;
                scenecontroller.bar_dogru(scenecontroller.Soru);
            }
            else //YANLIŞ CEVAPLANIRSA
            {
                scenecontroller.Soru -= 1;
                scenecontroller.bar_yanlis(scenecontroller.Soru);
                dogrucevap = Resources.Load("GameObject/EmojiDogru") as GameObject;
                dogrucevap.transform.GetChild(1).GetComponent<Text>().text = currentquestion.correctansw;
                Destroy(Instantiate(dogrucevap), 2);
            }
        }
        
 
}

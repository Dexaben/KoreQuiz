using UnityEngine;
using UnityEngine.UI;

public class TFGameManager : MonoBehaviour
{
    public SceneController scenecontroller;
    public Question[] questions;
   
    private Question currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField] GameObject[] btns = new GameObject[2];


    int rand;
    void Start()
    {
        scenecontroller = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
      
        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
       
            System.Collections.Generic.List<string> puzzletemp = new System.Collections.Generic.List<string>();
            puzzletemp.Clear();
            while (puzzletemp.Count < questions.Length)
            {
                rand = Random.Range(0, questions.Length);
                string puzzletemps = "dogruyanlis" + rand;
                if (!PlayerPrefs.HasKey("dogruyanlis" + rand))
                {
                    currentQuestion = questions[rand];
                    factText.text = currentQuestion.fact;
                    PlayerPrefs.SetInt("dogruyanlis" + rand, 1);
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
        if(PlayerPrefs.HasKey("Para"))
        {
            if(PlayerPrefs.GetInt("Para") >= 1500)
            {
                int temp = PlayerPrefs.GetInt("Para");
                GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
                dogrucevap.GetComponent<parascript>().incPara = -1500;
                Destroy(Instantiate(dogrucevap), 3.1f);
                PlayerPrefs.SetInt("Para", temp -= 1500);
                if (currentQuestion.isTrue == false)
                {
                    scenecontroller.Soru -= 1;
                    scenecontroller.bar_dogru(scenecontroller.Soru);
                    btns[1].GetComponent<Image>().color = Color.green;
                }
                if (currentQuestion.isTrue == true)
                {
                    scenecontroller.Soru -= 1;
                    scenecontroller.bar_dogru(scenecontroller.Soru);
                    btns[0].GetComponent<Image>().color = Color.green;
                }
            }
            else
            {
                Instantiate(Resources.Load("GameObject/ParaYok") as GameObject);
            }
        }
   
    }

    public void UserSelectTrue()
    {
        
            if (currentQuestion.isTrue)
            {
                scenecontroller.Soru -= 1;
                scenecontroller.bar_dogru(scenecontroller.Soru);
                btns[0].GetComponent<Image>().color = Color.green;
            }
            else
            {
                scenecontroller.Soru -= 1;
                scenecontroller.bar_yanlis(scenecontroller.Soru);
                btns[0].GetComponent<Image>().color = Color.red;
                btns[1].GetComponent<Image>().color = Color.green;
            }
     
      

    }

    public void UserSelectFalse()
    {
       
            if (!currentQuestion.isTrue)
            {

                scenecontroller.Soru -= 1;
                scenecontroller.bar_dogru(scenecontroller.Soru);
                btns[1].GetComponent<Image>().color = Color.green;
            }
            else
            {
                scenecontroller.Soru -= 1;
                scenecontroller.bar_yanlis(scenecontroller.Soru);
                btns[0].GetComponent<Image>().color = Color.green;
                btns[1].GetComponent<Image>().color = Color.red;
            }
        }
        

 
}

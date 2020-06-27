using UnityEngine;
using UnityEngine.UI;

public class DuzSoruScript : MonoBehaviour {
    public SceneController scenecontroller;
    public Question_duzSoru[] question;
    private Question_duzSoru currentquestion;
    public Text soruText;
    public GameObject[] answ = new GameObject[4];
    
    int rand;
    GameObject btn;

    void Start () {
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
                string puzzletemps = "duzsoru" + rand;
                if (!PlayerPrefs.HasKey("duzsoru" + rand))
                {
                    currentquestion = question[rand];
                    soruText.text = currentquestion.questions;
                    answ[0].transform.GetChild(0).GetComponent<Text>().text = currentquestion.answ1;
                    answ[1].transform.GetChild(0).GetComponent<Text>().text = currentquestion.answ2;
                    answ[2].transform.GetChild(0).GetComponent<Text>().text = currentquestion.answ3;
                    answ[3].transform.GetChild(0).GetComponent<Text>().text = currentquestion.answ4;
                    PlayerPrefs.SetInt("duzsoru" + rand, 1);
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
    public void AnswerTheQuestion()
    {
       
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.red;
            btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.gameObject;

            if (btn.transform.GetChild(0).GetComponent<Text>().text == currentquestion.correctansw)
            {
                btn.GetComponent<Image>().color = Color.green;
                scenecontroller.Soru -= 1;
                scenecontroller.bar_dogru(scenecontroller.Soru);
            }
            else
            {
                scenecontroller.Soru -= 1;
                scenecontroller.bar_yanlis(scenecontroller.Soru);
                for (int i = 0; i < 4; i++)
                {
                    if (answ[i].transform.GetChild(0).GetComponent<Text>().text == currentquestion.correctansw)
                    {
                        answ[i].GetComponent<Image>().color = Color.green;
                    }
                }
            }
    
        
    }
}

using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SceneController scenecontroller;
    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;

    public System.Collections.Generic.List<Sprite> gamePuzzles = new System.Collections.Generic.List<Sprite>();

    public System.Collections.Generic.List<Button> btns = new System.Collections.Generic.List<Button>();

    private bool firstGuess, secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    private void Awake()
    {
        scenecontroller = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        
       
        puzzles = Resources.LoadAll<Sprite>("KartEslestirme/Sprites");
    }

    private void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles()
    {
        int sayi = 0;
        int[] liste = new int[6];
        sayi = Random.Range(0, puzzles.Length);
        liste[0] = sayi;
        for (int i = 0; i < liste.Length; i++)
        {
            sayi = UnityEngine.Random.Range(0, puzzles.Length);
            while (System.Array.IndexOf(liste, sayi) != -1)
            {
                sayi = Random.Range(0, puzzles.Length);
            }
            liste[i] = sayi;
        }


        int looper = btns.Count;
        int index = 0;

        for (int i = 0; i < looper; i++)
        {
            if(index == looper/2)
            {
            
                index = 0;
            }

            gamePuzzles.Add(puzzles[liste[index]]);

            index++;
        }
    }

    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if(!firstGuess)
        {
            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
        }
        else if(!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            countGuesses++;
            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }


    System.Collections.IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(.5f);

        if(firstGuessPuzzle == secondGuessPuzzle && btns[firstGuessIndex] != btns[secondGuessIndex])
        {
            yield return new WaitForSeconds(.2f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();
        }
        else
        {
            yield return new WaitForSeconds(.2f);
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }

        yield return new WaitForSeconds(.2f);
        firstGuess = secondGuess = false;

    }

    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {
            scenecontroller.Soru -= 1;
            scenecontroller.bar_dogru(scenecontroller.Soru);
        }
    }
    public void soruyudogrugec()
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
            }
            else
            {
                Instantiate(Resources.Load("GameObject/ParaYok") as GameObject);
            }
        }
    }
    public void Soruyu_Gec()
    {
      
            scenecontroller.Soru -= 1;
            scenecontroller.bar_yanlis(scenecontroller.Soru);
      
       
    }
    void Shuffle(System.Collections.Generic.List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

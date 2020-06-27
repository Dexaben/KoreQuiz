using UnityEngine;

public class hazirolScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text level_text;
    [SerializeField] private UnityEngine.UI.Text timer_text;

  
     GameManager gameManager;
    float Timer = 3;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        level_text.text = "LEVEL " + gameManager.currentLevel;
       
    }
    void Update()
    {
        if (Timer >= 0.0f)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 3 && Timer > 2)
            {
                timer_text.text = "3";
            }
            if (Timer <= 2 && Timer > 1)
            {
                timer_text.text = "2";
            }
            if (Timer <= 1 && Timer > 0)
            {
                timer_text.text = "1";
            }
        }
    }
}


using UnityEngine;
using UnityEngine.UI;

public class skorslider : MonoBehaviour {
    [SerializeField] private System.Collections.Generic.List<GameObject> alphabets = new System.Collections.Generic.List<GameObject>();
    [SerializeField] private Slider skorSlider;
    [SerializeField] private Text skorText;
    [SerializeField] private SceneController scncontroller;
    public float  skor_;
    bool pause = true;
    void Start()
    {
        scncontroller = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
        pause = true;
        skor_ = 0;
        skorSlider.value = 0f;
        scncontroller.levelalphabe = "noStar";
        for (int i = 0; i < 3; i++)
        {
            Image image = alphabets[i].GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 0.5f;
            image.color = tempColor;
        }
    }
    void Update()
    { 
        if(pause == false)
        {
            if (skorSlider.value >= skor_)
            {
                skorText.text = (skor_).ToString();
                skorSlider.value = skor_;
                pause = true;
            }
            else
            {
                skorSlider.value += 3f * Time.deltaTime;
              
            }
        }
          
    }
    public void skorUpdate(int skor) {
        skor_ = skor;
        pause = false;
        
        if(skor >= 0 && skor < 20)
        {
            for(int i =0; i<3; i++)
            {
                Image image = alphabets[i].GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 0.5f;
                image.color = tempColor;
            }
           
            
        }
        if (skor >= 20 && skor < 40) 
        {
            scncontroller.levelalphabe = "oneStar";
            Image image = alphabets[0].GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
        if (skor >= 40 && skor < 60) 
        {
            scncontroller.levelalphabe = "twoStar";
            Image image = alphabets[1].GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
        if (skor >= 60) 
        {
            scncontroller.levelalphabe = "threeStar";
            Image image = alphabets[2].GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
      
    }
}

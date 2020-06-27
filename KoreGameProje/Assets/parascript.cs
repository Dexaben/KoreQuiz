using UnityEngine;

public class parascript : MonoBehaviour {
    private float mevcutpara;
    public float incPara;
    int finishPara;
    bool start = false;
    public UnityEngine.UI.Text para_text;
    void OnEnable () {
        start = false;
        if (incPara == 0)
        {
            PlayerPrefs.GetInt("Para");
        }
        mevcutpara = PlayerPrefs.GetInt("Para");
        para_text.text = (int)mevcutpara + " KP";
        finishPara = (int)mevcutpara + (int)incPara;

            incPara = incPara / 150f;
            Invoke("START", 0.5F);
       
    }
    void START()
    {
        start = true;
    }
	void Update () {

            if (start)
            {
                if (incPara > 0)
                {
                    mevcutpara += (float)incPara * Time.deltaTime * 100;
                    para_text.text = (int)mevcutpara + " KP";
                    para_text.color = Color.green;
                    if ((int)mevcutpara >= finishPara)
                    {
                        para_text.text = finishPara + " KP";
                        para_text.color = Color.white;
                        start = false;
                    }
                }
                else if (incPara < 0)
                {
                    mevcutpara += (float)incPara * Time.deltaTime * 100;
                    para_text.text = (int)mevcutpara + " KP";
                    para_text.color = Color.red;
                    if ((int)mevcutpara <= finishPara)
                    {
                        para_text.text = finishPara + " KP";
                        para_text.color = Color.white;
                        start = false;
                    }
                }

            }

        }
}

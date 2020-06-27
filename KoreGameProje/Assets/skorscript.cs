
using UnityEngine;

public class skorscript : MonoBehaviour
{
    private float mevcutSkor;
    public float incSkor;
    int finishSkor;
    bool start = false;
    public UnityEngine.UI.Text skor_Text;
    void OnEnable()
    {
        start = false;
        if (incSkor == 0)
        {
            PlayerPrefs.GetInt("AnaSkor");
        }
        mevcutSkor = PlayerPrefs.GetInt("AnaSkor");
        skor_Text.text = "SKOR " + (int)mevcutSkor;
        finishSkor = (int)mevcutSkor + (int)incSkor;

        incSkor = incSkor / 150f;
        Invoke("START", 0.5F);

    }
    void START()
    {
        start = true;
    }
    void Update()
    {

        if (start)
        {

            mevcutSkor += (float)incSkor * Time.deltaTime * 100;
            skor_Text.text = "SKOR "+(int)mevcutSkor;
            skor_Text.color = Color.yellow;
            if ((int)mevcutSkor >= finishSkor)
            {
                skor_Text.text = "SKOR "+finishSkor;
                skor_Text.color = Color.white;
                start = false;

            }
        }
    }
}

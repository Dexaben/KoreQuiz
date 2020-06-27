using UnityEngine;

public class alphabetscript : MonoBehaviour
{
    [SerializeField] private System.Collections.Generic.List<Sprite> alphabeImages;
    [SerializeField] private UnityEngine.UI.Image image;
    public string a;
    void Start()
    {
        switch (a)
        {
            case "noStar":
                gameObject.SetActive(false);
                break;
            case "oneStar":
                image.sprite = alphabeImages[0];
                break;
            case "twoStar":
                image.sprite = alphabeImages[1];
                break;
            case "threeStar":
                image.sprite = alphabeImages[2];
                break;
        }
    }
}

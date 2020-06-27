using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BuyButton : MonoBehaviour
{
   [SerializeField] private Purchaser purchaser;
    public enum ItemType
    {
        ALTIN10000,
        ALTIN50000,
        ELMAS25,
        ELMAS100,
        UnliminatedEnergy,
    }
    public ItemType itemtype;
    public Text priceText;
    private string defaultText;
    void Awake()
    {
        defaultText = priceText.text;
        StartCoroutine(LoadPriceRoutine());
    }
    public void ClickBuy()
    {
        switch(itemtype)
        {
            case ItemType.ALTIN10000:
                purchaser.Buy10000altin();
                break;
            case ItemType.ALTIN50000:
                purchaser.Buy50000altin();
                break;
            case ItemType.ELMAS25:
                purchaser.Buy25elmas();
                break;
            case ItemType.ELMAS100:
                purchaser.Buy100elmas();
                break;
            case ItemType.UnliminatedEnergy:
                purchaser.buyUnliminatedEnergy();
                break;
          
        }
    }
    private IEnumerator LoadPriceRoutine()
    {
        while(!purchaser.IsInitialized())
        {
            yield return null;
        }
        string loadedPrice = "";
        switch (itemtype)
        {
            case ItemType.ALTIN10000:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.ALTIN_10000);
                break;
            case ItemType.ALTIN50000:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.ALTIN_50000);
                break;
            case ItemType.ELMAS25:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.ELMAS_25);
                break;
            case ItemType.ELMAS100:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.ELMAS_100);
                break;
            case ItemType.UnliminatedEnergy:
                loadedPrice = purchaser.GetProducePriceFromStore(purchaser.UnliminatedEnergy);
                break;
           
        }
        priceText.text = defaultText + "  (" + loadedPrice+")";
    }
}

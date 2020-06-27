using System;
using UnityEngine;
using UnityEngine.Purchasing;


    // Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
    public class Purchaser : MonoBehaviour, IStoreListener 
    {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        // Product identifiers for all products capable of being purchased: 
        // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
        // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
        // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

        // General product identifiers for the consumable, non-consumable, and subscription products.
        // Use these handles in the code to reference which product to purchase. Also use these values 
        // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
        // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
        // specific mapping to Unity Purchasing's AddProduct, below.
      public string ALTIN_10000 = "altin_10000";
      public  string ALTIN_50000 = "altin_50000";
    public string ELMAS_25 = "elmas_25";
  public  string ELMAS_100 = "elmas_100";
 public  string UnliminatedEnergy = "unliminated_energy";


        void Start()
        {
      ALTIN_10000 = "altin_10000";
    ALTIN_50000 = "altin_50000";
     ELMAS_25 = "elmas_25";
    ELMAS_100 = "elmas_100";
        UnliminatedEnergy = "unliminated_energy";

            // If we haven't set up the Unity Purchasing reference
            if (m_StoreController == null)
            {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();
            }
        }

        public void InitializePurchasing()
        {
            // If we have already connected to Purchasing ...
            if (IsInitialized())
            {
                // ... we are done here.
                return;
            }

            // Create a builder, first passing in a suite of Unity provided stores.
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            // Add a product to sell / restore by way of its identifier, associating the general identifier
            // with its store-specific identifiers.
            builder.AddProduct(ALTIN_10000, ProductType.Consumable);
            builder.AddProduct(ALTIN_50000, ProductType.Consumable);
        builder.AddProduct(ELMAS_25, ProductType.Consumable);
        builder.AddProduct(ELMAS_100, ProductType.Consumable);
        
        builder.AddProduct(UnliminatedEnergy, ProductType.NonConsumable);

            // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
            // if the Product ID was configured differently between Apple and Google stores. Also note that
            // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
            // must only be referenced here. 


            // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
            // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
            UnityPurchasing.Initialize(this, builder);
        }


        public bool IsInitialized()
        {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        public void Buy10000altin()
        {
            // Buy the consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(ALTIN_10000);
        }
        public void Buy50000altin()
        {
            // Buy the consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(ALTIN_50000);
        }
    public void Buy25elmas()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(ELMAS_25);
    }

    public void Buy100elmas()
    {
        // Buy the consumable product using its general identifier. Expect a response either 
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        BuyProductID(ELMAS_100);
    }




    public void buyUnliminatedEnergy()
        {
            // Buy the non-consumable product using its general identifier. Expect a response either 
            // through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(UnliminatedEnergy);
        }
  
    public string GetProducePriceFromStore(string id)
        {
            if(m_StoreController!=null&&m_StoreController.products !=null)
            {
                return m_StoreController.products.WithID(id).metadata.localizedPriceString;
            }
            else
            {
                return "";
            }
        }


        void BuyProductID(string productId)
        {
            // If Purchasing has been initialized ...
            if (IsInitialized())
            {
                // ... look up the Product reference with the general product identifier and the Purchasing 
                // system's products collection.
                Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else
                {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else
            {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }


        //  
        // --- IStoreListener
        //

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
        }


        public void OnInitializeFailed(InitializationFailureReason error)
        {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
        // A consumable product has been purchased by this user.
        if (String.Equals(args.purchasedProduct.definition.id, ALTIN_10000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
            dogrucevap.GetComponent<parascript>().incPara = 10000;
            Destroy(Instantiate(dogrucevap), 3.1f);
            int temp = PlayerPrefs.GetInt("Para");
            PlayerPrefs.SetInt("Para", temp += 10000);

        }
        else if (String.Equals(args.purchasedProduct.definition.id, ALTIN_50000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            GameObject dogrucevap = Resources.Load("GameObject/Para") as GameObject;
            dogrucevap.GetComponent<parascript>().incPara = 50000;
            Destroy(Instantiate(dogrucevap), 3.1f);
            int temp = PlayerPrefs.GetInt("Para");
            PlayerPrefs.SetInt("Para", temp += 50000);

        }
        else if (String.Equals(args.purchasedProduct.definition.id, ELMAS_25, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
         
            int temp = PlayerPrefs.GetInt("Elmas");
            PlayerPrefs.SetInt("Elmas", temp += 25);

        }
        else if (String.Equals(args.purchasedProduct.definition.id, ELMAS_100, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            
            int temp = PlayerPrefs.GetInt("Elmas");
            PlayerPrefs.SetInt("Elmas", temp += 100);

        }

        else if (String.Equals(args.purchasedProduct.definition.id, UnliminatedEnergy, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("UnliminatedEnergy", 1);
        }
       
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 
            return PurchaseProcessingResult.Complete;
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }
    }

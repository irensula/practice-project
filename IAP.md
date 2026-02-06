[Unity docs](https://docs.unity.com/en-us/iap)

# ADD PAYMENT METHOD IN UNITY

## 1. Install Unity IAP 

    Window → Package Manager → In App Purchasing → Install 

 

## 2. Connect Project to Unity Services 

    Edit → Project Settings → Services 

    Enable In-App Purchasing 

    Make sure the project is linked to a valid Unity Project ID 
 
## 3. Open IAP Catalog 

    Services → In-App Purchasing → IAP Catalog 

    Add a product: 

    ID: buygame (choose the name yourself) 

    Type: Non-Consumable 

    Locale: Google Play 

    Title / Description / Price → fill in 

    Click Add Product 

## 4. Setup Purchase Button 

Create a UI Button in your scene (UI → Button) 

Add component IAP Button 

    Product ID: buygame 

    Button Type: Purchase 

In On Purchase Complete, add the method Purchased from the script below 

## 5. Setup IAP Listener 

    Create an empty GameObject, name it IAP_Manager 

    Add component IAP Listener 

    Dont Destroy On Load: check 

## 6. Purchase Script  

Create a C# script named IAPScript.cs and attach it to a game object in Hierarchy IAP_Manager: 

using UnityEngine; 
using UnityEngine.Purchasing; 
using UnityEngine.SceneManagement; 

public class IAPScript : MonoBehaviour 
{ 
    public void Purchased(Product product) 
    { 
        if (product.definition.id == "buygame") 
        { 
            UnityEngine.SceneManagement.SceneManager.LoadScene("FullGameScene"); 
        } 
    } 
} 

## 7. Test Purchases (Internal Testing) 

    Go to Google Play Console → Internal Testing 

    Add tester emails (Gmail accounts) 

    Upload your build (.aab) 

    Test the purchase using Google Play test cards 

    Verify that PlayerPrefs stores the purchase and unlocks the full game content 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject shopPanel; // ---> ShopPanel'ini trigger'lamak için atadığımız değişken.
    public int currentSelectedItem; // ---> Seçilen itemi izlemek için atadığımız değişken.
    public int currentItemCost; // ---> Seçilen Itemın fiyatını atadığımız değişken.
    private bool itemSold = false;

    private Player _player;
    public MossGiant_Boss mossGiant_Boss;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.tag == "Player") // ---> Tag'i Player olan obje geldiğinde shopPanel'i aç.
        {

            _player = other.GetComponent<Player>();

            if (_player != null) // ---> Collider'a çarpan şey null'a eşit olmadığında.
            {

                UIManager.Instance.OpenShop(_player.diamonds); // ---> UIManager'da ki Instance'ı ve OpenShop methodunu çağırıyoruz. UIManager'a buradan bilgi gönderiyoruz.
                UIManager.Instance.UpdateGemCount(_player.diamonds);

            }

            shopPanel.SetActive(true);

        }

    }
    private void OnTriggerExit2D(Collider2D other) // ---> OnTriggerExit kullandık ki Player Collider'dan çıktığında kapansın.
    {
        
        if (other.tag == "Player") // ---> Tag'i Player olan obje geldiğinde shopPanel'i aç.
        {

            shopPanel.SetActive(false);

        }

    }

    public void SelectItem(int item) // ---> Int Selection'ı atayarak hangi itemi seçtiğimizi belirtecez.
    {

        switch(item)
        {

            case 0:
                UIManager.Instance.UpdateShopSelection(58); // ---> UIManager'da atadığımız Selection görselini burada case içerisinde yer değiştiriyoruz.
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            
            case 1:
                UIManager.Instance.UpdateShopSelection(-25); // ---> UIManager'da atadığımız Selection görselini burada case içerisinde yer değiştiriyoruz.
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;

            case 2:
                UIManager.Instance.UpdateShopSelection(-117); // ---> UIManager'da atadığımız Selection görselini burada case içerisinde yer değiştiriyoruz.
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;

        }

    }

    public void BuyItem() // ---> Itemi alırken çalıştıracağımız method.
    {

        mossGiant_Boss = FindObjectOfType<MossGiant_Boss>();
        
        if (_player.diamonds >= currentItemCost) // ---> Player'daki gemler atadığımız currentItemCost'a eşit yada büyükse
        {

            if (currentSelectedItem == 0)
            {

                _player.PlayerDamageMultiplier();
                _player.diamonds -= currentItemCost;
                UIManager.Instance.OpenShop(_player.diamonds);
                UIManager.Instance.UpdateGemCount(_player.diamonds);

            }
            
            if (currentSelectedItem == 1)
            {

                _player.BootsOfFlight();
                _player.diamonds -= currentItemCost;
                UIManager.Instance.OpenShop(_player.diamonds);
                UIManager.Instance.UpdateGemCount(_player.diamonds);

            }
            
            if (currentSelectedItem == 2 && itemSold == false)
            {

                GameManager.Instance.HasKeyToCastle = true; // ---> GameManager'daki atadığımız bool'u burada true'ya çeviriyoruz.
                itemSold = true;
                mossGiant_Boss.dmgToBoss = true;
                _player.diamonds -= currentItemCost;
                UIManager.Instance.OpenShop(_player.diamonds);
                UIManager.Instance.UpdateGemCount(_player.diamonds);

            }
            else
            {
            
                Debug.Log("asd");

            }
        
        }

    }

}

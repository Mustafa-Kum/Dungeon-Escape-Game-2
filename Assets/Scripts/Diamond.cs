using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    public int gems; // ---> Gemlerin değerini atadığımız değişken. 

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.tag == "Player") // ---> Player bu objeye çarptığında.
        {

            Player player = other.GetComponent<Player>(); // ---> Other'dan Player'ın özelliklerini çektik.
            
            if (player != null) // ---> Player olduğuna dair kontrol.
            {

                player.AddGems(gems); // ---> Player Script'indeki AddGem method'unu alıyoruz ve içine gems değişkenini koyuyoruz.
                Destroy(this.gameObject); // ---> Bu objeyi yok et.

            }

        }

    }

}

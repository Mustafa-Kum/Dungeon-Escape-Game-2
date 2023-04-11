using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour, IDamageable
{

    private Vector3 player; // ---> Vector olarak Player'ın pozisyonunu almak için atadığımız değişken.

    public int Health { get; set;} // ---> IDamageable'dan aldığımız değişkenleri buraya koymak zorundayız ki Damage verilebilen bir hale gelsin.
    private int health2 = 1;

    private void Start() 
    {

        player = GameObject.FindGameObjectWithTag("Player").transform.position; // ---> Tag'i Player olan gameobjenin pozisyonunu alıyoruz.
        Health = health2; // ---> Interface'deki bahsettiğimiz Health değişkinini Enemy'deki health değişkenine eşitledik.
       
        Destroy(this.gameObject, 3.0f); // ---> Herhangi bir şeye vurmazsak 5 saniye sonra objeyi yok et.
        
    }
    
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, player, 3 * Time.deltaTime); // ---> Player'ın olduğu yere doğru ilerleyecek.
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.CompareTag("Player")) // ---> Eğer tag'i Player ise.
        {

            Player _player = other.GetComponent<Player>(); // ---> Herhangi bir nesnede IDamageable interface'i var varsa hit'e eşitle.

            if (_player != null) // ---> Eğer ki herhangi bir şeye vurursak
            {

                _player.Damage(); // ---> IDamageable Interface'indeki Damage methodunu çağır.

            }
            
            Destroy(this.gameObject);

        }

    }

    public void Damage() // ---> IDamageable interface'inde sözleşme olarak bahsettiğimiz değişkenleri burada kullanmamız gerekiyor.
    {

        Health--; // ---> Damage methodu çağrıldığında Healt -1 aşağı inecek.
        Destroy(this.gameObject);

    }

}

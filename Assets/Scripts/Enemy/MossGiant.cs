using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable // ---> Mono yerine Enemy yaptık çünkü Enemy Script'inden her şeyi alacağız. IDamageable interface'i buradan implemente ediyoruz.
{

    public int Health { get; set; } // ---> IDamageable interface'inde sözleşme olarak bahsettiğimiz değişkenleri burada kullanmamız gerekiyor.
    public Player _player;
    
    public override void Init() // ---> Enemy scriptindeki Init'i burada override ediyoruz çünkü gereksiz yere Start fonksiyonunu kullanmaya gerek yok.
    {

        _player = GameObject.Find("Player").GetComponent<Player>();
        base.Init(); // ---> Bu kod Enemy'deki Init fonksiyonunu tam olarak çağıracak sonra bu kodun devamında yazdığımız şeyler çağrılacak.
        Health = base.health; // ---> Interface'deki bahsettiğimiz Health değişkinini Enemy'deki health değişkenine eşitledik.

    }

    public void Damage() // ---> IDamageable interface'inde sözleşme olarak bahsettiğimiz değişkenleri burada kullanmamız gerekiyor.
    {

        Health = Health - _player.PlayerDamage; // ---> Damage methodu çağrıldığında Health -1 aşağı inecek.
        _anim.SetTrigger("Hit"); // ---> Animator'de atadığımız trigger parametresi olan Hit burada Trigger'lanıyor.
        isHit = true; // ---> Player Enemy'e vurduğu zaman Enemy yürümeyi durdurması için oluşturduğumuz değişkeni burada true yapıyoruz.
        _anim.SetBool("InCombat", true);

        if (Health < 1 && !isDead) // ---> Health değişkeni 1'den aşağı bir sayı olduğunda.
        {

            isDead = true;
            _anim.SetTrigger("Death"); // ---> Animator'deki Death Trigger'ını burada Trigger'lıyoruz.
            Destroy(GetComponent<Collider2D>());
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject; // ---> Öldüğünde diamond'ı spawnlıyoruz ve bu değişkeni bir oyun nesnesine dönüştürüyoruz.
            diamond.GetComponent<Diamond>().gems = base.gems; // ---> Diamond'ın Componentlerine ulaşıp gem sayısını değiştirmek için kulanndığımız yer.

        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable // ---> Mono yerine Enemy yaptık çünkü Enemy Script'inden her şeyi alacağız. IDamageable interface'i buradan implemente ediyoruz.
{

    public int Health { get; set; } // ---> IDamageable interface'inde sözleşme olarak bahsettiğimiz değişkenleri burada kullanmamız gerekiyor.
    public GameObject acidEffectPrefab; // ---> Prefab yaptığımız acideffectprefab'e ulaşmak için atadığımız değişken.

    public override void Init() // ---> Enemy scriptindeki Init'i burada override ediyoruz çünkü gereksiz yere Start fonksiyonunu kullanmaya gerek yok.
    {

        base.Init(); // ---> Bu kod Enemy'deki Init fonksiyonunu tam olarak çağıracak sonra bu kodun devamında yazdığımız şeyler çağrılacak.
        Health = base.health; // ---> Interface'deki bahsettiğimiz Health değişkinini Enemy'deki health değişkenine eşitledik.

    }

    public void Damage() // ---> IDamageable interface'inde sözleşme olarak bahsettiğimiz değişkenleri burada kullanmamız gerekiyor.
    {

        Health--; // ---> Damage methodu çağrıldığında Healt -1 aşağı inecek.

        if (Health < 1 && !isDead) // ---> Health değişkeni 1'den aşağı bir sayı olduğunda.
        {

            isDead = true;
            _anim.SetTrigger("Death"); // ---> Animator'deki Death Trigger'ını burada Trigger'lıyoruz.
            Destroy(GetComponent<Collider2D>());
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject; // ---> Öldüğünde diamond'ı spawnlıyoruz ve bu değişkeni bir oyun nesnesine dönüştürüyoruz.
            diamond.GetComponent<Diamond>().gems = base.gems; // ---> Diamond'ın Componentlerine ulaşıp gem sayısını değiştirmek için kulanndığımız yer.

        }

    }

    public override void Movement() // ---> Enemy Script'inde bulunan Movement metodunu burada override edecez.
    {

        // ---> Herhangi bir hareket yapmasın diye bir şey yazmıyoruz.

    }

    public void Attack()
    {

        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity); // ---> Prefab'i nerede oluşturup çağıracağımızın kodu.

    }

}

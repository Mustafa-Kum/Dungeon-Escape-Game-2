using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant_Boss : Enemy, IDamageable // ---> Mono yerine Enemy yaptık çünkü Enemy Script'inden her şeyi alacağız. IDamageable interface'i buradan implemente ediyoruz.
{

    public int Health { get; set; } // ---> IDamageable interface'inde sözleşme olarak bahsettiğimiz değişkenleri burada kullanmamız gerekiyor.
    public Player _player;
    public GameObject acidEffectPrefab; // ---> Prefab yaptığımız acideffectprefab'e ulaşmak için atadığımız değişken.
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    public UIManager _uiManager;
    public bool dmgToBoss = false;
    
    public override void Init() // ---> Enemy scriptindeki Init'i burada override ediyoruz çünkü gereksiz yere Start fonksiyonunu kullanmaya gerek yok.
    {

        _player = GameObject.Find("Player").GetComponent<Player>();
        base.Init(); // ---> Bu kod Enemy'deki Init fonksiyonunu tam olarak çağıracak sonra bu kodun devamında yazdığımız şeyler çağrılacak.
        Health = base.health; // ---> Interface'deki bahsettiğimiz Health değişkinini Enemy'deki health değişkenine eşitledik.

    }
    
    public override void Update()
    {

        base.Update();
        Attack();

    }
    
    public void Damage() // ---> IDamageable interface'inde sözleşme olarak bahsettiğimiz değişkenleri burada kullanmamız gerekiyor.
    {
        
        if(dmgToBoss == true)
        {

            Health = Health - _player.PlayerDamage ; // ---> Damage methodu çağrıldığında Health -1 aşağı inecek.
            _anim.SetTrigger("Hit"); // ---> Animator'de atadığımız trigger parametresi olan Hit burada Trigger'lanıyor.
            isHit = true; // ---> Player Enemy'e vurduğu zaman Enemy yürümeyi durdurması için oluşturduğumuz değişkeni burada true yapıyoruz.
            _anim.SetBool("InCombat", true);

        }

        if (Health < 1 && !isDead) // ---> Health değişkeni 1'den aşağı bir sayı olduğunda.
        {

            _uiManager = FindObjectOfType<UIManager>();
            _uiManager.VictorySequence();
            _uiManager.PauseButtonOnDeath();
            isDead = true;
            _player._isDead = false;
            _anim.SetTrigger("Death"); // ---> Animator'deki Death Trigger'ını burada Trigger'lıyoruz.
            Destroy(GetComponent<Collider2D>());

        }

    }

    public void Attack()
    {

        if (Time.time > _canFire && isDead == false)
        {

            _fireRate = 5f;
            _canFire = Time.time + _fireRate;
            Instantiate(acidEffectPrefab, transform.position, Quaternion.identity); // ---> Prefab'i nerede oluşturup çağıracağımızın kodu.

        }

    }

}

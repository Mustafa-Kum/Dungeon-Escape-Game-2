using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour // ---> Abstract = Kesin olarak Enemy'e sahip olan GameObjelerde olacak.
{

    public GameObject diamondPrefab; // ---> Düşmanlardan düşereceğimiz elmasları atadığımız değişken.
    [SerializeField]
    protected int health; // ---> Tüm düşmanların canlarını atamak için oluşturduğumuz health değişkeni.
    [SerializeField]
    protected float speed; // ---> Tüm düşmanların hızlarını atamak için oluşturduğumuz speed değişkeni.
    [SerializeField]
    protected int gems; // ---> Tüm düşmanların gemlerini atamak için oluşturduğumuz gem değişkeni.
    [SerializeField]
    protected Transform pointA, pointB; // ---> Enemy'lerin yürüyecekleri mesafeler.
    
    protected Vector3 currentTarget; // ---> PointA ve PointB'yi target göstermek için oluşturduğumuz değişken.
    protected Animator _anim; // ---> Animator component'ine ulaşmak için atadığımız değişken.
    protected SpriteRenderer sprite; // ---> SpriteRenderer'a ulaşmak için kullanacağımız değişkene atadık.

    protected bool isHit = false; // ---> Player Enemy'e vurduğu anda yürümesini durduması için oluşurduğumuz değişken.
    protected Player player; // ---> Player'dan Componentleri almak için atadığımız değişken.
    protected bool isDead = false; // ---> Objenin ölü olup olmadığını kontrol eden değişken.

    public virtual void Init() // ---> İlk olarak çalıştırılacak şey. Start gibi ama bu script'i alan hepsinde uygulanacak ve diğer script'lerde init'i kullanarak Start fonksiyonunu gereksiz yere kullanmayacaz.
    { 

        _anim = GetComponentInChildren<Animator>(); // ---> Animator zaten Enemy'nin içinde olduğu için component'i almak için direkt GetComponent'ı kullanıyoruz fakat nesnesini döndürmek için Children'da kullandık. Idle'a falan ulaşabilmek için.
        sprite = GetComponentInChildren<SpriteRenderer>(); // ---> SpriteRenderer diğer Enemy'lerde olduğu için component'i almak için direkt GetComponent'ı kullanıyoruz fakat nesnesini döndürmek için Children'da kullandık.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); // ---> Tag'i Player olan gameobjesini bulacak ve Componentlerini alacak.

    }

    private void Start()
    {

        Init(); // ---> Yukarıdaki Init fonksiyonunu burada çağırıyoruz ki çalışsın.

    }
 
    public virtual void Update() // ---> Tüm düşmanların update methodunu oluşturduğumuz ve override yaptırabilceğimiz fonksiyon.
    {
        
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _anim.GetBool("InCombat") == false || isDead == true) // ---> Animator'de hangi state'in çalıştığı bilgisine ulaştığımız yer ve Olan State Idle ve Incombat parametreside false ise hiçbir şey yapma. Or isDead değişkeni false ise hiçbir şey yapma.
        {

            return; // ---> Hiçbir şey yapma.

        }

        if (isDead == false)
        {

            Movement();

        }
        
    }
    
    public virtual void Movement() // ---> Her yarattığımız Enemy Movement özelliğini buradan alacak.
    {

        if (currentTarget == pointA.position) // ---> PointA'ya geldiğin zaman X düzleminde ters dön.
        {

            sprite.flipX = true;

        }
        else // ---> Diğer zamanlarda X düzleminde ters dönme.
        {

            sprite.flipX = false;

        }
        
        if (transform.position == pointA.position) // ---> Bulunduğu yer pointA'ya eşit ise.
        {

            currentTarget = pointB.position; // ---> CurrentTarget pointB yani pointB'ye yürü.
            _anim.SetTrigger("Idle"); // ---> Animator'de "Idle" trigger'ını çalıştırarak Idle animasyonuna geçiş yapar.

        }
        else if (transform.position == pointB.position) // ---> Bulunduğu yer pointB'ya eşit ise.
        {

            currentTarget = pointA.position; // ---> CurrentTarget pointA yani pointA'ya yürü.
            _anim.SetTrigger("Idle"); // ---> Animator'de "Idle" trigger'ını çalıştırarak Idle animasyonuna geçiş yapar.

        }

        if (isHit == false)
        {

            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime); // ---> Yukarıdaki if'leri bağlayan ve yürütme komutu veren yer.

        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition); // ---> Distance değişkeni Distance yardımı ile Player'ın uzaklığına erişecek.

        if (distance > 2.0f) // ---> Atadığımız distance 2 kareden az ise.
        {

            isHit = false;
            _anim.SetBool("InCombat", false); // ---> Animator içerisinde oluşturduğumuz InCombat bool'u false'a çevrilecek.

        }

        Vector3 direction = player.transform.localPosition - transform.localPosition; // ---> Düşmanın Player'ın nerede olduğunu sayısal olarak kontrol eden kod.

        if (direction.x > 0 && _anim.GetBool("InCombat") == true) // ---> Enemy InCombat'ta ve bizim ile arasındaki mesafe 0'dan büyük olduğunda X'i döndür.
        {

            sprite.flipX = false;

        }
        else if (direction.x < 0 && _anim.GetBool("InCombat") == true) // ---> Enemy InCombat'ta ve bizim ile arasındaki mesafe 0'dan küçük olduğunda X'i döndür.
        {

            sprite.flipX = true;

        }
    } 

}

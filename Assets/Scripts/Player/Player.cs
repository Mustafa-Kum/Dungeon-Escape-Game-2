using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds; // ---> Diamond'ları Player'da tutmak için atadığımız değişken.
    private Rigidbody2D _rigid; // ---> Rigidbody component'ine gitmek için atadığımız değişken.
    [SerializeField]
    private float _jumpForce = 5.0f; // ---> Zıplama gücünü atadığımız ve ayarladığımız yer.
    private bool _resetJump = false; // ---> Zıplamaya hafif bir bekleme süresi eklemek için atadığımız değişken.
    [SerializeField]
    private float _speed = 2.8f; // ---> Player'ın hızını kontrol etmek için atadığımız değişken.
    private PlayerAnimation _playerAnim; // ---> PlayerAnimator Script'inin _anim diye bir değişkene atadık.
    private SpriteRenderer _playerSprite; // ---> SpriteRenderer'a ulaşmak için kullanacağımız değişkene atadık.
    private bool _grounded = false;
    private SpriteRenderer _swordArcSprite; // ---> SpriteRenderer'a ulaşmak için kullanacağımız değişkene atadık.
    public bool _isDead = true;
    private float _jumpforceMultiplier = 1.1f;
    public int PlayerDamage = 1;
    public UIManager _uiManager;
    public FixedJoystick _joyStick;
    
    public int Health { get; set;} // ---> IDamageable'dan aldığımız değişkenleri buraya koymak zorundayız ki Damage verilebilen bir hale gelsin.
    
    void Start()
    {
        
        _rigid = GetComponent<Rigidbody2D>(); // ---> Rigidbody zaten Player'ın içinde olduğu için component'i almak için direkt GetComponent'ı kullanıyoruz.
        _playerAnim = GetComponent<PlayerAnimation>(); // ---> PlayerAnimation zaten Player'ın içinde olduğu için component'i almak için direkt GetComponent'ı kullanıyoruz.
        _playerSprite = GetComponentInChildren<SpriteRenderer>(); // ---> SpriteRenderer zaten Player'ın içinde olduğu için component'i almak için direkt GetComponent'ı kullanıyoruz fakat nesnesini döndürmek için Children'da kullandık. 
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>(); // ---> Birden fazla children olduğu için gitmek istediğimizin indexini belirttikten sonra GetComponent<SpriteRenderer>() kullanıyoruz.
        _uiManager = FindObjectOfType<UIManager>();
        Health = 3; // ---> Player'ın Health'ini atadığımız yer.

    }

    void Update()
    {
        
        Movement(); // ---> Player'ın hareket fonksyonunu her saniye çağırıyoruz.

        if(_isDead == true)
        {

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true) // ---> Attack animasyonunu çağıracağımız yer. Space'e bastığında ve IsGrounded fonksiyonu true olduğunda gerçekleşecek.
            {

                _playerAnim.Attack();

            }

        }



    }

    void Movement() // ---> Player'ın tüm hareketlerinin olduğu fonksyon.
    {
        
        if(_isDead == true)
        {

            float move = _joyStick.Horizontal; // ---> Sağ ve Sola hareket etmek için atadığımız tuş yeri. GetAxisRaw -> D veya A ya basmayı kestikten sonra hafif kaymasını önler.
            _grounded = IsGrounded(); // ---> _grounded değişkenini IsGrounded fonksiyonuna atadık ki RayTracer her zaman IsGrounded'ı kontrol etsin.


            if (move > 0) // ---> Karakter animator'de ki değişken 0'dan büyük olduğunda sağ'a yürüyecek.
            {

                Flip(true);

            }
            else if (move < 0) // ---> Karakter animator'de ki değişken 0'dan küçük olduğunda sol'a yürüyecek.
            {

                Flip(false);

            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded() == true) // ---> Space'ye bastığımda ve IsGrounded Fonksiyonu true olduğunda.
            {
                
                _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce); // ---> Zıpladığımızda hızımız aynı kalacak.
                StartCoroutine(ResetJumpRoutine());
                _playerAnim.Jump(true); // ---> PlayerAnimation script'indeki Jump fonksiyonunu burada çağırıyoruz. True ise zıplıyoruz.

            }

            _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y); // ---> Sağ ve Sola hareket ederkenki hızını kontrol ettiğimiz yer. Velocity = Hız.
            _playerAnim.Move(move); // ---> İçerisindeki move yukarıdan yani " float move "'dan 1 değerini alacak ve PlayerAnimation'daki Move'a gönderecek.

        }
        else if (_isDead == false)
        {

            _rigid.velocity = Vector2.zero;

        }
        
        
    
    }
    
    bool IsGrounded()
    {
        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.58f, 1 << 8); // ---> Raycast ışını sadece 8'inci layer'ı göz önünde bulunduracak ve Raycast ışınının boyu ve yönünü ayarladığımız yer.
        
        if (hitInfo.collider != null) // ---> Raycast ışını bir şeye çarparken.
        {

            if (_resetJump == false) // ---> Normal zamanda resetJump zaten false olduğu için burada bekle
            {

                _playerAnim.Jump(false); // ---> PlayerAnimation script'indeki Jump fonksiyonunu burada çağırıyoruz. 
                return true; // ---> True dönerse aşağıdaki false'ı asla çalıştırmayacak.

            }
            
        }

        return false;
    
    }

    void Flip(bool faceRight) // ---> Sağ'a mı ya da Sol'a mı gideceğimizin kararını veren bool fonksiyonu.
    {

        if (faceRight == true) // ---> move değeri 0'dan büyük ise player'ı döndürme.
        {

            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition; // ---> Karakter döndüğünde x,y ve z'nin nerede olduğunu atıyoruz ve tanımlıyoruz.
            newPos.x = 1.01f; // ---> Tanımladığımız x'i burada yeni pozisyona atıyoruz.
            _swordArcSprite.transform.localPosition = newPos; // ---> Tanımladığımız x'i burada transform olarak kullanıyoruz.

        }
        else if (faceRight == false) // ---> move değeri 0'dan küçük ise player'ı döndür.
        {

            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition; // ---> Karakter döndüğünde x,y ve z'nin nerede olduğunu atıyoruz ve tanımlıyoruz.
            newPos.x = -1.01f; // ---> Tanımladığımız x'i burada yeni pozisyona atıyoruz.
            _swordArcSprite.transform.localPosition = newPos; // ---> Tanımladığımız x'i burada transform olarak kullanıyoruz.

        }

    }

    IEnumerator ResetJumpRoutine() // ---> Zıplamaya hafif bekleme süresi ekleyeceğimiz yer.
    {

        _resetJump = true; // ---> True olduğunda 0.1 saniye bekleyecek ve hemen ardından false olacak.
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;

    }
    
    public void Damage()
    {

        if (Health < 1)
        {

            return;

        }

        Debug.Log("Player");
        Health --; // ---> Player Dmg yediğinde gidecek olan Health değeri.
        StartCoroutine(FlashRed());
        UIManager.Instance.UpdateLives(Health); // ---> UIManager'daki UpdateLives methoduna Health değişkenimizi veriyoruz.

        if(Health < 1)
        {

            _playerAnim.Death();
            _isDead = false;
            _uiManager.GameOverSequence();
            _uiManager.PauseButtonOnDeath();

        }
        
    }

    public void AddGems(int amount) // ---> Gem aldığımızda bu sayıyı kontrol etmek için atadığımız method.
    {

        diamonds += amount; // ---> Diamond script'inden aldığımız değeri burada amount olarak arttırıyoruz.
        UIManager.Instance.UpdateGemCount(diamonds); // ---> Eşitlediğimiz diamonds değişkenini UIManager'da ki UpdateGemCount methoduna veriyoruz.

    }

    public void BootsOfFlight()
    {
        
        _jumpForce *= _jumpforceMultiplier;

    }

    public void PlayerDamageMultiplier()
    {

        PlayerDamage = PlayerDamage * 2;

    }

    public void JumpButton()
    {

        if(_isDead == true)
        {

            if (IsGrounded() == true)
            {
                
                _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce); // ---> Zıpladığımızda hızımız aynı kalacak.
                StartCoroutine(ResetJumpRoutine());
                _playerAnim.Jump(true); // ---> PlayerAnimation script'indeki Jump fonksiyonunu burada çağırıyoruz. True ise zıplıyoruz.

            }

        }

    }

    public void AttackButton()
    {

        if(_isDead == true)
        {

            if (IsGrounded() == true) // ---> Attack animasyonunu çağıracağımız yer. Space'e bastığında ve IsGrounded fonksiyonu true olduğunda gerçekleşecek.
            {

                _playerAnim.Attack();

            }

        }

    }

    private IEnumerator FlashRed()
    {
        _playerSprite.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _playerSprite.material.color = Color.white;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    private Animator _anim; // ---> Animator'a ulaşmak için atadığımız değişken.
    private Animator _swordAnimation; // ---> Sword Animasyonu için atadığımız değişken.

    void Start()
    {

        _anim = GetComponentInChildren<Animator>(); // ---> Sprite'lara ulaşmak için Animator'a GetComponentInChildren'dan ulaştık.
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>(); // ---> Player'ın altındaki Sprite'a ulaşmak için transform.GetChild(1)'i kullandık.

    }

    public void Move(float move) // ---> Player tarafından erişmemiz gerektiği için public olacak ve Hareket geçişlerini içinde barındıracak.
    {
        
        _anim.SetFloat("Move", Mathf.Abs(move)); // ---> Player'da ki fonksiyon Move olduğu için onu alıyoruz ve buradaki move'a atıyoruz. Mathf.Abs = Mutlak değer.

    }

    public void Jump(bool jumping) // ---> Bool değeri zıpladığımızı bilip bilmemesi için atadığımız değişken.
    {

        _anim.SetBool("Jumping", jumping); // ---> Animator'de atadığımız parametrenin adı Jumping.

    }

    public void Attack() // ---> Attack animasyonuna ulaşmak için yaptığımız fonksiyon.
    {

        _anim.SetTrigger("Attack"); // ---> Animator'de atadığımız parametrenin adı Attack.
        _swordAnimation.SetTrigger("SwordAnimation"); // ---> Animator'de atadığımız parametrenin adı SwordAnimation.

    }

    public void Death()
    {

        _anim.SetTrigger("Death");

    }

    public void Hit()
    {

        _anim.SetTrigger("Hit");

    }

}

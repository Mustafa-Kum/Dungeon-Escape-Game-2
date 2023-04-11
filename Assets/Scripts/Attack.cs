using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private bool _canDamage = true; // ---> Her vuruş arasında Cooldown oluşturmak için atadığımız değişken.
    
    void OnTriggerEnter2D(Collider2D other) // ---> Box Collider2D Triggerlandığında olacakları bu Fonksiyon belirleyecek. 
    {

        Debug.Log("Hit:" + other.name);

        IDamageable hit = other.GetComponent<IDamageable>(); // ---> Herhangi bir nesnede IDamageable interface'i var varsa hit'e eşitle.

        if (hit != null) // ---> hit değişkenimin null dışında bir şeye çarptıysa.
        {

            if ( _canDamage == true ) // ---> Değişken true'ya eşit olduğunda.
            {

                hit.Damage(); // ---> IDamageable Interface'indeki Damage methodunu çağır.
                _canDamage = false; // ---> Methodu çağırdıktan sonra false'a ata.
                StartCoroutine(ResetDamage()); // ---> Ne kadar zaman bekleyeceğimizi belirten methodu burada çağırıyoruz.

            }

        }

    }

    IEnumerator ResetDamage() // ---> Her vuruş arası ne kadar zaman bekleyeceğimizi belirten metod.
    {

        yield return new WaitForSeconds(0.5f);
        _canDamage = true; // ---> 0.5 Saniye geçtiği anda _canDamage'i tekrar true'ya çevir.

    }

}

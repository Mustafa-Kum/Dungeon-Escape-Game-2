using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public interface IDamageable // ---> Direkt olarak arayüz olacağı için Mono'yu ve class'ı sildik. Bu sözleşme gibidir ve her kullandığımız script'te interface özellikleri geçmelidir.
{

    int Health { get; set; } // ---> Interface olarak kullandığımız için bir değişken atarken get ve set'i kullanmak zorundayız.
    
    void Damage(); // ---> Interface'i olan tüm script'lerde bu Damage metodu olmak zorunda.

}

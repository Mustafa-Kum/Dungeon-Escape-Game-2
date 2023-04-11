using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
 
    private Spider _spider; // ---> Spider'a ulaşmak için atadığımız değişken.
    
    void Start()
    {

        _spider = transform.parent.GetComponent<Spider>(); // ---> Spider, Sprite'ın parent objesini olduğu için onun özelliklerini kullanmak için bu şekilde ulaşıyoruz.

    }

    public void Fire()
    {

        _spider.Attack(); // ---> Spider'daki Attack metodunu burada çağırıyoruz.

    }

}

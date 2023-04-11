using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDmg : MonoBehaviour
{
    
    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.CompareTag("Player")) // ---> Eğer tag'i Player ise.
        {

            Player _player = other.GetComponent<Player>(); // ---> Herhangi bir nesnede IDamageable interface'i var varsa hit'e eşitle.

            if (_player != null) // ---> Eğer ki herhangi bir şeye vurursak
            {

                _player.Damage(); // ---> IDamageable Interface'indeki Damage methodunu çağır.
                _player.Damage(); // ---> IDamageable Interface'indeki Damage methodunu çağır.
                _player.Damage(); // ---> IDamageable Interface'indeki Damage methodunu çağır.

            }

        }
    }    

}

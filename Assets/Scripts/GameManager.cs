using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance; // ---> Bu "instance" değişkeni, yalnızca sınıfın kendisi tarafından erişilebilen bir örnek olacaktır.

    public static GameManager Instance
    {

        get
        {

            if (_instance == null)
            {

                Debug.LogError("GameManager is Null");

            }

            return _instance;
            
        }

    }

    public bool HasKeyToCastle { get; set;} // ---> Oyunu bitirmek için olan key'i burada kontrol ediyoruz.
    
    private void Awake()
    {

        _instance = this;

    }

}

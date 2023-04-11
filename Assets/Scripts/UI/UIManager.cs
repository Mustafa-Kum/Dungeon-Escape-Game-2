using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ---> UI kütüphanesine ulaşmak için dahil ettiğmiz yer.
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    private static UIManager _instance; // ---> Bu "instance" değişkeni, yalnızca sınıfın kendisi tarafından erişilebilen bir örnek olacaktır.

    public static UIManager Instance
    {

        get
        {

            if (_instance == null)
            {

                Debug.LogError("UI Manager i Null");

            }

            return _instance;
            
        }

    }

    public Text playerGemCountText; // ---> Unity'den text yerini atadığımız yer.
    public Image selectionImg; // ---> Shop'da neyi seçtiğimizi gösteren değişken.
    public Text gemCountText; // ---> Healt bar'ının altındaki gem sayısını yönetmek için atadığımız değişken.
    public Image[] healthBars; // ---> Health barları tek bir değişkene atayarak Unity'de imzalayacağız.
    [SerializeField]
    private GameObject _pauseMenuPanel;
    [SerializeField]
    private Text _gameOverText, _victoryGameText; // ---> GameOver Text'in değişkeni.
    [SerializeField]
    private GameObject _gameOverMenuPanel, _victoryGameMenuPanel;
    [SerializeField]
    private GameObject _pauseButton;
    

    private void Awake()
    {

        _instance = this;

    }
    
    public void OpenShop(int gemCount) // ---> Shop'daki player'ın ne kadar gem'i olduğunu bildiğimiz ve atadığımız yer
    {

        playerGemCountText.text = "" + gemCount + "G"; // ---> gemCount bilgisini alıp Text'e yansıttığımız yer.

    }

    public void UpdateShopSelection(int yPos) // ---> Itemları seçtğimizde oluşacak durumları atadığımız method.
    {

        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos); // ---> Seçtiğimiz item için Selection olarak atadığımız görseli yer değiştirecez.

    }

    public void UpdateGemCount(int count) // ---> Healt bar'ının altındaki Gem sayısını değiştirebilmek için atadığımız method.
    {

        gemCountText.text = "" + count + "G";

    }

    public void UpdateLives(float livesRemaining) // ---> Health'e bağlı olarak yapılacak işlemler için atadığımız değişken.
    {

        for(int i = 0; i <= livesRemaining; i++) // ---> Health değerini sürekli kontrol ediyoruz. i sürekli artıyor ve Health değerine eşit olduğu zamanlar atadağımız fonksiyon ile update ediyoruz.
        {

            if (i == livesRemaining)
            {

                healthBars[i].enabled = false;

            }

        }

    }

    public void RestartGameButton()
    {

        SceneManager.LoadScene("Game");
        Time.timeScale = 1;

    }

    public void PauseButton()
    {
        
        Time.timeScale = 0;
        _pauseMenuPanel.SetActive(true);
    
    }

    public void PauseButtonOnDeath()
    {

        _pauseButton.SetActive(false);

    }

    public void ResumeGame()
    {
        
        Time.timeScale = 1;
        _pauseMenuPanel.SetActive(false);
    
    }

    public void QuitButton()
    {

        Application.Quit();

    }
    
    public void MainMenuButton()
    {

        SceneManager.LoadScene("Main_Menu");

    }

    public void GameOverSequence()
    {
        
        StartCoroutine(ShowGameOverTextAfterDelay());
        StartCoroutine(GameOverFlickerRoutine());

    }

    public void VictorySequence()
    {
        
        StartCoroutine(ShowVictoryTextAfterDelay());
        StartCoroutine(VictoryFlickerRoutine());

    }
    
    public IEnumerator GameOverFlickerRoutine() // ---> Öldükten sonraki GameOver yazısını loop'a sokacaz.
    {
        
        while(true)
        {
            
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        
        }
    
    }

    public IEnumerator ShowGameOverTextAfterDelay() // ---> Game Over yazısı 2 saniye sonra gelecek.
    {
        
        yield return new WaitForSeconds(2f);
        _gameOverText.gameObject.SetActive(true);
        _gameOverMenuPanel.SetActive(true);
    
    }

    public IEnumerator VictoryFlickerRoutine() // ---> Öldükten sonraki GameOver yazısını loop'a sokacaz.
    {
        
        while(true)
        {
            
            _victoryGameText.text = "Victory";
            yield return new WaitForSeconds(0.5f);
            _victoryGameText.text = "";
            yield return new WaitForSeconds(0.5f);
        
        }
    
    }

    public IEnumerator ShowVictoryTextAfterDelay() // ---> Game Over yazısı 2 saniye sonra gelecek.
    {
        
        yield return new WaitForSeconds(2f);
        _victoryGameText.gameObject.SetActive(true);
        _victoryGameMenuPanel.SetActive(true);
    
    }

}

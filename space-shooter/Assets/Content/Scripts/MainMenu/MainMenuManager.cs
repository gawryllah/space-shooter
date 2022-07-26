using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public Image logo;
    Color logoColor;

    public GameObject menu;
    public GameObject newGameView;

    public GameObject newGameViewButtons;
    public GameObject bossModeSpec;

    public GameObject howToPlayView;
    public GameObject instructionView;

    public Button bgMusicToggle;

    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(true);
        newGameView.SetActive(false);
        bossModeSpec.SetActive(false);
        howToPlayView.SetActive(false);
        instructionView.SetActive(false);

        logoColor = logo.color;
        StartCoroutine(logoAnim());

        if (PlayerPrefs.HasKey("SS-volume"))
            volumeSlider.value = PlayerPrefs.GetFloat("SS-volume");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void RestHiScore()
    {
        PlayerPrefs.SetInt("SS-score", 0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowNewGameView()
    {
        menu.SetActive(false);
        newGameView.SetActive(true);
    }

    public void GoToHowToPlay()
    {
        menu.SetActive(false);
        howToPlayView.SetActive(true);
    }

    public void GoToInstruction()
    {
        howToPlayView.SetActive(false);
        instructionView.SetActive(true);
    }

    public void GoBackNewGameToMenu()
    {
        menu.SetActive(true);
        newGameView.SetActive(false);
    }

    public void GoBackBossSpecToNewGame()
    {
        bossModeSpec.SetActive(false);
        newGameViewButtons.SetActive(true);
    }

    public void GoBackToHowToPlay()
    {
        howToPlayView.SetActive(true);
        instructionView.SetActive(false);
    }

    public void GoBackHTPtoMainMenu()
    {
        howToPlayView.SetActive(false);
        menu.SetActive(true);
    }

    public void ToggleBgMusic()
    {
        SoundManager.instance.AudioSourceToggle();
        bgMusicToggle.GetComponentInChildren<TMP_Text>().text = SoundManager.instance.audioSource.enabled ? "BG Music: ON" : "BG Music: OFF";
    }



    public void BossMode()
    {

        PlayerPrefs.SetString("SS-Boss", "true");
        PlayerPrefs.SetString("SS-Arcade", "false");

        bossModeSpec.SetActive(true);
        newGameViewButtons.SetActive(false);
    }

    public void ArcadeMode()
    {
        PlayerPrefs.SetString("SS-Boss", "false");
        PlayerPrefs.SetString("SS-Arcade", "true");
        StartGame();
    }

    public void SetTillBossPoints(string points)
    {
        //Debug.Log(points.ToString());
        PlayerPrefs.SetInt("SS-TBP", int.Parse(points.ToString()));
    }


    private IEnumerator logoAnim()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            logo.color = Color.clear;
            yield return new WaitForSecondsRealtime(0.8f);
            logo.color = logoColor;
        }

    }

    public void ChangeVolume()
    {
        SoundManager.instance.ChangeVolume(volumeSlider.value);

        PlayerPrefs.SetFloat("SS-volume", volumeSlider.value);
    }
}

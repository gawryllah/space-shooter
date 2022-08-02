using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public Image logo;
    Color logoColor;
    // Start is called before the first frame update
    void Start()
    {
        logoColor = logo.color;
        StartCoroutine(logoAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void RestHiScore()
    {
        PlayerPrefs.SetInt("SS-score", 0);
    }

    public void ExitGame()
    {
        Application.Quit();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayBeginner()          //beginner = 1-10 indexleri (tutorial da yapılacak sonra))..
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        //UnityEditor.EditorApplication.isPlaying = false;            //NOT: SADECE EDİTÖRDEYKEN BUNU KULLAN

        Application.Quit();                                       //BU EDİTÖRDE ÇALIŞMAZ FAKAT GERÇEK OYUNU OYNADIĞIN ZAMAN OYUNU KAPATIR.
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayChapter2()
    {
        SceneManager.LoadScene(11);
    }
}

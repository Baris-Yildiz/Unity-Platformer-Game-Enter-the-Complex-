using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public Text text;
    /*
    IEnumerator FadeOutColor(Color color)                           
    {
        while (color.a > 0)                                          
        {
            color.a -= 0.2f;

            yield return new WaitForSecondsRealtime(0.1f);          
        }
    }
    */
    private LevelChanger levelChanger;

    IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(1f);

        Counters.IncreaseSeconds();

        text.text = (Counters.hours_str + " : " + Counters.minutes_str + " : " + Counters.seconds_str);

        StartCoroutine(Timer());
    }

    void Start()
    {
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();

        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        switch (gameObject.name)
        {
            case "Text":
                text.text = "LEVEL " + (levelIndex).ToString();
                break;

            case "Deaths":
                text.text += Counters.deathCount.ToString();
                break;

            case "Timer":

                text.text = (Counters.hours_str + " : " + Counters.minutes_str + " : " + Counters.seconds_str);                                       //Counters.hours.ToString,Counters.minutes.ToString(),Counters.seconds.ToString()

                StartCoroutine(Timer());

                break;

            case "Your Time":

                text.text += (Counters.hours_str + " : " + Counters.minutes_str + " : " + Counters.seconds_str);

                break;

        }

    }
}

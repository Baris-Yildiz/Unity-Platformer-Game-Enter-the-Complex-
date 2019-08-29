using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private Animator LevelAnimator;

    private Animator CharAnimator;

    private GameObject star2;

    private GameObject star3;

    private GameObject yourtime;

    private GameObject deaths1;

    IEnumerator WaitAndDelete(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);

        Destroy(GameObject.Find("Image"));
    }

    private void Start()
    {

        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        if (GameObject.Find("CharacterController"))
        {
            CharAnimator = GameObject.Find("CharacterController").GetComponent<Animator>();         //küçültme/büyütme hallerindeyken renk değişimi için
        }


        LevelAnimator = GameObject.Find("LevelChanger").GetComponent<Animator>();

        if (levelIndex == 10)
        {
            star2 = GameObject.Find("Star2");

            star3 = GameObject.Find("Star3");

            yourtime = GameObject.Find("Your Time");

            StartCoroutine(WaitAndDelete(1.5f));           //summary bölümünde karartma efekti olarak kullandığımız image ı yok etmezsek butona basamayacağız.

            if(Counters.deathCount >= 50)
            {
                Destroy(star3);
            }

            if (Counters.minutes >= 10)
            {
                Destroy(star2);
            }
        }
    }

    public void FadeToLevel()
    {
        LevelAnimator.SetTrigger("FadeOut");
    }

    public void Shrink()
    {
        CharAnimator.SetTrigger("CharacterShrink");
    }

    public void Normalize()
    {
        CharAnimator.ResetTrigger("CharacterShrink");
        CharAnimator.SetTrigger("CharacterNormalize");
    }
}

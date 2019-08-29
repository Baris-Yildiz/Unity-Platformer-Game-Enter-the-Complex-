using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    new private AudioSource audio;

    void Awake()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        DontDestroyOnLoad(gameObject);

        audio = gameObject.GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        if (Counters.deathCount == 0)           //ilk bölümde öldükten sonra müzik oynatılmasın diye, bu yüzden bu şartı koy.
        {
            audio.Play();
        }
    }



}

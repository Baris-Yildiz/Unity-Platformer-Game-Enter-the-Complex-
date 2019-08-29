using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedButton_Script : MonoBehaviour
{
    /*Kırmızı tuşa collider ekledik, eğer karaktere kırmızı tuş collider'ı ekleseydik her bölümde kırmızı tuş ve kapı olmayacağı için null referance error verirdi
    Fakat kırmızı tuşa bunu verirsek, kırmızı tuşun bulunduğu her bölümde kırmızı bir kapı da bulunacağı için null referance vermezdi yani objeyi her zaman bulur.*/

    //public SpriteHandler spriteHandler;

    new private AudioSource audio;

    IEnumerator FadeOutColor(string str , Color color)                           //fade out effekti
    {
        while (color.a > 0)                                          //rengin saydamlığı 0 dan büyükse yani opaksa 0.1 saniyede bir opaklığını 0.1f değerinde düşür.
        {
            color.a -= 0.2f;

            yield return new WaitForSecondsRealtime(0.1f);          //1 saniye sonunda tamamen kaybolur.(saydam olur)

            redDoor.spriteRenderer.color = color;                               //program rengi göremediği için buradan ayarla şimdilik

        }

        Destroy(GameObject.Find(str), 0.1f);              //red door u yok eder. ikinci argüman kaç saniyede yok edeceğini belirtir (bu durumda 0,1 saniyede yok edecektir. süreli bölüm konseptleri için belki işe yarayabilir).

    }

    IEnumerator FadeInColor(string str, Color color)                           
    {
        while (color.a < 1)                                          
        {
            color.a += 0.2f;

            yield return new WaitForSecondsRealtime(0.1f);          

            redDoor_Spawn.spriteRenderer.color = color;                               //mal program rengi göremediği için buradan ayarla şimdilik

        }

        redDoor_Spawn.boxCollider2D.enabled = true;                 //renk geldikten sonra collideri aç ki mermiler ve diğer objeler çarpabilsin.
                    
    }

    private int PressCount;

    public Char_Movement character;

    public RedDoor_Script redDoor;

    public RedDoorSpawn redDoor_Spawn;

    public Color redDoor_Alpha = new Color(255, 255, 255, 1);            //blue ve red door spritelarının renklerini ayarlama işlemi. Bu renk sprite renginin altında kaldığı için önemsiz fakat son argümanı (yani alpha) sprite opaklığını etkiliyor.

    public Color redDoorSpawn_Alpha = new Color(255, 255, 255, 0);

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();

        if (GameObject.Find("RedDoor_Spawn"))           //her bölümde açılan bir kapı olmayabilir!
        {
            redDoor_Spawn = GameObject.Find("RedDoor_Spawn").GetComponent<RedDoorSpawn>();

            redDoor_Spawn.spriteRenderer.color = redDoorSpawn_Alpha;
        }
        
        character = GameObject.Find("Character").GetComponent<Char_Movement>();

        if (GameObject.Find("RedDoor"))
        {
            redDoor = GameObject.Find("RedDoor").GetComponent<RedDoor_Script>();        //BAŞKA BİR SCRİPT KULLANIRKEN BUNU YAZ YOKSA BAZEN ALGILAMIYOR (NULL REFERANCE )

            redDoor.spriteRenderer.color = redDoor_Alpha;
        }


        //spriteHandler = GameObject.Find("Character").GetComponent<SpriteHandler>();

        

    }


    private void OnTriggerEnter2D(Collider2D collider)      //argüman olan collider = red button un collide olduğu her şeyi belirtir.
    {                                                       //TRİGGERLARLA İŞLEM YAPARKEN RİGİDBODY2D Yİ UNUTMA. HAREKET ETMESİNİ İSTEMİYORSAN DİNAMİK YAP + X VE Y POZİSYONUNU DONDUR. STATİC YAPARSAN ÇALIŞMIYOR.

        
        
        if(collider.tag == "Character")            
        {
            character.grounded = true;                          //üstüne zıplayınca saçma sapan titremesin diye

            PressCount += 1;
            
            if (PressCount == 1)                                //bu komutun sadece 1 kere çalışması gerek o yüzdeb presscount ayarladık.
            {
                if (GameObject.Find("RedDoor"))
                {
                    StartCoroutine(FadeOutColor("RedDoor", redDoor_Alpha));
                }
                

                if (GameObject.Find("RedDoor_Spawn"))       //sadece 1 tane red door spawn olmalı.
                {
                    StartCoroutine(FadeInColor("RedDoor_Spawn", redDoorSpawn_Alpha));
                }

                audio.Play();
            }
        }
    }
}

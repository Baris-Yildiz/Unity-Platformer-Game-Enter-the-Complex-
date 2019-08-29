using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueButton_Script : MonoBehaviour
{
    new private AudioSource audio;

    private int BluePressCount;

    public Char_Movement character;

    public BlueDoor_Script blueDoor;

    public Color blueDoor_Alpha = new Color(255, 255, 255, 1);            //blue door spritelarının renklerini ayarlama işlemi. Bu renk sprite renginin altında kaldığı için önemsiz fakat son argümanı (yani alpha) sprite opaklığını etkiliyor.


    IEnumerator FadeOutColor(string str, Color color)                           //fade out effekti
    {
        while (color.a > 0)                                          //rengin saydamlığı 0 dan büyükse yani opaksa 0.1 saniyede bir opaklığını 0.1f değerinde düşür.
        {
            color.a -= 0.2f;

            yield return new WaitForSecondsRealtime(0.1f);          //1 saniye sonunda tamamen kaybolur.(saydam olur)

            blueDoor.spriteRenderer.color = color;                   //program rengi göremediği için buradan ayarla şimdilik

        }

        Destroy(GameObject.Find(str), 0.1f);              

    }

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();

        character = GameObject.Find("Character").GetComponent<Char_Movement>();

        if (GameObject.Find("BlueDoor"))
        {
            blueDoor = GameObject.Find("BlueDoor").GetComponent<BlueDoor_Script>();         //"BlueDoor objesini bul, scriptini al." demektir.

            blueDoor.spriteRenderer.color = blueDoor_Alpha;                                 //sprite renklerine ekledik. alphayı -> blueDoor.spriteRenderer.color.a şeklinde ayarlayamadığımızdan başka bir renge ayarladık.
        }
                              
    }

    private void OnTriggerEnter2D(Collider2D collider)      //argüman olan collider = blue button un collide olduğu her şeyi belirtir.
    {                                                       //TRİGGERLARLA İŞLEM YAPARKEN RİGİDBODY2D Yİ UNUTMA. HAREKET ETMESİNİ İSTEMİYORSAN DİNAMİK YAP + X VE Y POZİSYONUNU DONDUR. STATİC YAPARSAN ÇALIŞMIYOR.



        if (collider.tag == "Character")
        {
            character.grounded = true;            //üstünde zıplayınca saçma sapan titremesin diye

            BluePressCount += 1;

            if (BluePressCount == 1)                                //bu komutun sadece 1 kere çalışması gerek o yüzdeb presscount ayarladık.
            {
                if (GameObject.Find("BlueDoor"))
                {
                    StartCoroutine(FadeOutColor("BlueDoor", blueDoor_Alpha));
                }

                audio.Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform SpawnTarget;                   //portalın atacağı konum.

    private GameObject Character;                //ana karakter.

    private CircleCollider2D OtherPortalsCollider;

    new private AudioSource audio;

    //SPAWN POİNTİN/TARGETİN YÖNÜNÜ KENDİN AYARLA EDİTÖRDE..

    IEnumerator DisablePortal()
    {
        OtherPortalsCollider.enabled = false;

        audio.Play();

        yield return new WaitForSecondsRealtime(2);

        OtherPortalsCollider.enabled = true;
    }
    
    private void SetSpawnAndColliderTo(string str)
    {
        SpawnTarget = GameObject.Find(str).transform.GetChild(0).GetComponent<Transform>();  //child lara ve component'larına ulaşmak için .

        OtherPortalsCollider = GameObject.Find(str).GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();

        Character = GameObject.Find("Character");           //bu yapılmazsa karakterin pozisyonu yenilenmez.

        if(gameObject.name == "Portal")         //Bu scriptin bağlı oldugu obje => gameObject (GameObject sınıfıyla karıştırma!)
        {
            if (GameObject.Find("Portal2"))
            {
                SetSpawnAndColliderTo("Portal2");
            }
        }

        if (gameObject.name == "Portal2")           
        {
            if (GameObject.Find("Portal"))
            {
                SetSpawnAndColliderTo("Portal");
            }
        }

        //AÇIKLAMA: PORTAL PREFABLERİ VAR, VE BUNLARIN SPAWNPOİNT ADINDA CHİLD LARI VAR. BU CHİLD'LARIN ÖNEMİ KARAKTERİN PORTALIN TAM İÇİNDE CANLANIP SONSUZA KADAR BİR PORTALDAN DİĞERİNE GEÇMESİNİ ÖNLEMEKTİR. YANİ BU CHİLD' LAR PORTALIN COLLİDER'INDAN BİRAZ DAHA UZAKTADIR.
    }   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            Character.transform.position = SpawnTarget.position;        //karakterle collide olduğu an spawntargeti karakterin konumu olarak belirle.

            StartCoroutine(DisablePortal());             //ışınlandıktan sonra 2 saniye boyunca tekrar ışınlanamaz.
        }
    }
}

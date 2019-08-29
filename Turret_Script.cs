using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;               //IENUMARETOR İÇİN GEREKLİ UNİTYENGİNE.UI

public class Turret_Script : MonoBehaviour              
{
    IEnumerator BurstFire()
    {
        yield return new WaitForSecondsRealtime(1);

        for(int i = 0; i < 3; i++)                  //1 sn bekle, ateş , 0.2sn bekle , ateş , 0.2sn bekle , ateş , 0.2sn bekle.
        {
            Shoot();

            audio.Play();

            yield return new WaitForSecondsRealtime(0.2f);
        }

        StartCoroutine(BurstFire());
    }

    IEnumerator ShootSpeed(float seconds)     //IEnumerator = bekleme zamanları, saniye vs . ile çalışmakta kullanılan bir değişken çeşidi. "Sayı üretme" anlamında "(numerator)"
    {

        yield return new WaitForSecondsRealtime(seconds);             //seconds = kaç saniyede bir ateş edeceği 

        if(gameObject.name == "Old Turret")
        {
            FirePoint.Rotate(fp_pos);           //orijinal transformu, verilen vektördeki değerler kadar döndürür. örn. verilen(0,10,-50) ise x, 0 derece ; y, 10 derece; z, -50 derece döner.

            Shoot();

            FirePoint.Rotate(Fp_pos_Reversed);          //rotasyon durumunu resetlemek için (0,0,0) a geri dödürmek için.
        }
        else
        {
            Shoot();
        }

        audio.Play();

        StartCoroutine(ShootSpeed(seconds));    //Kendini tekrarlaması için yeniden aynı metotu çağırdık. DİKKAT : STARTCOROUTİNE KULLANILMALI!!! (ienumaretor'lerde)
    }

    void Shoot()
    {
        Instantiate(bullet, FirePoint.position, FirePoint.rotation);        //Shoot() çağırılınca mermiyi firepointte spawnlayacak. Mermi spawnlanınca mermi scripti de otomatik olarak çağırılacak.
                                                                            //FirePoint.rotation da spawnladığı için hep ileri gider, yönünü şaşırmaz.
    }

    new private AudioSource audio;

    public GameObject bullet;                           // mermi prefabı

    public Transform FirePoint;                     //Prefabın içinde tanımlanmış bir firepoint transformu. mermi burada belirecek.

    private Vector3 fp_pos = new Vector3(0, 0, -50);

    private Vector3 Fp_pos_Reversed = new Vector3(0, 0, 50);

    // Bullet a Rigidbody ve COllider ekle!
    //Rigidbody de gravity scale = 0 yap;

    void Start()                //NOT : Burst fire ve sniper taretlerinden birden fazla spawnlarsan isimlerine (1) eklenir. bu eklentileri silmelisin yoksa hangi taret olduğunu anlamaz.
    {
        audio = gameObject.GetComponent<AudioSource>();

        if (gameObject.name == "BurstFireTurret")
        {
            StartCoroutine(BurstFire());
        }

        else if(gameObject.name == "Old Turret")
        {
            StartCoroutine(ShootSpeed(0.4f));               //0.1f eğlenceli :)
        }

        else if(gameObject.name == "SniperTurret")
        {
            StartCoroutine(ShootSpeed(2f));
        }
        else
        {
            StartCoroutine(ShootSpeed(1));
        }
    }

    private void Update()
    {
        if(gameObject.name == "Old Turret")
        {
            float Rand_Rotation = Random.Range(-40, 40);

            fp_pos.z = Rand_Rotation;

            Fp_pos_Reversed.z = -Rand_Rotation;
        }

    }






}

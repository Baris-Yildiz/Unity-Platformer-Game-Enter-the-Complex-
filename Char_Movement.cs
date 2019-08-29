using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Char_Movement : MonoBehaviour
{

    IEnumerator WaitAndExitLevel(int x)
    {
        yield return new WaitForSeconds(x);             

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);          //yeni bölümü yükle
    }

    private string RightKey = "d";

    private string LeftKey = "a";

    private LevelChanger levelChanger;                      //fadeout ve fadein için animasyonalra ulaşmak.

    private Vector2 SmallColliderSize, NormalColliderSize;

    private SpriteHandler spriteHandlerScript;

    public SpriteRenderer spriteRenderer;

    public BoxCollider2D boxCollider;               //karakter büyük boydayken kullanılacak collider.

    public Vector3 velocity;                //KARAKTERİN NE KADAR HAREKET EDECEĞİ

    private float speed;                     //HIZ, GİDECEĞİ KONUM.

    public float speedValue = 15;

    public float walkAcceleration;          //İVME , ivme = hız/zaman dır, hız sabit olmak koşuluyla ivmeyi arttırmak ; zamanı azaltmak demektir. Zamanı azaltmak da zemin kayganlık hissini azaltıyor.

    public float groundDeceleration;       //DURMA HIZI, artarsa daha çabuk duracaktır, azaltılırsa daha uzun zamanda duracaktır yani kayganlık hissi verecektir.

    public Sprite Left; 

    public Sprite Right;

    public bool grounded;                  //yerde olunup olunmadığını anlamak için bir bool

    public float jumpHeight;                //zıplama yüksekliği

    //public PlatformScript platformScript;               // BAŞKA BİR SCRİPTİ KULLANMA, BU SCRİPTTEKİ ELEMANLARI KULLANMAK İÇİN ScriptAdı.Eleman . Classlar gibi düşün.

    //public BoxCollider2D plat_collider;

    public Color char_Alpha = new Color(255, 255, 255, 1);

    void Start()
    {

        spriteHandlerScript = gameObject.GetComponent<SpriteHandler>();     /*gameobject = character , 
                                                                                getcomponent<SpriteHandler> = characterdeki spritehandler scriptini arar.
                                                                                Sonrasi spritehandler scriptindeki metotlari cagirmakta kullanilacaktir.
                                                                                Normalde 1 metot icin baska bir scripte gerek olmaz fakat pratik olsun diye bunu yaptim.. */
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;          //Bölüm numarasını alır.

        spriteRenderer.color = char_Alpha;

        NormalColliderSize = new Vector2(0.7302246f, 1.43884f);

        SmallColliderSize = new Vector2(0.4545455f, 0.9090909f);

        speed = speedValue;
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Counters.deathCount = 0;
            Counters.minutes = 0;
            Counters.hours = 0;
            Counters.seconds = 0;
            SceneManager.LoadScene(0);
        }

        float gravity = Physics2D.gravity.y;

        spriteRenderer.color = char_Alpha;

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;          //Bölüm numarasını alır.

        //======================================================= SAĞA SOLA YÜRÜMEK =============================================================


        float moveInput = Input.GetAxisRaw("Horizontal");      //horizontal = yatay demek, d ye basılınca 1 a ya basılınca -1 verir. x i belirtir. -1 sol 1 sağdır.

        velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);   /* 3 argüman var, bunlar sırasıyla (ilk konum,son konum,ivme) dir.
                                                                                                              velocity.x ilk durumda 0 dır, son konumu yani geleceği konum hız*yön dür.(Buradaki hız aslında 'kaç birim yürüyeceği'dir.)
                                                                                                              ivme ise bildiğin gibi, arttırırsan daha hızlı gider azaltırsan daha yavaş.
                                                                                                              */

        transform.Translate(velocity * Time.deltaTime);       //karakteri yeni konumuna yerleştirir..

        //AŞAĞIDAKİ KOD KARAKTERİN KAYMASINI ÖNLEYECEKTİR..

        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);   //Sağ veya Sol tuşlarına(d veya a & sağ ok veya sol ok) basılınca karakter yürür..
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);   /*else yani sağ veya sol tuşlarına basılmadığında karakterin hareketi:
                                                                                                    0 a doğru yürümek olur başka bir deyişle hızını 0 a düşürür,durur.
                                                                                                    ve bunu groundDeceleration ivmesiyle yapar. 
                                                                                                    */
        }

        //burası



        //========================================================================================================================================

        //===================================================  COLLİDER İÇİN KODLAR  ==============================================================

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, transform.rotation.z); /*burada karakter ile 'collide' olan nesneleri toplayan bir liste yaptık
                                                                                                üç argüman var sırasıyla point,size,angle
                                                                                                point = collider veya hitbox'un ortası
                                                                                                size = collider boyutu, bizimkisi karakteri saran bir boxCollider componenti olacaktır.
                                                                                                angle açıdır, colliderı döndürmek için kullanılabilir.
                                                                                                SİZE,POİNT VE ANGLE İLE BİR COLLİDER OLUŞTURULUR, OverlapBoxAll İSE BU COLLİDER A ÇARPAN BAŞKA COLLİDERLARI YAZDIRIR.. BURASI ÇOKOMELLİ..:)
                                                                                                bu listenin ilk elemanları => ground , character(boxcollider) dir. başka bir cisme çarpınca yenisi eklenir.
                                                                                              */
        
        grounded = false;

        foreach (Collider2D hit in hits)                          //listedeki elemanları tarıyor...
        {

            

            if (hit == boxCollider)
                                                          /*buradaki boxColllider üstte tanımladığımız boxcollider2d dir. Yani karakterin kendi collideridir. 
                                                            Karakter aslinda her zaman kendiyle "collide" oluyor, bunu engellemek için bunu yazdık.*/
                continue;

            switch (hit.tag)
            {
                case "Platform":
                    grounded = true;
                    break;

                case "Ground":
                    grounded = true;
                    break;

                case "Obstacle":

                    Counters.IncreaseDeaths();

                    SceneManager.LoadScene(sceneIndex);             //Ediörün en üstünde File > Build Settings 'den bölüm indexini görebilirsin ve yeni scene leri oraya sürükleyerek ekleyebilirsin .Örn. Level01 in indexi 0 dır. Engele çarpılınca yine Level01 i oynatacak(baştan başlatacak)
                                                                        
                    break;

                case "RedPortal":

                    gameObject.tag = "Immune";

                    levelChanger.FadeToLevel();                     //portala girince ekranı karartır.

                    StartCoroutine(WaitAndExitLevel(2));     
                    
                    //SceneManager.LoadScene(sceneIndex+1);           //Portala girince index 1 artacak. Level01 indexi = 0 , Level02 ninki 1 dir. Level01 deyken portala girince index 1 artacak ve Level02 oynayacak.

                    break;

                case "RedButton":
                    grounded = true;
                    break;

                case "BlueButton":
                    grounded = true;
                    break;

                case "ShrinkPortal":

                    //levelChanger.Shrink();              //karakterin rengini değiştirecek animasyonu oynatır.

                    speed = speedValue +5;

                    Left = spriteHandlerScript.SmallLeft;      //Left ve right spritelarını küçük boyutlarıyla değiştir.

                    Right = spriteHandlerScript.SmallRight;

                    boxCollider.size = SmallColliderSize;

                    jumpHeight = 2;

                    continue;               //collider ile çarpışmamasını, colliderin sadece bir şeyleri aktive etmesini vs. istiyorsan continue de. Bu durumda karakter küçültme portalına değince çarpmayacak, yoluna devam edebilecek..

                case "Normalizer":

                    //levelChanger.Normalize();           //karakterin rengini değiştirecek animasyonu oynatır.

                    speed = speedValue;

                    Left = spriteHandlerScript.NormalLeft;      

                    Right = spriteHandlerScript.NormalRight;

                    boxCollider.size = NormalColliderSize;

                    jumpHeight = 3;

                    continue;               //collider ile çarpışmamasını, colliderin sadece bir şeyleri aktive etmesini vs. istiyorsan continue de. Bu durumda karakter küçültme portalına değince çarpmayacak, yoluna devam edebilecek..

                case "JumpPad":
                    velocity.y = Mathf.Sqrt(Mathf.Abs(gravity) * 4 * jumpHeight);
                    grounded = false;
                    break;
            }                                                       
  

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);        //distance = çarpılan collider (hit) ile argüman olarak verilen collider (boxCollider yani karakterin collideri) arasındaki mesafeyi belirtir.

            if (colliderDistance.isOverlapped)               //isOverlapped = True ise colliderin içine girmiş (bir nevi noclip olmuş) demektir.
            {

                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);  //karakteri colliderin gerisine atıyor.
                                                                                                                                                                                          
            }
        }




        //======================================================================================================================


        //==================================================== ZIPLAMA =========================================================


        if (grounded == true) {

            velocity.y = 0;                                        //Karakter bir platformun üzerindeyken y sini sıfırla. Bu olmasaydı 250.satırdaki kod, karakteri aşağı doğru hareket ettirecekti.
            
            if (Input.GetMouseButtonDown(0))                      //Input.GetMouseButton = basılı tutunca ,, Input.GetMouseButtonDown = basınca ,, Jump = Space.
            {
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(gravity));          /*Sol tık a basılınca karakter y sini arttır. Aşağı geri düşmesini bir alt satırda yapıyoruz.
                                                                                                       Mathf.Abs(Pysics2D.gravity.y) kullanılmasaydı karakterin y si azalırdı dolayısıyla zıplayamazdı.(Bu yüzden gravity.y yi pozitif yapmalıyız.) gravity = physics2.gravity.y
                                                                                                       Çünkü gravity.y negatif olabilirdi. Yerdeyken abs. (mutlak değer) kullanılmasa hata veriyor.
                                                                                                       */
                
                
            }
        }

        velocity.y += gravity * Time.deltaTime;                //bu satırda zıpladıktan sonra karaktere gravity uygulanılır ve karakter aşağı iner. Karakter yavaş zıplıyorsa gravity yi değiştir.
                                                                           //Physics2D.gravity default olarak (0,-25) e eşittir. Edit > Project Settings > Physics 2D den değiştirilebilir.
    }

    private void FixedUpdate()              //fixedupdate de spriteları değiştirmek karakterde oluşan "titremeyi" giderir. sorun çıkarırsa kod bloğunu update deki yerine geri al
    {
        //==========SAĞA SOLA BAKMA SPRİTE INI AYARLAMAK===========//
        if (Input.GetKey(LeftKey) || Input.GetKey(KeyCode.LeftArrow))
        {
            spriteHandlerScript.ChangeSpriteTo(Left);
        }


        if (Input.GetKey(RightKey) || Input.GetKey(KeyCode.RightArrow))
        {
            spriteHandlerScript.ChangeSpriteTo(Right);
        }
    }





}

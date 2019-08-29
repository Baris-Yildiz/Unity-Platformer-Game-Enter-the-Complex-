using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Collision : MonoBehaviour
{
    public BoxCollider2D boxCollider;


    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0); /*burada karakter ile 'collide' olan nesneleri toplayan bir liste yaptık
                                                                                                üç argüman var sırasıyla point,size,angle
                                                                                                point = collider veya hitbox'un ortası
                                                                                                size = collider boyutu, bizimkisi karakteri saran bir boxCollider componenti olacaktır.
                                                                                                angle açıdır, colliderı döndürmek için kullanılabilir.
                                                                                                SİZE,POİNT VE ANGLE İLE BİR COLLİDER OLUŞTURULUR, OverlapBoxAll İSE BU COLLİDER A ÇARPAN BAŞKA COLLİDERLARI YAZDIRIR.. BURASI ÇOKOMELLİ..:)
                                                                                                bu listenin ilk elemanları => ground , character(boxcollider) dir. başka bir cisme çarpınca yenisi eklenir.
                                                                                              */

        

        foreach (Collider2D hit in hits)                          //listedeki elemanları tarıyor...
        {
            
            if (hit == boxCollider)                               /*buradaki boxColllider üstte tanımladığımız boxcollider2d dir. Yani karakterin kendi collideridir. 
                                                                    Karakter aslinda her zaman kendiyle "collide" oluyor, bunu engellemek için bunu yazdık.*/
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);        //distance = çarpılan collider (hit) ile argüman olarak verilen collider (boxCollider yani karakterin collideri) arasındaki mesafeyi belirtir.

            if (colliderDistance.isOverlapped)                  //isOverlapped = True ise colliderin içine girmiş (bir nevi noclip olmuş) demektir.
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }
        }
    }



}

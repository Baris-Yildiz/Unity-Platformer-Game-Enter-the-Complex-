using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject self;

    public BoxCollider2D bullet_collider;

    private Vector3 velocity;

    public int speed = 10;

    public int acceleration = 870;

    private void FixedUpdate()
    {
        velocity.x = Mathf.MoveTowards(velocity.x, speed, acceleration);            //karakterin x ekseninde yürümesi mantığıyla aynı. velocity = yürüme miktarı

        transform.Translate(velocity * Time.deltaTime);                              //velocity her saniye değiştiği gibi, her saniye de karakter konumuna velocity ekleniyor. 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Character" && collision.tag != "Portal")                       //Karakter dışında herhangi bir collidera çarparsa mermi yok olur. Duvarların arasından geçmemesi için yapıldı.
        {                                                        //Eğer karaktere değdiğinde yok olsaydı, karakter ölmezdi. Ki bu fizik yasalarına aykırı...
            Destroy(self);
        }
        
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScipt : MonoBehaviour
{
    public Camera GameView;

    Color black = new Color(0, 0, 0,0);             //siyah rengi oluşturduk

    public int AddCount = 0;

    IEnumerator Wait(int x)
    {
        yield return new WaitForSeconds(x);             //bu metotu kullanmadım ama işe yarayabilir. 
    }

    void Start()
    {
        GameView.backgroundColor = black;                   //oyun açılınca ekranı siyah yapıyor.
    }

    
    void Update()
    {

        GameView.backgroundColor = black;

        

        if(AddCount > 150)                            //tamamen beyaz olmasını engellemek için durdurma mekanizması.
        {
            black.b += 0;
            black.r += 0;
            black.g += 0;
        }
        else
        {
            black.b += 0.0012f;                      // belirli bir addcount'a kadar r g b (red,green,blue) kodlamasındaki blue yu arttırıyor.                     
            black.r += 0.0012f;                      // red i arttır
            black.g += 0.0012f;                      // green i arttır
                                                                
            AddCount += 1;
        }

    }
}

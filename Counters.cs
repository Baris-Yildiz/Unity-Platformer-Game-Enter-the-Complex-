using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Counters                /*SCENE DEN BAĞIMSIZ BİR SCRİPTE ULAŞMAK İÇİN: 
                                                1. CLASS YERİNDEN MONO BEHAVOİR U SİL
                                                2. CLASS DA DAHİL OLMAK ÜZERE FONKSİYONLARI VE DEĞİŞKENLERİ STATİC YAP.
                                                */
{
    static public int deathCount = 0;

    static public int seconds = 0;

    static public int minutes = 0;

    static public int hours = 0;

    static public string seconds_str = "0" + seconds.ToString();

    static public string minutes_str = "0" + minutes.ToString();

    static public string hours_str = "0" + hours.ToString();

    static public int IncreaseDeaths()
    {
        deathCount += 1;

        return deathCount;
    }

    static public void IncreaseSeconds()
    {

        if(seconds == 59)
        {
            seconds = 0;

            minutes += 1;
        }

        else if(minutes == 60)
        {
            minutes = 0;

            seconds = 0;

            hours += 1;
        }

        else
        {
            seconds += 1;
        }

        if (seconds < 10)
        {
            seconds_str = "0" + seconds.ToString();
        }
        else
        {
            seconds_str = seconds.ToString();
        }

        if (minutes < 10)
        {
            minutes_str = "0" + minutes.ToString();
        }
        else
        {
            minutes_str = minutes.ToString();
        }

        if (hours < 10)
        {
            hours_str = "0" + hours.ToString();
        }
        else
        {
            hours_str = hours.ToString();
        }
    }

    
}

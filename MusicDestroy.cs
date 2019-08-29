using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDestroy : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.Find("Music"))
        {
            Destroy(GameObject.Find("Music"));
        }
    }
    
    
}

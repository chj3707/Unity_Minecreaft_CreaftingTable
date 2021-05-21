using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * º¸°üÇÔ
 */ 

public class Inventroy : Singleton_Mono<Inventroy> // ½Ì±ÛÅæ Àû¿ë
{
    public int m_SlotCount = 27; // ½½·Ô °³¼ö


    void Start()
    {
        SlotGenerator.GetInstance.SetSlot(m_SlotCount, this.transform); // ½½·Ô ¼¼ÆÃ
    }

    
    void Update()
    {
        
    }
}

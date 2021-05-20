using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 보관함
 */ 

public class Inventroy : MonoBehaviour
{
    private int m_SlotCount = 27;


    void Start()
    {
        SlotSetting.GetInstance().SetSlot(m_SlotCount, this.transform); // 슬롯 세팅

    }

    
    void Update()
    {
        
    }
}

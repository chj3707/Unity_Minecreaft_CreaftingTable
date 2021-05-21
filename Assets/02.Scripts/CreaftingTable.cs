using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 제작대
 */

public class CreaftingTable : MonoBehaviour
{
    private int m_SlotCount = 9; // 슬롯 개수

    void Start()
    {
        SlotGenerator.GetInstance().SetSlot(m_SlotCount, this.transform);
    }

    
    void Update()
    {
        
    }
}

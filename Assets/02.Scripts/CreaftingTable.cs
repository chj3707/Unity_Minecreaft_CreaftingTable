using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���۴�
 */

public class CreaftingTable : MonoBehaviour
{
    private int m_SlotCount = 9; // ���� ����

    void Start()
    {
        SlotGenerator.GetInstance().SetSlot(m_SlotCount, this.transform);
    }

    
    void Update()
    {
        
    }
}

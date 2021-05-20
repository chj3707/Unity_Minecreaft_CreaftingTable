using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ¡¶¿€¥Î
 */

public class CreaftingTable : MonoBehaviour
{
    private int m_SlotCount = 9;

    void Start()
    {
        SlotSetting.GetInstance().SetSlot(m_SlotCount, this.transform);
    }

    
    void Update()
    {
        
    }
}

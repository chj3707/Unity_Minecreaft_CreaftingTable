using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���ÿ� Ŭ����
 */

public class SlotSetting : MonoBehaviour
{
    private static SlotSetting m_Instance = null;

    public GameObject m_Slot = null; // ������ ������Ʈ(����)

    // ���� ���� �Լ�
    public void SetSlot(int p_slotcount, Transform p_transform)
    {
        for (int i = 0; i < p_slotcount; i++)
        {
            GameObject copyObj = GameObject.Instantiate(m_Slot);
            copyObj.SetActive(true);
            copyObj.name = string.Format($"SLOT_{i + 1}");
            copyObj.transform.parent = p_transform;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

    public static SlotSetting GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = FindObjectOfType<SlotSetting>();
            return m_Instance;
        }

        return m_Instance;
    }

    void Start()
    {
        m_Slot.SetActive(false);
    }

    
    void Update()
    {
        
    }
}

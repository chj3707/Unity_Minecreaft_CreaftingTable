using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/*
 * �Һ� ������ ������
 */

public class ItemGenerator : Singleton_Mono<ItemGenerator> // �̱��� ����
{
    private ConsumerItem m_ConsumerItem = null;   // �Һ� ������

    public int m_MaxItemCount = 10; // ������ ���� �� �ִ� �ִ� ����

    // ���Կ� ������ �������ִ� �Լ� (���� ��ܺ��� �����ذ��� ������)
    void SetSlotIntoItem(E_CONSUMER_ITEMS p_item)
    {
        Slot tempSlot;         // n��° ����
        Image tempImage;       // n��° ������ Image
        Text tempText;         // n��° ������ Text
        int currItemCount;     // n��° ������ ���� �����۰���(Text �Ľ�)
        int tempVal;           // n��° ���� �ִ밳�� - ������ �ִ� �����۰���(������ ����)
        int getItemCount = 15; // ��ư Ŭ������ ������ ������ ����
        E_CONSUMER_ITEMS tempConsumeItem = E_CONSUMER_ITEMS.None;

        for (int i = 0; i < Inventroy.GetInstance.m_SlotCount; i++)
        {
            tempSlot = Inventroy.GetInstance.transform.GetChild(i).GetComponent<Slot>(); // n��° ���� ���� Ȯ�ο�
            
            if (tempSlot.m_SlotState == E_SLOTSTATE.Full) // �������� ������
            {
                tempConsumeItem = GetFiledInfoToReflectionValueType<E_CONSUMER_ITEMS>(tempSlot.m_ItemInfo, tempConsumeItem); // ������ ������ ���� ����
                if (tempConsumeItem != p_item) // �ٸ� �������̸� ���� �������� �Ѿ
                {
                    continue;
                }

                tempText = tempSlot.transform.GetComponentInChildren<Text>(); // �������� �ִ� ������ ���� Text������Ʈ�� ����
                currItemCount = int.Parse(tempText.text); // ������ ���� �Ҵ�
                
                if(currItemCount >= m_MaxItemCount) // �ִ� ���� �̸� ���� �������� �Ѿ
                {
                    continue;
                }

                // �ִ� ������ �ƴ� ��
                tempVal = m_MaxItemCount - currItemCount; // �ִ밳�� ���� ������ ����
                
                if (tempVal >= getItemCount) // ������ ������ ������ ������ �������� ũ��
                {
                    currItemCount += getItemCount; // ���� ���ְ� ���� �������� �Ѿ
                    tempText.text = currItemCount.ToString(); // ���� ���� ����
                    break;
                }

                // ������ ������ ������ ������ �������� ������
                getItemCount -= tempVal;
                currItemCount += tempVal;       // ���� ���� ä����
                tempText.text = currItemCount.ToString(); // ���� ���� ����
            }

            // �������� ���� ���� �϶��� ó��
            else
            {
                if (getItemCount > 0)
                {
                    tempSlot.m_ItemInfo = m_ConsumerItem;       // ���Կ� ������ ���� �Ҵ�
                    tempSlot.m_SlotState = E_SLOTSTATE.Full;    // ������ �ִ� ���·� ����
                    tempImage = tempSlot.transform.GetChild(0).GetComponent<Image>(); // �������� ���� ������ ���� ������Ʈ Image ������Ʈ�� ����
                    tempText = tempImage.transform.GetChild(0).GetComponent<Text>(); // ������ ���� ������Ʈ(�̹���)�� ���� ������Ʈ Text������Ʈ�� ����

                    currItemCount = int.Parse(tempText.text); // ���� ������ ����
                    tempVal = m_MaxItemCount - currItemCount; // �ִ밳�� ���� ������ ����

                    // ������ ������ ������ ������ �������� ������ ä���ְ� ���� �������� �Ѿ
                    if (tempVal < getItemCount)
                    {
                        currItemCount += tempVal;       // ������ ���� ä���ֱ�
                        getItemCount -= tempVal;        // ä���� ������ŭ ����
                        tempImage.sprite = GetFiledInfoToReflectionReferenceType<Sprite>(tempSlot.m_ItemInfo, tempImage.sprite); // Sprite ����
                        tempText.text = currItemCount.ToString(); // ���� ���� ����
                        tempSlot.UpdateSlotUI(); // ���� UI ������Ʈ
                        continue;
                    }

                    tempImage.sprite = GetFiledInfoToReflectionReferenceType<Sprite>(tempSlot.m_ItemInfo, tempImage.sprite); // Sprite ����
                    tempText.text = getItemCount.ToString(); // ���� ����
                    tempSlot.UpdateSlotUI(); // ���� UI ������Ʈ
                    break;
                }
            }
        }
    }
    
    // �ؿ� �ڵ�� �ٲ�
    // ���÷��� ����ؼ� Ŭ������ ������� ����(Sprite) ��������
    //void GetFiledInfoToSprite(object p_object, ref Image p_image)
    //{
    //    // https://ansohxxn.github.io/c%20sharp/ch9-8/ ���÷��� ����
    //    // ���÷������� ��� ������ ���� (m_ItemInfo :: �ڷ��� objct)
    //    Type type = p_object.GetType();
    //    FieldInfo[] fields = type.GetFields(BindingFlags.Public |
    //                                        BindingFlags.Instance);
    //    foreach (var field in fields)
    //    {
    //        if (field.FieldType.Name == "Sprite") // �ڷ����� Sprite�� ��� ����
    //        {
    //            p_image.sprite = field.GetValue(p_object) as Sprite; // sprite �Ҵ�
    //            break;
    //        }
    //    }
    //}

    // ���÷��� ����ؼ� Ŭ���� ������� ������ �������� (���� ����) class
    public T GetFiledInfoToReflectionReferenceType<T>(object p_object, T p_dataType) where T : class
    {
        // https://ansohxxn.github.io/c%20sharp/ch9-8/ ���÷��� ����
        // ���÷������� ��� ������ ���� (m_ItemInfo :: �ڷ��� objct)
        T retValue;
        Type type = p_object.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                                            BindingFlags.Instance);
        foreach (var field in fields)
        {
            if (field.FieldType == p_dataType.GetType())  // �Ű������� ������ �ڷ����� ���� �ڷ��� ã��
            {
                retValue = field.GetValue(p_object) as T; // ����ȯ
                return retValue;
            }
        }
        return null;
    }

    // ���÷��� ����ؼ� Ŭ���� ������� ������ �������� (�� ����) enum
    public T GetFiledInfoToReflectionValueType<T>(object p_object, T p_dataType) where T : struct
    {
        // ���÷������� ��� ������ ���� (m_ItemInfo :: �ڷ��� objct)
        T retValue;
        Type type = p_object.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                                            BindingFlags.Instance);
        foreach (var field in fields)
        {
            if (field.FieldType == p_dataType.GetType())  // �Ű������� ������ �ڷ����� ���� �ڷ��� ã��
            {
                retValue = (T)field.GetValue(p_object); // ����ȯ
                return retValue;
            }
        }
        return default(T);
    }

    // ���� ��ư
    public void _On_GetWoodBtn()
    {
        if (EventManager.GetInstance.m_isDragging)
        {
            return;
        }
        m_ConsumerItem = ItemManager.GetInstance.GetItem<ConsumerItem>("Wood", E_ITEMTYPE.Consumption); // ������ ���� ������
        SetSlotIntoItem(m_ConsumerItem.m_Item); // ���Կ� ������ ����
    }

    // �� ��ư
    public void _On_GetStringBtn()
    {
        if (EventManager.GetInstance.m_isDragging)
        {
            return;
        }
        m_ConsumerItem = ItemManager.GetInstance.GetItem<ConsumerItem>("String", E_ITEMTYPE.Consumption); // ������ ���� ������
        SetSlotIntoItem(m_ConsumerItem.m_Item);
    }

    // ��� ��ư
    public void _On_GetCarrotBtn()
    {
        if (EventManager.GetInstance.m_isDragging)
        {
            return;
        }
        m_ConsumerItem = ItemManager.GetInstance.GetItem<ConsumerItem>("Carrot", E_ITEMTYPE.Consumption); // ������ ���� ������
        SetSlotIntoItem(m_ConsumerItem.m_Item); 
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

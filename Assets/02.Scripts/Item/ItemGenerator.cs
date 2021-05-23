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
    private object m_ItemInfo = null;   // ������

    public int m_MaxItemCount = 10;     // ������ ���� �� �ִ� �ִ� ����

    // ���Կ� ������ �������ִ� �Լ� (���� ��ܺ��� �����ذ��� ������)
    void SetSlotIntoItem(E_CONSUMEITEMS p_item)
    {
        Slot tempSlot;         // n��° ����
        Image tempImage;       // n��° ������ Image
        Text tempText;         // n��° ������ Text
        int currItemCount;     // n��° ������ ���� �����۰���(Text �Ľ�)
        int tempVal;           // n��° ���� �ִ밳�� - ������ �ִ� �����۰���(������ ����)
        int getItemCount = 15; // ��ư Ŭ������ ������ ������ ����
        E_CONSUMEITEMS tempItem = E_CONSUMEITEMS.None;

        for (int i = 0; i < Inventroy.GetInstance.m_SlotCount; i++)
        {
            tempSlot = Inventroy.GetInstance.transform.GetChild(i).GetComponent<Slot>(); // n��° ���� ���� Ȯ�ο�
            
            if (tempSlot.m_SlotState == E_SLOTSTATE.Full) // �������� ������
            {
                tempItem = GetFiledInfoToReflectionValueType<E_CONSUMEITEMS>(tempSlot.m_ItemInfo, tempItem); // ������ ������ ���� ����
                if (tempItem != p_item) // �ٸ� �������̸� ���� �������� �Ѿ
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
                    tempSlot.m_ItemInfo = m_ItemInfo;           // ���Կ� ������ ���� �Ҵ�
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

    // ���÷��� ����ؼ� Ŭ���� ������� ������ ������ ��������
    //public E_ITEMS GetFiledInfoToReflectionEnum(object p_object, E_ITEMS p_item)
    //{
    //    E_ITEMS retItem = E_ITEMS.None;
    //    if (p_object == null)
    //    {
    //        return retItem;
    //    }

    //    // ���÷������� ��� ������ ���� (m_ItemInfo :: �ڷ��� objct)
    //    Type type = p_object.GetType();
    //    FieldInfo[] fields = type.GetFields(BindingFlags.Public |
    //                                        BindingFlags.Instance);
    //    foreach (var field in fields)
    //    {
    //        if (field.FieldType == p_item.GetType())       // �Ű������� ������ �ڷ����� ���� �ڷ��� ã��
    //        {
    //            retItem = (E_ITEMS)field.GetValue(p_object); // ����ȯ
    //            return retItem;
    //        }
    //    }
    //    return retItem;
    //}


    // �Һ� ������ ���� ��������
    void GetConsumptionItem(string p_name, E_ITEMTYPE p_type)
    {
        if (EventManager.GetInstance.m_isDragging)
        {
            return;
        }
        m_ItemInfo = ItemManager.GetInstance.GetItem<ConsumeItem>(p_name, p_type); // ������ ���� ������

        E_CONSUMEITEMS tempEnum = E_CONSUMEITEMS.None;
        tempEnum = GetFiledInfoToReflectionValueType(m_ItemInfo, tempEnum); // object�� ����� ������ ���� ������

        SetSlotIntoItem(tempEnum); // ���Կ� ������ ����
    }

    // ���� ��ư
    public void _On_GetWoodBtn()
    {
        GetConsumptionItem("Wood", E_ITEMTYPE.Consumption);
    }

    // �� ��ư
    public void _On_GetStringBtn()
    {
        GetConsumptionItem("String", E_ITEMTYPE.Consumption);
    }

    // ��� ��ư
    public void _On_GetCarrotBtn()
    {
        GetConsumptionItem("Carrot", E_ITEMTYPE.Consumption);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

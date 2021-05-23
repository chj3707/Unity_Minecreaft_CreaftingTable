using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
 * ���۴�
 */

public class CreaftingTable : Singleton_Mono<CreaftingTable> // �̱��� ����
{
    protected CreaftingTable() { }

    private int m_SlotCount = 9; // ���� ����

    private List<object> m_ItemList = new List<object>();                // ������ ����Ʈ
    private List<object[,]> m_ItemRecipeList = new List<object[,]>();    // ������ ������

    
    private object[,] m_CreaftingTable;        // ���۴뿡 �ִ� ������ Ȯ�ο�
    
    private int m_Row = 3; 
    private int m_Col = 3; 
    
    void Start()
    {
        SlotGenerator.GetInstance.SetSlot(m_SlotCount, this.transform);

        m_CreaftingTable = new object[m_Row, m_Col]; // �޸� �Ҵ�

        SetItemRecipe(); // ������ ����
    }


    // ���, �Һ� ������ ���� ������ ����
    void SetItemRecipe()
    {
        m_ItemList.Add(ItemManager.GetInstance.GetItem<ConsumeItem>(("Stick"),E_ITEMTYPE.Consumption));           // ����� �߰�
        m_ItemList.Add(ItemManager.GetInstance.GetItem<EquipItem>(("WoodenAxe"),E_ITEMTYPE.Equipment));           // ���� ���� �߰�
        m_ItemList.Add(ItemManager.GetInstance.GetItem<EquipItem>(("FishingRod"), E_ITEMTYPE.Equipment));         // ���˴� �߰�
        m_ItemList.Add(ItemManager.GetInstance.GetItem<EquipItem>(("CarrotOnAStick"), E_ITEMTYPE.Equipment));     // ��� ���˴� �߰�

        // ������ �̸� �������� �뵵
        string tempstr = "";
        E_EQUIPITEMS tempEquipEnum = E_EQUIPITEMS.None;
        E_CONSUMEITEMS tempConsumeEnum = E_CONSUMEITEMS.None;

        for (int i=0; i<m_ItemList.Count; i++)
        {
            if (m_ItemList[i].GetType() == typeof(EquipItem)) // ���
            {
                tempstr = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(m_ItemList[i], tempEquipEnum).ToString(); // i��° ������ �̸� �������� 
            }
            if (m_ItemList[i].GetType() == typeof(ConsumeItem)) // �Һ�
            {
                tempstr = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(m_ItemList[i], tempConsumeEnum).ToString(); // i��° ������ �̸� �������� 
            }
            

            object[,] recipe = new object[m_Row,m_Col];
            // �����
            if (tempstr == "Stick") 
            {
                
                recipe[0, 0] = null;                 recipe[0, 1] = null;                      recipe[0, 2] = null;
                recipe[1, 0] = null;                 recipe[1, 1] = E_CONSUMEITEMS.Wood;       recipe[1, 2] = null;
                recipe[2, 0] = null;                 recipe[2, 1] = E_CONSUMEITEMS.Wood;       recipe[2, 2] = null;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // ��������
            if (tempstr == "WoodenAxe") 
            {
                recipe[0, 0] = E_CONSUMEITEMS.Wood;  recipe[0, 1] = E_CONSUMEITEMS.Wood;       recipe[0, 2] = null;
                recipe[1, 0] = E_CONSUMEITEMS.Wood;  recipe[1, 1] = E_CONSUMEITEMS.Stick;      recipe[1, 2] = null;
                recipe[2, 0] = null;                 recipe[2, 1] = E_CONSUMEITEMS.Stick;      recipe[2, 2] = null;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // ���˴�
            if (tempstr == "FishingRod") 
            {
                recipe[0, 0] = null;                 recipe[0, 1] = null;                      recipe[0, 2] = E_CONSUMEITEMS.Stick;
                recipe[1, 0] = null;                 recipe[1, 1] = E_CONSUMEITEMS.Stick;      recipe[1, 2] = E_CONSUMEITEMS.String;
                recipe[2, 0] = E_CONSUMEITEMS.Stick; recipe[2, 1] = null;                      recipe[2, 2] = E_CONSUMEITEMS.String;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // ��� ���˴�
            if (tempstr == "CarrotOnAStick") 
            {
                recipe[0, 0] = null;                recipe[0, 1] = null;                       recipe[0, 2] = null;
                recipe[1, 0] = null;                recipe[1, 1] = E_EQUIPITEMS.FishingRod;    recipe[1, 2] = null;
                recipe[2, 0] = null;                recipe[2, 1] = null;                       recipe[2, 2] = E_CONSUMEITEMS.Carrot;
                m_ItemRecipeList.Add(recipe);
                continue;
            }
        }  
    }

    // ������ ���� �� ���̺� �ʱ�ȭ �뵵
    public void TableReset()
    {
        // ���̺� ä�� ��
        E_EQUIPITEMS tempEquipEnum = E_EQUIPITEMS.None;
        E_CONSUMEITEMS tempConsumeEnum = E_CONSUMEITEMS.None;
        object[] tempArr = new object[transform.childCount]; // 1���� �迭�� �����ͼ� 2���� �迭�� �ű��

        for (int i = 0; i < transform.childCount; i++)
        {
            object tempObj = transform.GetChild(i).GetComponent<Slot>().m_ItemInfo; // ���۴� ���� ���� ����
            
            // null �̸� �����ϰ� continue
            if (tempObj == null)
            {
                tempArr[i] = tempObj; // ���۴� n��° ���� ������
                continue;
            }
            // ��� ������
            else if (tempObj.GetType() == typeof(EquipItem))
            {
                tempObj = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(tempObj, tempEquipEnum);   // �ش� ������ �����۰� ����
            }
            // �Һ� ������
            else
            {
                tempObj = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(tempObj, tempConsumeEnum); // �ش� ������ �����۰� ����
            }

            Text tempText = transform.GetChild(i).GetComponentInChildren<Text>(); // �ؽ�Ʈ ������ ���� �ӽú���       
            tempText.text = (int.Parse(tempText.text) - 1).ToString();            // ������ ���� -1

            // ������ �ϳ��� ���� ������ �ش� ���� �ʱ�ȭ
            if (int.Parse(tempText.text) == 0)
            {
                Image tempImage = transform.GetChild(i).GetChild(0).GetComponent<Image>();  // �̹��� ������ ���� �ӽú���

                tempObj = null;                                                             // ���� ���� �ʱ�ȭ
                tempImage.enabled = false;                                                  // �̹��� ��Ȱ��ȭ
                tempText.enabled = false;                                                   // �ؽ�Ʈ ��Ȱ��ȭ
            }

            tempArr[i] = tempObj; // ���۴� n��° ���� ������
        }

        // 1���� -> 2���� �ű��
        for (int y = 0; y < m_Row; y++)
        {
            for (int x = 0; x < m_Col; x++)
            {
                m_CreaftingTable[y, x] = tempArr[(y * m_Row) + x];
            }
        }
    }

    // ���̺� ���� ����
    public void TableInfoRenewal()
    {
        object[] tempArr = new object[transform.childCount]; // 1���� �迭�� �����ͼ� 2���� �迭�� �ű��

        // �迭 ä��� �뵵
        E_EQUIPITEMS tempEquipEnum = E_EQUIPITEMS.None; 
        E_CONSUMEITEMS tempConsumeEnum = E_CONSUMEITEMS.None;

        for (int i = 0; i < transform.childCount; i++)
        {
            object tempObj = transform.GetChild(i).GetComponent<Slot>().m_ItemInfo; // ���۴� ���� ���� ����

            // null �̸� �����ϰ� continue
            if (tempObj == null)
            {
                tempArr[i] = tempObj; // ���۴� n��° ���� ������
                continue;
            }
            // ��� ������
            else if (tempObj.GetType() == typeof(EquipItem))
            {
                tempObj = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(tempObj, tempEquipEnum);   // �ش� ������ �����۰� ����
            }
            // �Һ� ������
            else
            {
                tempObj = ItemGenerator.GetInstance.
                    GetFiledInfoToReflectionValueType(tempObj, tempConsumeEnum); // �ش� ������ �����۰� ����
            }

            tempArr[i] = tempObj; // ���۴� n��° ���� ������
        }

        // 1���� -> 2���� �ű��
        for (int y = 0; y < m_Row; y++)
        {
            for (int x = 0; x < m_Col; x++)
            {
                m_CreaftingTable[y, x] = tempArr[(y * m_Row) + x];
                //Debug.Log(m_CreaftingTable[y, x]);
            }
        }

        FinishedItem.GetInstance.FinishedCheak(); // �ϼ� üũ
    }

    // ���۴�� ����ص� ������ ��(���۴뿡 ������ �ø�������)
    public object CompareTableToRecipe() 
    {
        bool endflag = false;
        int index = 0;
        foreach (var recipe in m_ItemRecipeList)
        {
            // �ϳ��� �ٸ��� ������ ����
            for (int y = 0; y < m_Row; y++)
            {
                for (int x = 0; x < m_Col; x++)
                {
                    if (recipe[y, x] == null && m_CreaftingTable[y, x] != null) 
                    {
                        // �����Ǵ� ��ĭ�ε� ���̺� ���� �ִ°��
                        ++index;
                        endflag = true;
                        break;
                    }
                    else if(recipe[y,x] != null && m_CreaftingTable[y,x] == null)
                    {
                        // �����Ǵ� ��ĭ�� �ƴѵ� ���̺� ���� ���°��
                        ++index;
                        endflag = true;
                        break;
                    }

                    // �����ǿ� ���̺� �Ѵ� ��ĭ�� �ƴҶ� ��
                    if (recipe[y, x] != null && m_CreaftingTable[y, x] != null)
                    {
                        // �����ǿ� �ٸ��� �÷��� �Ѽ� ����
                        if (!recipe[y, x].Equals(m_CreaftingTable[y, x]))
                        {
                            ++index;
                            endflag = true;
                            break;
                        }
                    }
                }
                if (endflag)
                    break;
            }

            // �����ǿ� ��ġ
            if(!endflag)
            {
                return m_ItemList[index]; // ���� ������ ����
            }

            endflag = false; // �÷��� �ʱ�ȭ
        }
        return null;
    }

    void Update()
    {
        
    }
}

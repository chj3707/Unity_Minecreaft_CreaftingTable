using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 * ���۴�
 */

public class CreaftingTable : Singleton_Mono<CreaftingTable> // �̱��� ����
{
    protected CreaftingTable() { }

    private int m_SlotCount = 9; // ���� ����

    private List<object> m_ItemList = new List<object>();                // ������ ����Ʈ
    private List<E_ITEMS[,]> m_ItemRecipeList = new List<E_ITEMS[,]>();  // ������ ������

    
    private E_ITEMS[,] m_CreaftingTable;        // ���۴뿡 �ִ� ������ Ȯ�ο�
    
    private int m_Row = 3; 
    private int m_Col = 3; 
    
    void Start()
    {
        SlotGenerator.GetInstance.SetSlot(m_SlotCount, this.transform);

        m_CreaftingTable = new E_ITEMS[m_Row, m_Col]; // �޸� �Ҵ�

        SetItemRecipe(); // ������ ����
    }


    // ���, �Һ� ������ ���� ������ ����
    void SetItemRecipe()
    {
        m_ItemList.Add(ItemManager.GetInstance.GetItem<Item>("Stick"));              // ����� �߰�
        m_ItemList.Add(ItemManager.GetInstance.GetItem<Item>("WoodenAxe"));          // ���� ���� �߰�
        m_ItemList.Add(ItemManager.GetInstance.GetItem<Item>("FishingRod"));         // ���˴� �߰�
        m_ItemList.Add(ItemManager.GetInstance.GetItem<Item>("CarrotOnAStick"));     // ��� ���˴� �߰�

        // ������ �̸� �������� �뵵
        E_ITEMS tempItem = E_ITEMS.None;
        string tempstr = "";
        
        for (int i=0; i<m_ItemList.Count; i++)
        {
            tempstr = ItemGenerator.GetInstance.
                GetFiledInfoToReflectionString(m_ItemList[i], tempItem); // i��° ������ �̸� �������� 

            E_ITEMS[,] recipe = new E_ITEMS[m_Row,m_Col];
            // �����
            if (tempstr == "Stick") 
            {
                
                recipe[0, 0] = E_ITEMS.None;  recipe[0, 1] = E_ITEMS.None;       recipe[0, 2] = E_ITEMS.None;
                recipe[1, 0] = E_ITEMS.None;  recipe[1, 1] = E_ITEMS.Wood;       recipe[1, 2] = E_ITEMS.None;
                recipe[2, 0] = E_ITEMS.None;  recipe[2, 1] = E_ITEMS.Wood;       recipe[2, 2] = E_ITEMS.None;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // ��������
            if (tempstr == "WoodenAxe") 
            {
                recipe[0, 0] = E_ITEMS.Wood;  recipe[0, 1] = E_ITEMS.Wood;       recipe[0, 2] = E_ITEMS.None;
                recipe[1, 0] = E_ITEMS.Wood;  recipe[1, 1] = E_ITEMS.Stick;      recipe[1, 2] = E_ITEMS.None;
                recipe[2, 0] = E_ITEMS.None;  recipe[2, 1] = E_ITEMS.Stick;      recipe[2, 2] = E_ITEMS.None;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // ���˴�
            if (tempstr == "FishingRod") 
            {
                recipe[0, 0] = E_ITEMS.None;  recipe[0, 1] = E_ITEMS.None;       recipe[0, 2] = E_ITEMS.Stick;
                recipe[1, 0] = E_ITEMS.None;  recipe[1, 1] = E_ITEMS.Stick;      recipe[1, 2] = E_ITEMS.String;
                recipe[2, 0] = E_ITEMS.Stick; recipe[2, 1] = E_ITEMS.None;       recipe[2, 2] = E_ITEMS.String;
                m_ItemRecipeList.Add(recipe);
                continue;
            }

            // ��� ���˴�
            if (tempstr == "CarrotOnAStick") 
            {
                recipe[0, 0] = E_ITEMS.None;  recipe[0, 1] = E_ITEMS.None;       recipe[0, 2] = E_ITEMS.None;
                recipe[1, 0] = E_ITEMS.None;  recipe[1, 1] = E_ITEMS.FishingRod; recipe[1, 2] = E_ITEMS.None;
                recipe[2, 0] = E_ITEMS.None;  recipe[2, 1] = E_ITEMS.None;       recipe[2, 2] = E_ITEMS.Carrot;
                m_ItemRecipeList.Add(recipe);
                continue;
            }
        }  
    }

    // ���̺� ���� ����
    public void TableInfoRenewal()
    {
        E_ITEMS[] tempArr = new E_ITEMS[transform.childCount]; // 1���� �迭�� �����ͼ� 2���� �迭�� �ű��
        for (int i = 0; i < transform.childCount; i++)
        {
            E_ITEMS tempObj = transform.GetChild(i).GetComponent<Slot>().m_ItemInfo.m_Item; // ���۴� ���� ���� ����
            
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
    public Item CompareTableToRecipe()
    {
        bool endflag = false;
        int index = 0;
        foreach (var item in m_ItemRecipeList)
        {
            // �ϳ��� �ٸ��� ������ ����
            for (int y = 0; y < m_Row; y++)
            {
                for (int x = 0; x < m_Col; x++)
                {
                    if (item[y, x] != m_CreaftingTable[y, x])
                    {
                        ++index;
                        endflag = true;
                        break;
                    }
                }
                if (endflag)
                    break;
            }

            // �����ǿ� ��ġ
            if(!endflag)
            {
                return m_ItemList[index] as Item; // ���� ������ ����
            }
               
        }
        return null;
    }

    void Update()
    {
        
    }
}

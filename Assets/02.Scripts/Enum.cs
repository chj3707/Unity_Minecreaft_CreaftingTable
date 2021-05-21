using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ����
public enum E_SLOTSTATE
{
    Empty = 0, // �������� ���� ����
    Full       // �������� �ִ� ����
}

// ��� ������
public enum E_EQUIPMENT_ITEMS
{
    None = -1,

    WoodenAxe,          // ���� ����
    FishingRod,         // ���˴�
    CarrotOnAStick,     // ��� ���˴�

    Max
}

// �Һ� ������
public enum E_CONSUMER_ITEMS
{
    None = -1,

    Wood,               // ����
    Stick,              // �����
    String,             // ��
    Carrot,             // ���

    Max
}

// ������ Ÿ��
public enum E_ITEMTYPE
{
    None = -1,

    Equipment,       // ��� ������
    Consumption      // �Ҹ� ������
}

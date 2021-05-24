using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/*
 * ��ü������ ����� ���� Ŭ����
 */

public static class Core
{
    // �ؿ� �ڵ�� �ٲ�
    // ���÷��� ����ؼ� Ŭ������ ������� ����(Sprite) ��������
    //void static GetFiledInfoToSprite(object p_object, ref Image p_image)
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

    // ���÷��� ����ؼ� Ŭ���� ������� ������ �������� (���� ����)
    public static T GetFiledInfoToReflectionReferenceType<T>(object p_object, T p_dataType) where T : class
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

    // ���÷��� ����ؼ� Ŭ���� ������� ������ �������� (�� ����)
    public static T GetFiledInfoToReflectionValueType<T>(object p_object, T p_dataType) where T : struct
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
                retValue = (T)field.GetValue(p_object);   // ����ȯ
                return retValue;
            }
        }
        return default(T);
    }
}

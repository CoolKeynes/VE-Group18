using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UMA.CharacterSystem;
using UMA.PoseTools;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeCloth : MonoBehaviour
{
    public void ChangeClothes(DynamicCharacterAvatar avatar, string wardrobeItem)
    {
        // �Ƴ���ǰ���е�Wardrobe�Ҳ����ָ���Ƴ��ض���λ������
        avatar.ClearSlot("MaleShirt2");

        // ����µ�Wardrobe��
        // �����wardrobeItem������Ҫ��ɫ���ϵ��·������ƣ�ȷ��������������WardrobeRecipes���ж�Ӧ����
        //avatar.SetSlot(wardrobeItem);

        // ����UMA��ɫ��Ӧ�ø���
        avatar.BuildCharacter();
    }
}


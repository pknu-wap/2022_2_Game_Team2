using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Dictionary<int, string[]> content;

    void Awake()
    {
        content = new Dictionary<int, string[]>();
        GenerateContent();
    }

    void GenerateContent()
    {
        content.Add(0, new string[] {"���", "����� ó������?"});
        content.Add(1, new string[] {"���⼭ �������� ���� 4���� ��ƿ;���.", "�� ���� ������ ���� �˰� �־�.", "���ϴ� ������ �˰�ʹٸ� ������ ��ȭ�� �ɾ�!"});
        content.Add(2, new string[] {"� ������ ���ϴ�?"});
    }

    public string GetContent(int id, int contentNum)
    {
        if(contentNum >= content[id].Length)
        {
            return null;
        }
        else 
        {
            return content[id][contentNum];
        }
    }
}

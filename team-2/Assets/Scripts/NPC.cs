using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Dictionary<int, string[]> content;
    Dictionary<int, string[]> information_Rooms;

    void Awake()
    {
        content = new Dictionary<int, string[]>();
        information_Rooms = new Dictionary<int, string[]>();
        GenerateContent();
        GenerateInformationRooms();
    }

    void GenerateContent()
    {
        content.Add(0, new string[] {"���", "����� ó������?", "���� ��ó�� ���� ���� ������� �Ѹ��̾�."});
        content.Add(1, new string[] {"���⼭ �������� �� �濡 �����ϴ� ���� 4���� ��ƿ;���.", "���� �̷�, ������, ����ã��, ������������ �����Ǿ��־�.","�� ���� ������ ���� �˰� �־�.", "���ϴ� ������ �˰�ʹٸ� ������ ��ȭ�� �ɾ�!"});
        content.Add(2, new string[] {"� ������ ���ϴ�?"});
    }

    void GenerateInformationRooms()
    {
        information_Rooms.Add(0, new string[] {"���� ù��° ���� �̷ι��̾�.", "�̷ο��� �˼����� ������� ����ִ� �͸� �˾�.", "�׻� �ڸ� �����ϱ�ٷ�!"});
        information_Rooms.Add(1, new string[] {"���� �ι�° ���� �������̾�.", "�������� ���� ��� �����̿����� ������...", "������ ���������� ������ ���ؼ��� �������� ����ؾ� �ϳ���.", "�������� �ʰ� ������!"});
        information_Rooms.Add(2, new string[] {"������ ù��° ���� ����ã�� ���̾�.", "���� ���� ��Ҵ��� �𸣰����� ������ ���㰡�� ����̴���.", "�������ڿ� ���� Ƣ����� �𸣴� ������!"});
        information_Rooms.Add(3, new string[] {"������ �ι�° ���� ���������� ���̾�.", "������ ȹ���ϱ� ���ؼ��� ������������ �ؼ� 3�� �̰ܾ��ϳ���.", "������������ ���ų� ���ºΰ� �Ǹ� �������� �������� �𸣴� ������!"});
        information_Rooms.Add(4, new string[] {"������ �ٸ����� �Ǹ� ���� �ڿ� ���̴� ���� �����־�.", "�� ��ȿ��� ���ù����� ���簡 ��� �ִ� �� ����..", "�Ƹ� �� ���縦 �����߸��� ���⼭ ���� �� ���� ������?"});
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

    public string GetInformation(int roomId, int informationNum)
    {
        if(informationNum >= information_Rooms[roomId].Length)
        {
            return null;
        }
        else
        {
            return information_Rooms[roomId][informationNum];
        }
    }
}

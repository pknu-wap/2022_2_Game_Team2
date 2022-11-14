using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �÷��̾�
    public Player player;

    // �ý��� �Ŵ���
    public GameObject systemManager;

    // �ʵ� �� ��������Ʈ.
    public GameObject[] fields;
    public Transform[] spawnPoints;

    public GameObject[] save_Artifacts;
    public GameObject[] maze_Spawn_point;
    public GameObject[] room_Artifacts;
    public GameObject[] rooms;

    public void Field_Change(GameObject interactionOBJ)
    {
        Transform nextPos = interactionOBJ.GetComponent<Objects>().NextRoomPosition;

        systemManager.GetComponent<FadeInOut>().FadeFunc();

        StartCoroutine(tpPos(nextPos));
    }

    public void Get_Artifact(GameObject artifact)
    {
        Debug.Log("���� ���� �̸� : " + artifact.name);
        if(artifact.name == room_Artifacts[0].name)
        {
            room_Artifacts[0].SetActive(false);
            Maze_Room_Second_Phase();
        }
    }

    // �̷� 2������
    public void Maze_Room_Second_Phase()
    {
        for(int i = 0; i < maze_Spawn_point.Length; i++)
        {
            maze_Spawn_point[i].SetActive(true);
        }
    }

    // �̷� �ʱ�ȭ!
    public void Maze_Room_First_Phase()
    {
        for (int i = 0; i < maze_Spawn_point.Length; i++)
        {
            maze_Spawn_point[i].SetActive(false);
        }
        room_Artifacts[0].SetActive(true);
    }

    IEnumerator tpPos(Transform nextPos)
    {
        yield return new WaitForSeconds(2.0f);
        Vector3 spawnPos = new Vector3(nextPos.position.x, nextPos.position.y + 1, nextPos.position.z);
        player.transform.position = spawnPos;
    }
}
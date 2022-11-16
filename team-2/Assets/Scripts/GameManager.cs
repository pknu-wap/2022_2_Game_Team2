using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Dictionary<GameObject, int> roomState; // Room Clear?
    // �÷��̾�
    public Player player;

    // �ý��� �Ŵ���
    public GameObject systemManager;

    // ���� ����
    private bool isGetArtifact = false; // Room Artifact Get?
    private bool isInRoom = false;  // now player in Room? condition
    private bool isNeedArtifact = false; // if Get Room Artifact Player can Escape

    // �ʵ� �� ��������Ʈ.
    public GameObject[] fields;
    public Transform[] spawnPoints;

    public GameObject[] save_Artifacts;
    public GameObject[] maze_Spawn_point;
    public GameObject[] room_Artifacts;
    public GameObject[] rooms;
    
    // ���� �÷��̾� �������Ʈ
    public GameObject playerInRoom;

    // ���� ���� ���� ��ȣ
    public int artifactNum = -1;    // 0 -> Maze Artifact, 1 -> Jump Artifact, 2 -> treasure Artifact, 3 -> Quiz Artifact 

    void Start()
    {
        roomState = new Dictionary<GameObject, int>();
        
        for(int i = 0; i < rooms.Length; i++)
        {
            roomState.Add(rooms[i], 0);
        }

        foreach(KeyValuePair<GameObject, int> pair in roomState)
        {
            Debug.Log(pair.Key.name + "," + pair.Value);
        }
    }

    public void Field_Change(GameObject interactionOBJ)
    {
        Objects OBJcomponent = interactionOBJ.GetComponent<Objects>();
        if(!isGetArtifact && !isNeedArtifact)  {    
            Transform nextPos = OBJcomponent.NextRoomPosition;
            systemManager.GetComponent<FadeInOut>().FadeFunc();
            
            StartCoroutine(tpPos(nextPos));
        }
        else{
            Debug.Log("Can't exit");
            player.isLoading = false;
        }
        if(OBJcomponent.inRoom)
        {
            isInRoom = true;    // �Ա��ΰ��� ǥ���ϴ� bool�� ����
            isNeedArtifact = true;
        }
        else
        {
            if(artifactNum >= 0) {
                save_Artifacts[artifactNum].SetActive(true);
            }
            //save();
        }
    }

    public void Get_Artifact(GameObject artifact)
    {
        Debug.Log("���� ���� �̸� : " + artifact.name);
        if(artifact.name == room_Artifacts[0].name)
        {
            room_Artifacts[0].SetActive(false);
            Maze_Room_Second_Phase();
            roomState[rooms[0]] = 1;
            artifactNum = 0;
        }
        if(isNeedArtifact)
        {
            isNeedArtifact = false;
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
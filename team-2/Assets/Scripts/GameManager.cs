using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Data data;
    Dictionary<GameObject, int> roomState; // Room Clear?
    // �÷��̾�
    public Player player;

    // �ý��� �Ŵ���
    public GameObject systemManager;
    

    // ���� ����
    private bool isGetArtifact = false; // Room Artifact Get?
    private bool isNeedArtifact = false; // if Get Room Artifact Player can Escape
    private bool isPause = false;

    public bool getIsPause() {return isPause;}
    // �ʵ� �� ��������Ʈ.
    public Transform[] spawnPoints;

    public GameObject[] save_Artifacts;
    public GameObject[] maze_Spawn_point;
    public GameObject[] treasure_Spawn_point;
    public GameObject[] room_Artifacts;
    public GameObject[] rooms;
    public GameObject[] treasuresBox;
    
    // ���� �÷��̾� �������Ʈ
    public GameObject playerInRoomOBJ;

    // ���� ���� ���� ��ȣ
    public int artifactNum = -1;    // 0 -> Maze Artifact, 1 -> Jump Artifact, 2 -> treasure Artifact, 3 -> Quiz Artifact 

    // ������
    // 1. Lava Set parameter
    public GameObject Jump_Map_lava;
    public Vector3 lavaStartPos;

    void Start()
    {
        roomState = new Dictionary<GameObject, int>();
        lavaStartPos = new Vector3(0, -37, 300);
        
        for(int i = 0; i < rooms.Length; i++)
        {
            roomState.Add(rooms[i], 0);
        }

        foreach(KeyValuePair<GameObject, int> pair in roomState)
        {
            Debug.Log(pair.Key.name + "," + pair.Value);
        }

        isPause = false;
    }

    public void Field_Change(GameObject interactionOBJ)
    {
        Objects OBJcomponent = interactionOBJ.GetComponent<Objects>();
        if(OBJcomponent.inRoom)
        {
            //isNeedArtifact = true;
            playerInRoomOBJ = OBJcomponent.RoomOBJ;
            if(roomState[playerInRoomOBJ] != 0)
            {
                player.isLoading = false;
                Debug.Log("Aready Clear!!");
                return;
            }
            isNeedArtifact = true;
            Transform nextPos = OBJcomponent.NextRoomPosition;
            systemManager.GetComponent<FadeInOut>().FadeFunc();
                
            StartCoroutine(tpPos(nextPos));
        }
        else
        {
            if(!isGetArtifact && !isNeedArtifact)  {    
                Transform nextPos = OBJcomponent.NextRoomPosition;
                systemManager.GetComponent<FadeInOut>().FadeFunc();
                    
                StartCoroutine(tpPos(nextPos));
            }
            else{
                systemManager.GetComponent<SystemManager>().narration(interactionOBJ);
                Debug.Log("Can't exit");
                player.isLoading = false;
                return;
            }
            if(artifactNum == 0) {
                save_Artifacts[artifactNum].SetActive(true);
                
                PlayerPrefs.SetInt("maze_state", 1);
            }
            else if(artifactNum == 1) {
                save_Artifacts[artifactNum].SetActive(true);
                
                PlayerPrefs.SetInt("jump_state", 1);
            }
            else if(artifactNum == 2) {
                save_Artifacts[artifactNum].SetActive(true);
                
                PlayerPrefs.SetInt("treasure_state", 1);
            }
            else if(artifactNum == 3) {
                save_Artifacts[artifactNum].SetActive(true);
                
                PlayerPrefs.SetInt("RCP_state", 1);
            }
            StartCoroutine(Current_Save());
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
        else if(artifact.name == room_Artifacts[1].name)
        {
            room_Artifacts[1].SetActive(false);
            Jump_Room_Second_Phase();
            roomState[rooms[1]] = 1;
            artifactNum = 1;
        }
        else if(artifact.name == room_Artifacts[2].name)
        {
            room_Artifacts[2].SetActive(false);
            Treasure_Second_Phase();
            roomState[rooms[2]] = 1;
            artifactNum = 2;
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
            maze_Spawn_point[i].transform.GetChild(0).transform.localPosition = new Vector3(0,0,0);
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
        if(artifactNum == 0)
        {
            room_Artifacts[artifactNum].SetActive(true);
            save_Artifacts[artifactNum].SetActive(false);
        }
        artifactNum = -1;
    }

    // ������ 2������
    public void Jump_Room_Second_Phase()
    {
        StartCoroutine(LavaON(Jump_Map_lava));
    }

    public void Jump_Room_First_Phase()
    {
        if(artifactNum == 1)
        {
            room_Artifacts[artifactNum].SetActive(true);
            save_Artifacts[artifactNum].SetActive(false);
        }
        Jump_Map_lava.transform.localPosition = lavaStartPos;
        artifactNum = -1;
    }

    public void Treasure_Second_Phase()
    {
        for(int i = 0; i < treasure_Spawn_point.Length; i++)
        {
            treasure_Spawn_point[i].transform.GetChild(0).transform.localPosition = new Vector3(0,0,0);
            treasure_Spawn_point[i].SetActive(true);
        }
    }

    public void Treasure_First_Phase()
    {
        for(int i = 0; i < treasure_Spawn_point.Length; i++)
        {
            treasure_Spawn_point[i].SetActive(false);
        }
        for(int i = 0; i < treasuresBox.Length; i++)
        {
            treasuresBox[i].GetComponent<Treasure>().reward.SetActive(false);
            treasuresBox[i].GetComponent<Treasure>().box.SetActive(true);
            treasuresBox[i].gameObject.GetComponent<BoxCollider>().enabled = true;        
        }
        if(artifactNum == 2) 
        {
            save_Artifacts[artifactNum].SetActive(false);
        }
        artifactNum = -1;
    }

    public void PauseFunc()
    {
        if(isPause == true)
        {
            Time.timeScale = 1;
            isPause = false;
            systemManager.GetComponent<SystemManager>().pause_Panel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPause = true;
            systemManager.GetComponent<SystemManager>().pause_Panel.SetActive(true);
        }
    }


    // ���� ����
    public void GameOver()
    {
        player.isLoading=true;
        StartCoroutine(GameOverLoadSetting());
    }

    public void Gameload()
    {
        systemManager.GetComponent<SystemManager>().talkId = 0;
        systemManager.GetComponent<SystemManager>().informationId = -1;
        systemManager.GetComponent<SystemManager>().contentNum = 0;
        systemManager.GetComponent<SystemManager>().informationNum = 0;

        if(PlayerPrefs.HasKey("spawnPoint"))
        {
            player.transform.position = spawnPoints[PlayerPrefs.GetInt("spawnPoint")].transform.position;
        }
        else
        {
            player.transform.position = spawnPoints[0].transform.position;
        }

        if(PlayerPrefs.HasKey("maze_state"))
        {
            roomState[rooms[0]] = 1;
            rooms[0].SetActive(false);
            save_Artifacts[0].SetActive(true);
        }
        else
        {
            roomState[rooms[0]] = 0;
            rooms[0].SetActive(true);
            save_Artifacts[0].SetActive(false);
            Maze_Room_First_Phase();
        }

        if(PlayerPrefs.HasKey("jump_state"))
        {
            roomState[rooms[1]] = 1;
            rooms[1].SetActive(false);
            save_Artifacts[1].SetActive(true);
        }
        else
        {
            roomState[rooms[1]] = 0;
            rooms[1].SetActive(true);
            save_Artifacts[1].SetActive(false);
            Jump_Room_First_Phase();
        }

        if(PlayerPrefs.HasKey("treasure_state"))
        {
            roomState[rooms[2]] = 1;
            rooms[2].SetActive(false);
            save_Artifacts[2].SetActive(true);
        }
        else
        {
            roomState[rooms[2]] = 0;
            rooms[2].SetActive(true);
            save_Artifacts[2].SetActive(false);
            Treasure_First_Phase();
        }
    }
    IEnumerator Current_Save()
    {
        yield return new WaitForSeconds(3.0f);
        playerInRoomOBJ.SetActive(false);
    }

    IEnumerator tpPos(Transform nextPos)
    {
        yield return new WaitForSeconds(2.0f);
        Vector3 spawnPos = new Vector3(nextPos.position.x, nextPos.position.y + 0.5f, nextPos.position.z);
        player.transform.position = spawnPos;
    }

    IEnumerator LavaON(GameObject lava)
    {
        yield return new WaitForSeconds(0.125f);
        if(lava.transform.position.y <= 50 && player.live == true)
        {
            lava.transform.Translate(Vector3.up * Time.deltaTime);
            Debug.Log(Jump_Map_lava.transform.position);
            StartCoroutine(LavaON(lava));
        }
    }

    IEnumerator GameOverLoadSetting()
    {
        systemManager.GetComponent<FadeInOut>().GameOverFadeFunc();
        yield return new WaitForSeconds(3.0f);

        player.transform.position = spawnPoints[1].transform.position;
        player.live = true;
        if(artifactNum == 0)
        {
            roomState[rooms[artifactNum]] = 0;
            Maze_Room_First_Phase();
        }
        else if(artifactNum == 1)
        {
            roomState[rooms[artifactNum]] = 0;
            Jump_Room_First_Phase();
        }
        else if(artifactNum == 2)
        {
            roomState[rooms[artifactNum]] = 0;
            Treasure_First_Phase();
        }
    }
}
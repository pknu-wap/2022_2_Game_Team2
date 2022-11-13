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

    public void Field_Change(GameObject interactionOBJ)
    {
        Transform nextPos = interactionOBJ.GetComponent<Objects>().NextRoomPosition;

        systemManager.GetComponent<FadeInOut>().FadeFunc();

        StartCoroutine(tpPos(nextPos));
    }

    IEnumerator tpPos(Transform nextPos)
    {
        yield return new WaitForSeconds(2.0f);
        Vector3 spawnPos = new Vector3(nextPos.position.x, nextPos.position.y + 1, nextPos.position.z);
        player.transform.position = spawnPos;
    }
}
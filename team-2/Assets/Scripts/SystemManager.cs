using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    // �ý��� �Ŵ����� ���� UI �� ������ ���.
    public Player player;
    public GameObject scanOBJ;
    public NPC npc_Cat;
    public GameManager gameManager;
    public GameObject talkPanel;
    public Text talkText;
    public Text nameText;
    public bool isAction;


    public void SetAction(GameObject scanObject)
    {
        if(isAction)
        {
            isAction = false;
        }
        else {
            isAction = true;
            scanOBJ = scanObject;
            nameText.text = scanOBJ.name;
        }

        talkPanel.SetActive(isAction);
    }
}

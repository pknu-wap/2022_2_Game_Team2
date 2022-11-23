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

    public int talkId = 0;
    public int contentNum = 0;


    public void SetTextPanel(GameObject scanObject)
    {
        string content = npc_Cat.GetContent(talkId, contentNum);
        Debug.Log("content : " + content);
        if(content == null)
        {
            talkId = talkId >= 2 ? 2 : talkId+1;
            contentNum = 0;
            isAction = false; 
        }
        else {
            isAction = true;
            scanOBJ = scanObject;
            nameText.text = scanOBJ.name;
            talkText.text = content;
            contentNum++;
        }

        talkPanel.SetActive(isAction);
    }
}

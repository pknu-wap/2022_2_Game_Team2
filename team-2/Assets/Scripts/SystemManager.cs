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
    public GameObject Information_Panel;
    public Text talkText;
    public Text nameText;
    public bool isAction;
    public bool isSelectInformation;
    public bool isInformationAction = false;

    public int talkId = 0;
    public int informationId = -1;
    public int contentNum = 0;
    public int informationNum = 0;


    public void SetTextPanel(GameObject scanObject)
    {
        string content = npc_Cat.GetContent(talkId, contentNum);
        Debug.Log("content : " + content);
        if(content == null)
        {
            if(talkId >= 2)
            {
                if(isInformationAction == false)
                {
                    isInformationAction = true;
                    SetInformationPanel();
                }
                else 
                {
                    GetInformation(informationId, informationNum);
                }
                return;
            }
            contentNum = 0;
            talkId = talkId >= 2 ? 2 : talkId+1;
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

    public void SetInformationPanel()
    {
        isSelectInformation = true;
        Information_Panel.SetActive(true);
    }

    public void GetInformation(int information_id, int information_num)
    {
        string information = npc_Cat.GetInformation(information_id, information_num);

        if(information == null)
        {
            isInformationAction = false;
            informationNum = 0;
            informationId = -1;
            isAction = false;
            contentNum = 0;

            talkPanel.SetActive(isAction);
        }
        else
        {
            talkText.text = information;
            informationNum++;
        }
    }

    public void MazeInformation()
    {
        informationId = 0;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }

    public void JumpInformation()
    {
        informationId = 1;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }

    public void TreasureInformation()
    {
        informationId = 2;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }

    public void RCPInformation()
    {
        informationId = 3;
        Information_Panel.SetActive(false);
        isSelectInformation = false;
        GetInformation(informationId, informationNum);
    }
}

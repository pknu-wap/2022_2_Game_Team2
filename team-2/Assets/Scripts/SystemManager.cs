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

    public void narration(GameObject OBJ = null)
    {
        if(OBJ.CompareTag("Door"))
        {
            Debug.Log("�±׸� : " + OBJ.tag);
            nameText.text = "�����";
            talkText.text = "�ź�ο� ���� ���ؼ� ���� ������ �ʴ´�. ������ ã�ƺ���.";
            talkPanel.SetActive(true);
            StartCoroutine(TextPanelOut());
        }
    }

    public void PlayerText(GameObject scanObject)
    {
        if(scanObject.CompareTag("Broken_Door") && isAction == false)
        {
            isAction = true;
            nameText.text = "Player";
            talkText.text = "���� ������ �ʴ´�...";
        }
        else
        {
            isAction = false;
        }
        talkPanel.SetActive(isAction);   
    }

    public void SetTextPanel(GameObject scanObject)
    {
        string content = npc_Cat.GetContent(talkId, contentNum);
        Debug.Log("content : " + content);
        if(content == null)
        {
            Debug.Log("����Ʈ����");
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
            Debug.Log("����Ʈ�־�");
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

    IEnumerator TextPanelOut()
    {
        yield return new WaitForSeconds(2.0f);
        talkPanel.SetActive(false);
    }
}

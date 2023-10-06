using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;
    private bool justStarted;
    private bool isMobile;

    public static DialogManager instance;

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;

    // Use this for initialization
    void Start()
    {
        instance = this;
        isMobile = GameManager.instance.isMobile;
        SetUiLimits();
    }

    private void Update()
    {
        if (GameManager.instance.isMobile != isMobile) 
        {
            isMobile = GameManager.instance.isMobile;
            SetUiLimits();
        }
    }

    public void ShowDialog(string[] newLines, bool isPerson, DialogActivator ac)
    {
        if (!instance.dialogBox.activeInHierarchy)
        {
            dialogLines = newLines;

            currentLine = 0;

            CheckIfName();

            dialogText.text = dialogLines[currentLine];
            dialogBox.SetActive(true);

            nameBox.SetActive(isPerson);

            GameManager.instance.dialogActive = true;
        }
    }

    public void NextDialog()
    {
        if (instance.dialogBox.activeInHierarchy)
        {
            currentLine++;

            if (currentLine >= dialogLines.Length)
            {
                dialogBox.SetActive(false);
                GameManager.instance.dialogActive = false;

                if (shouldMarkQuest)
                {
                    shouldMarkQuest = false;
                    if (markQuestComplete)
                    {
                        //QuestManager.instance.MarkQuestComplete(questToMark);
                    }
                    else
                    {
                        //QuestManager.instance.MarkQuestIncomplete(questToMark);
                    }
                }
            }
            else
            {
                CheckIfName();

                dialogText.text = dialogLines[currentLine];
            }
        }
    }

    public void CheckIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = true;
    }

    private void SetUiLimits()
    {
        if (isMobile)
        {
            dialogText.rectTransform.anchorMin = new Vector2((float)0.06, (float)0.1);
            dialogText.rectTransform.anchorMax = new Vector2((float)0.94, (float)0.75);

            nameBox.GetComponent<RectTransform>().anchorMin = new Vector2((float)0.02, 1);
            nameBox.GetComponent<RectTransform>().anchorMax = new Vector2((float)0.5, 1);
        }
    }
}
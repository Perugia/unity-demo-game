using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.AudioSettings;

public class DialogActivator : MonoBehaviour, IInteractable
{

    public string[] lines;

    private bool canActivate;
    private bool _ifOpen = false;


    public bool ifOpen
    {
        get { return _ifOpen; }
        set { _ifOpen = value; }
    }

    public bool isPerson = true;

    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;

    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        //if(_ifOpen) ShowDialog();
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }

    private void OnMouseDown()
    {
        if (!GameManager.instance.dialogActive) ShowDialog();
    }

    private void ShowDialog()
    {
        if (canActivate)
        {
            DialogManager.instance.ShowDialog(lines, isPerson, this);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            return;
        }
    }
}
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueDataSO myDialogue;
    private DialogueManager dialogueManager;        //대화 매니저 참조



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueManager = FindAnyObjectByType<DialogueManager>();

        if (dialogueManager == null)
        {
            Debug.Log("다이얼 로그 매니저가 없음");
        }
    }

    private void OnMouseDown()
    {
        if (dialogueManager == null)
        {
            return;
        }
        if(dialogueManager.IsDiaiogueActive())
        {
            return;
        }
        if(myDialogue == null)
        {
            return;
        }

        dialogueManager.StartDialogue(myDialogue);
    }
}

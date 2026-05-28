using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using JetBrains.Annotations;

public class DialogueManager : MonoBehaviour
{
    [Header("Ui 요소")]

    public GameObject DialoguePanel;
    public Image characterImage;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    [Header("기본 설정")]
    public Sprite defaultCharImage;

    [Header("타이핑 효과 설정")]
    public float typingSpeed = 0.05f;
    public bool skipTypingOnClick = true;

    //내부 변수들
    private DialogueDataSO currentDialogue;
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(HandleNextInput);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDialogueActive && Input.GetKeyUp(KeyCode.Space))
        {
            HandleNextInput();
        }
    }



    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        dialogueText.text = "";

        for(int i = 0; i < textToType.Length; i++)
        {
            dialogueText.text += textToType[i];
            yield return new WaitForSeconds(typingSpeed);

        }

        isTyping = false;

    }

    private void CompleteTying()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        isTyping = false;

        //현재 줄의 텍스트 즉시 표시
        if(currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
        }
    }

    void ShowCurrentLine()
    {
        if(currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            if(typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);

            }


        }
        //현재 줄의 대화 내용으로 타이핑 시작
        string currentText = currentDialogue.dialogueLines[currentLineIndex];
        typingCoroutine = StartCoroutine(TypeText(currentText));

    }

    public void ShowNextLine()
    {
        currentLineIndex++;

        //마지막 대화 확인
        if(currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }
    }

    void EndDialogue()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null; 
        }

        isDialogueActive = false;
        isTyping = false;
        DialoguePanel.SetActive(false);
        currentLineIndex = 0;
    }


    public void HandleNextInput()
    {
        if(isTyping && skipTypingOnClick)
        {
            CompleteTying();
        }
        else if(!isTyping)
        {
            ShowNextLine();
        }
    }



    public void SkipDialogue()
    {
        EndDialogue() ;
    }

    public bool IsDiaiogueActive()
    {
        return isDialogueActive;
    }


    public void StartDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;

        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDialogueActive = true;


        DialoguePanel.SetActive(true);
        characterNameText.text = dialogue.characterName;

        if(characterImage != null )
        {
            if(dialogue.characterImage != null)
            {
                characterImage.sprite = dialogue.characterImage;
            }
            else
            {
                characterImage.sprite = defaultCharImage;
            }
        }


        ShowCurrentLine();
    }

}

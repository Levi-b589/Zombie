using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [System.Serializable]
    //Allows you to talk to the NPC
    public class Line
    {
        public string text;
        public string[] choices;   // leave null/empty for a normal line
        public string[] replies;   // NPC reply for each choice

        public Line(string text, string[] choices = null, string[] replies = null)
        {
            this.text = text;
            this.choices = choices;
            this.replies = replies;
        }
    }

    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public GameObject promptUI;            // "Press E to talk" text object
    public Button[] choiceButtons;         // e.g. 2 or 3 buttons, children of the panel
    public TMP_Text[] choiceLabels;        // the TMP text on each button

    // Other scripts (movement, camera, attack...) check this
    public static bool isTalking = false;
    public static bool hasTalked = false;

    private bool playerNearby = false;
    private bool waitingForChoice = false;
    private int dialogueIndex = 0;
    //Dialogue for the user to go through and talk to the NPC
    private Line[] dialogue =
    {
        new Line("NPC: Hey! Wait a minute."),
        new Line("NPC: There is something you should know."),
        new Line("NPC: The next area is dangerous."),
        new Line("NPC: Are you prepared to go in?",
                 new[] { "Yes, I'm ready.", "Not really..." },
                 new[] { "NPC: Good, you'll need that confidence.",
                         "NPC: Then be careful in there." }),
        new Line("NPC: Good luck! You may pass.")
    };
    //if promptUI is not there it doesnt show up
    void Start()
    {
        dialoguePanel.SetActive(false);
        HideChoices();
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }
    //Checks if E was pressed before you talk to the NPC
    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (!Keyboard.current.eKey.wasPressedThisFrame)
        {
            return;
        }

        // Nothing but a choice click can progress while choices are showing
        if (waitingForChoice)
        {
            return;
        }
        //If you pressed E near the NPC to talk then it runs the dialogue
        if (playerNearby && !hasTalked)
        {
            if (!dialoguePanel.activeSelf)
            {
                StartDialogue();
            }
            else
            {
                dialogueIndex++;

                if (dialogueIndex < dialogue.Length)
                {
                    ShowDialogue();
                }
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    void StartDialogue()
    {
        isTalking = true;
        dialoguePanel.SetActive(true);
        if (promptUI != null) promptUI.SetActive(false);
        dialogueIndex = 0;
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Line line = dialogue[dialogueIndex];
        dialogueText.text = line.text;

        if (line.choices != null && line.choices.Length > 0)
        {
            ShowChoices(line);
        }
        else
        {
            HideChoices();
        }
    }

    void ShowChoices(Line line)
    {
        waitingForChoice = true;

        // Free the mouse so the buttons can be clicked
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            bool used = i < line.choices.Length;
            choiceButtons[i].gameObject.SetActive(used);
            if (!used)
            {
                continue;
            }

            choiceLabels[i].text = line.choices[i];

            int index = i; // capture for the lambda
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnChoice(index));
        }
    }

    void OnChoice(int index)
    {
        Line line = dialogue[dialogueIndex];

        // Show the NPC's reply; pressing E afterwards moves to the next line
        dialogueText.text = line.replies[index];

        waitingForChoice = false;
        HideChoices();

        // Re-lock the cursor (delete these two lines if your game uses a visible cursor)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void HideChoices()
    {
        foreach (var b in choiceButtons)
        {
            b.gameObject.SetActive(false);
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        hasTalked = true;
        isTalking = false;
        Debug.Log("Conversation finished!");
    }
    //Makes you talk to the NPC before going to the next scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (promptUI != null && !hasTalked && !isTalking)
            {
                promptUI.SetActive(true);
            }
        }
    }
    //If you dont talk to the NPC then you cant go to the next level.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }
        }
    }
}

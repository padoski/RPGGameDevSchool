using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    
    public Dialogue Dialogue;

    Queue<string> Sentences;

    public GameObject DialoguePanel;
    public Text DisplayText;

    // Zmienne które mowia co jest aktywne szybkosc wypisywania itp
    string ActiveSentence;
    public float TypingSpeed;

    public bool isTalking;


    void Start()
    {
        Sentences = new Queue<string>();   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && DisplayText.text == ActiveSentence)
        {
            if (isTalking == true)
            {
                DisplayNextSentence();
            }
        }

    }

    void StarDialogue()
    {
    
        Sentences.Clear();
        foreach(string sentence in Dialogue.sentenceList)
        {
            Sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }


    //Przejscie do kolejnego zdania
    public void DisplayNextSentence()
    {
        if (Sentences.Count <= 0)
        {
            DisplayText.text = ActiveSentence;
            return;
        }
        
        ActiveSentence = Sentences.Dequeue();
        DisplayText.text = ActiveSentence;
        StartCoroutine(TypeTheSentence(ActiveSentence));
    }

    //NPC
    IEnumerator TypeTheSentence(string sentence)
    {
        DisplayText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DisplayText.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            StarDialogue();
            DialoguePanel.SetActive(true);
            isTalking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialoguePanel.SetActive(false);
        }
    }
}

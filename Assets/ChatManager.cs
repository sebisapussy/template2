using System.Collections;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    // Reference to the TMP text objects
    public TMP_Text chatText1;
    public TMP_Text chatText2;

    // Reference to the AudioSource component
    public AudioSource audioSource;

    public float seconds = 1f;
    // Array of 5 different chat messages
    public string[] chatMessages = {
        "Hello! How are you today?",
        "Did you see that new game trailer?",
        "I think it's going to be a great year for games!",
        "What are you looking forward to the most?",
        "I can't wait to try the new update!"
    };

    // Variable to keep track of the current message index
    private int currentMessageIndex = 0;

    // Coroutine to handle chat text update
    private IEnumerator Start()
    {
        // Loop through all the messages
        while (currentMessageIndex < chatMessages.Length)
        {
            // Disable the audio source before showing the message
            audioSource.enabled = true;

            // Update both chat texts with the current message
            chatText1.text = chatMessages[currentMessageIndex];
            chatText2.text = chatMessages[currentMessageIndex];

            // Wait for the specified duration before updating the chat messages
            yield return new WaitForSeconds(seconds);

            // Enable the audio source after the message is shown
            audioSource.enabled = false;

            // Move to the next message
            currentMessageIndex++;
        }

        // After showing all messages, clear both texts
        chatText1.text = "";
        chatText2.text = "";

        // Optionally, deactivate the GameObject after the chat is finished
        gameObject.SetActive(false);
    }
}

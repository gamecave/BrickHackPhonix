using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayLoop : MonoBehaviour
{
    string[] phonix = { "Abe Odd Hull Luck Oak", "Canoe key Pace He Grit", "Thief Act Huff Them Adder", "We Knit Train Sip Oars", "I'm On Stairs Sink", "Thesis Sawn Who Tomb He" };
    string[] answers = { "A bottle of Coke", "Can you keep a secret", "The fact of the matter", "When it rains it pours", "Monsters Inc.", "This is all new to me" };
    string currentPhonix;
    string currentAnswer;


    [SerializeField] Text promptText;
    [SerializeField] Text timerText;

    float timer = 60.0f;
    bool giveUp = false;
    int giveUpCount = 0;
    int roundCount = 0;

    // Booleans to hold game-states
    bool prompting = false; // Guessing
    bool generating = false; // In-between rounds
    bool waiting = true;    // Starting

    float startTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            // Display Start Screen
            StartScreen();
        }
        else if (generating)
        {
            // Display Round Screen
            GenerationScreen();
        }
        else if (prompting)
        {
            // Display Prompt
            RoundDisplay();
        }
        else
        {
            // Game Over
            EndGame();
        }
    }

    /// <summary>
    /// Beginning of game
    /// </summary>
    void StartScreen()
    {
        startTimer += Time.deltaTime;
        promptText.text = $"Starting in {20 - Mathf.FloorToInt(startTimer)} seconds!";
        timerText.text = "";

        if (startTimer >= 20)
        {
            waiting = false;
            generating = true;
            startTimer = 0.0f;
            roundCount++;
        }
    }

    /// <summary>
    /// In-between rounds display
    /// </summary>
    void GenerationScreen()
    {
        GetPrompt();
        promptText.text = $"Get ready for Round {roundCount}";
        startTimer += Time.deltaTime;
        if (startTimer >= 3)
        {
            generating = false;
            prompting = true;
            startTimer = 0.0f;
        }
    }

    void EndGame()
    {
        promptText.text = "Game Over!\nThe scores are: {scores}";
    }

    /// <summary>
    /// During Round display
    /// </summary>
    void RoundDisplay()
    {
        promptText.text = $"Your phrase is: {currentPhonix}";
        UpdateTimer();
    }

    /// <summary>
    /// Takes one of the prompts randomly and displays on screen.
    /// </summary>
    void GetPrompt()
    {
        giveUpCount = 0;
        giveUp = false;

        int rng = Random.Range(0, 6);
        currentPhonix = phonix[rng];
        currentAnswer = answers[rng];
    }

    /// <summary>
    /// Check player input and compare with answer to phonix
    /// </summary>
    void CheckAnswer(string guess, Player player)
    {
        guess = guess.ToLower();
        guess = guess.Trim();
        if(guess == currentAnswer)
        {
            // Award points based off of timer.
            player.GetComponent<Player>().score += (Mathf.FloorToInt(timer) * 100);
        }
    }

    void RevealAnswer()
    {
        roundCount++;
        timerText.text = "";
        promptText.text = $"The answer is: {currentAnswer}";

        startTimer += Time.deltaTime;
        if (startTimer >= 3)
        {
            startTimer = 0.0f;
            prompting = false;
            waiting = true;
        }
    }

    void UpdateTimer()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = $"Timer: {Mathf.FloorToInt(timer)}";
        }

        if (timer <= 0)
        {
            timer = 0.0f;
            RevealAnswer();
        }
    }
}

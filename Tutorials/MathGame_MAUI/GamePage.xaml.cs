using MathGame.CSharAcademy.Models;
using Windows.ApplicationModel.VoiceCommands;
using Windows.UI.Core;

namespace MathGame.CSharAcademy;

public partial class GamePage : ContentPage
{
	public string GameType { get; set; }
	int firstNumber = 0;
	int secondNumber = 0;
	int score = 0;

	const int totalQuestions = 2;
	int gamesLeft = totalQuestions;


    public GamePage(string gameType)
	{
		InitializeComponent();
		GameType = gameType;
		BindingContext = this;

		CreateNewQuestion();
	
	}	

	private void CreateNewQuestion()
	{

		var random = new Random();

		firstNumber = GameType != "Division" ? random.Next(1, 9) : random.Next(1, 99);
        secondNumber = GameType != "Division" ? random.Next(1, 9) : random.Next(1, 99);

		if (GameType == "Division")
		{
			while(firstNumber < secondNumber || firstNumber % secondNumber != 0)
			{
				firstNumber = random.Next(1, 99);
                secondNumber = random.Next(1, 99);
            }
		}

		QuestionLabel.Text = $"{firstNumber} {GameType} {secondNumber}";
    }

	private void OnAnswerSubmitted(object sender, EventArgs e)
	{
		var answer = Int32.Parse(AnswerEntry.Text);
		var isCorrect = false;

		switch (GameType)
		{
			case "+":
				isCorrect = answer == firstNumber + secondNumber;
				break;
            case "-":
                isCorrect = answer == firstNumber - secondNumber;
                break;
            case "×":
                isCorrect = answer == firstNumber * secondNumber;
                break;
            case "÷":
                isCorrect = answer == firstNumber / secondNumber;
                break;
        }

		ProcessAnswer(isCorrect);
		gamesLeft--;
        AnswerEntry.Text = "";
		if (gamesLeft > 0)
		{
			CreateNewQuestion();
		}
		else
		{
			GameOver();
		}

	}

    private void GameOver()
    {
		GameOperation gameOperation = GameType switch
		{
            "+" => GameOperation.Addition,
            "-" => GameOperation.Subtraction,
            "×" => GameOperation.Multiplication,
            "÷" => GameOperation.Division,
	
		};
		QuestionsArea.IsVisible = false;
		BackToMenuBtn.IsVisible = true;
		GameOverLabel.Text = $"GameOver! You got {score} out of {totalQuestions} right.";

		App.GameRepository.AddGame(new Game()
		{
			DatePlayed = DateTime.UtcNow,
			Type = gameOperation,
			Score = score
		}
		);
    }

    private void ProcessAnswer(bool isCorrect)
	{
		if (isCorrect)
			score += 1;

		AnswerLabel.Text = isCorrect ? "Correct" : "Incorrect";
	}

	private void OnBackToMenu(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}
}
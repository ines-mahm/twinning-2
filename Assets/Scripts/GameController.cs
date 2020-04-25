using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour
{

//Pattern aus Sprite
	public Sprite[] allPattern;
	public List<Sprite> gamePattern = new List<Sprite>();
    
//Leere Liste mit Button Objects
	public List<Button> cards = new List<Button>();
  
// Sprites Pattern aus Ordner laden bei Laufzeit
    void Awake()
	{
		allPattern = Resources.LoadAll<Sprite>("Sprites/cards");
	}

//start
    void Start()
	{
		GetCards();
		AddListeners();
		DefinePattern();
		Shuffle(gamePattern);
		AddCardPattern();
	}

// Leere Karten aufrufen und Befüllen der Liste mit Button Component
	void GetCards()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("CardButton");

		for (int i = 0; i < objects.Length; i++)
		{
			cards.Add(objects[i].GetComponent<Button>());
		}
	}

// Karten Pattern definieren
	void DefinePattern()
	{
		for (int i = 0; i < cards.Count; i++)
		{
			gamePattern.Add(allPattern[i]);
		}

        // Eine Karte doppelt anzeigen, indem man eine überschreibt
		gamePattern[0] = gamePattern[Random.Range(1, (gamePattern.Count - 1))];
	}

// Karten random shufflen    
	void Shuffle(List<Sprite> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Sprite temp = list[i];
			int randomIndex = Random.Range(i, list.Count);
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}
	}

// Definierte Pattern der Kartenliste zuweisen & abbilden   
	void AddCardPattern()
	{
        for (int i = 0; i < cards.Count; i++)
		{
			cards[i].image.sprite = gamePattern[i];
		}
	}

// Bei Klick kontrollieren ob es das Pattern doppelt gibt
	void AddListeners()
	{
        foreach(Button card_btn in cards)
		{
			card_btn.onClick.AddListener(() => Match());
		}
	}

//Kontrollieren ob es das gerade angeklickte Pattern zwei mal gibt
	public void Match()
	{
		int guessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
		string guessedCardName = gamePattern[guessIndex].name;

		int nameOccurences = 0;

		for (int i = 0; i < cards.Count; i++)
		{
			if (guessedCardName == gamePattern[i].name)
			{
				nameOccurences++;
			}
		}

		CheckIfGameIsFinished(nameOccurences > 1);
	}

 //Playercontroll
	void CheckIfGameIsFinished(bool gameWon)
	{
		if (gameWon)
		{
			Debug.Log("You guessed correct! :D");
			Debug.Log("Game Finished");
		}
		else
		{
			Debug.Log("You guessed wrong! :(");
		}
	}


} //GameController
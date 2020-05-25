using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

//Audio
    public AudioSource GameOver;

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
		DefineRandomPattern();
		//Shuffle(gamePattern);
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
	void DefineRandomPattern()
	{
		var listCopy = new List<Sprite>(allPattern);
		var randNum = 0;
		while (listCopy.Count > 0)
		{
			randNum = Random.Range(0, listCopy.Count);
			gamePattern.Add(listCopy[randNum]);
			listCopy.RemoveAt(randNum);
		}

        // Eine Karte doppelt anzeigen, indem man eine überschreibt
		randNum = Random.Range(2, gamePattern.Count);
		gamePattern[randNum%2==0?1:0] = gamePattern[randNum];
	}

	//Lev: ich habe diese methode mit DefinePattern zusammengelegt, weil das sonst zu kompliziert geworden wäre, die doppelte karte dem richtigen feld zuzuweisen.
	// Karten random shufflen    
	//void Shuffle(List<Sprite> list)
	//{
	//	for (int i = 0; i < list.Count; i++)
	//	{
	//		Sprite temp = list[i];
	//		int randomIndex = Random.Range(i, list.Count);
	//		list[i] = list[randomIndex];
	//		list[randomIndex] = temp;
	//	}
	//}

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
		var selectedGO = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
		var player_name = selectedGO.transform.parent.name.EndsWith("A") ? "A" : "B";
		int guessIndex = int.Parse(selectedGO.name);
		string guessedCardName = gamePattern[guessIndex].name;

		int nameOccurences = 0;

		for (int i = 0; i < cards.Count; i++)
		{
			if (guessedCardName == gamePattern[i].name)
			{
				nameOccurences++;
			}
		}

		CheckIfGameIsFinished(nameOccurences > 1, player_name);
	}

 //Playercontroll
	void CheckIfGameIsFinished(bool gameWon, string player_name)
	{
		if (gameWon)
		{
			Debug.Log("Player "+ player_name + " guessed correct! :D");
			Debug.Log("Game Finished");

            SceneManager.LoadScene(2);

		}
		else
		{
			Debug.Log(player_name+", you guessed wrong! :(");
            GameOver.Play();

        }
    }


} //GameController
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour
{
	//Pattern
    [SerializeField]
	private Sprite Image;

	public Sprite[] pattern;
	public List<Sprite> gamePattern = new List<Sprite>();
    //

	public List<Button> cards = new List<Button>();
  
	// Guesses - Controlling Game
    public bool firstGuess, secondGuess;
	private int countGuesses;
    private int countCorrectGuesses;
	private int gameGuesses;

	private int firstGuessIndex, secondGuessIndex;

	private string firstGuessCard, secondGuessCard;
    // 


    void Awake()
	{
		pattern = Resources.LoadAll<Sprite>("Sprites/cards");
	}

    void Start()
	{
		GetCards();
		AddListeners();
		AddGamePattern();
		Shuffle(gamePattern);
		gameGuesses = gamePattern.Count / 2;
		
	}

	void GetCards()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("CardButton");

		for (int i = 0; i < objects.Length; i++)
		{
			cards.Add(objects[i].GetComponent<Button>());
			cards[i].image.sprite = Image;
		}
	}

    //Karten zweimal abbilden
    void AddGamePattern()
	{
		int looper = cards.Count;
		int index = 0;

        for(int i=0; i < looper; i ++)
		{
            if (index == looper / 2)
			{
				index = 0;
			}

			gamePattern.Add(pattern[index]);
			index++;
		}
	}

    void AddListeners()
	{
        foreach(Button card_btn in cards)
		{
			card_btn.onClick.AddListener(() => Match());
		}
	}

    //Card Matching
	public void Match()
	{
		//string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log ("You are Clicking A button named " + name);

        if(!firstGuess)
		{
			firstGuess = true;
			firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			firstGuessCard = gamePattern[firstGuessIndex].name;
            cards[firstGuessIndex].image.sprite = gamePattern[firstGuessIndex];

		} else if (!secondGuess)
		{
			secondGuess = true;
			secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
			secondGuessCard = gamePattern[secondGuessIndex].name;
            cards[secondGuessIndex].image.sprite = gamePattern[secondGuessIndex];

			StartCoroutine(CheckIfTheCardsMatch());
			/*if (firstGuessCard == secondGuessCard)
			{
				Debug.Log("Yeay, the Cards match!");
			} else
			{
				Debug.Log("Sorry, the Cards DON'T match!");
			}*/


		}
	}

    IEnumerator CheckIfTheCardsMatch()
	{
        yield return new WaitForSeconds (1f);
		
		if (firstGuessCard == secondGuessCard) 
		{
			yield return new WaitForSeconds(.5f);

           
			cards[firstGuessIndex].interactable = false;
			cards[secondGuessIndex].interactable = false;

			cards[firstGuessIndex].image.color = new Color(0, 0, 0);
			cards[secondGuessIndex].image.color = new Color(0, 0, 0);

            CheckIfGameIsFinished();
		} else 
		{
			yield return new WaitForSeconds(.5f);

			cards[firstGuessIndex].image.sprite = Image;
			cards[secondGuessIndex].image.sprite = Image;
		}

		yield return new WaitForSeconds(.5f);
		
		firstGuess = secondGuess = false;
	}

	void CheckIfGameIsFinished()
	{
		countCorrectGuesses++;

		if (countCorrectGuesses == gameGuesses)
		{
			Debug.Log("Game Finished");
			Debug.Log("It took you " + countGuesses + " many guess(es) to finish the game");
		}
	}

    void Shuffle(List<Sprite> list)
	{
        for(int i = 0; i < list.Count; i++)
		{
			Sprite temp = list[i];
			int randomIndex = Random.Range(i, list.Count);
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}
	}

} //GameController
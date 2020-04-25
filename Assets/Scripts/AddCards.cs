using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCards : MonoBehaviour
{

	[SerializeField]
	private Transform GameField;

	[SerializeField]
	private GameObject card_btn;

    void Awake()
	{
        for (int i=0; i < 8; i++)
		{
			GameObject card = Instantiate(card_btn);
			card.name = "" + i;
			card.transform.SetParent(GameField, false);
		}
	}
}

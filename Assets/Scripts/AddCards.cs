using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCards : MonoBehaviour
{

	[SerializeField]
	private Transform GameField_A;
	[SerializeField]
	private Transform GameField_B;

	[SerializeField]
	private GameObject card_btn;

    void Awake()
	{
        for (int i=0; i < 16; i++)
		{
			GameObject card = Instantiate(card_btn);
			card.name = "" + i;
			card.transform.SetParent(i%2==0? GameField_A : GameField_B, false);
		}
	}
}

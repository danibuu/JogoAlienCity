using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudaCor : MonoBehaviour
{

	// Use this for initialization
	SpriteRenderer sr;
	private Color[] cores = new Color[] { Color.red, Color.green, Color.blue };
	public int valPedra = 0;
	void Start()
	{
		sr = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (valPedra == 2)
		{
			valPedra = 0;
		}
		else
		{
			valPedra++;
		}
		if (other.gameObject.CompareTag("Player"))
		{
			sr.color = cores [valPedra];
		}

	}
}
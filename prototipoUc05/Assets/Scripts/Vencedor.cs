using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vencedor : MonoBehaviour {

	// Use this for initialization
	public MensagemControle Mensagem;
	void Start () {
		GameObject mensagemControleObject = GameObject.FindWithTag ("MensagemControle");
		if (mensagemControleObject != null) {
			Mensagem = mensagemControleObject.GetComponent<MensagemControle> ();
		}
	}

	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D other) {
		if(other.gameObject.CompareTag("Player"))
		{
			Mensagem.Venceu();
		}
		Destroy(other.gameObject);
	}
}

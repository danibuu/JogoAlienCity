using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiNaAgua : MonoBehaviour {

	// Use this for initialization
	private MensagemControle MC;

	void Start () {
		GameObject mensagemControleObject = GameObject.FindWithTag ("MensagemControle");
		if(mensagemControleObject != null){
			MC = mensagemControleObject.GetComponent<MensagemControle> ();
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.CompareTag("Player"))
		{
			MC.GameOver();
		}
		Destroy (other.gameObject);
	}


	// Update is called once per frame
	void Update () {
		
	}
}

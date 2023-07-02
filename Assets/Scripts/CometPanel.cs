using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometPanel : MonoBehaviour{
	public Comet comet;
	public Player player;
	public TurnManager turnManager;
	
	public void SetCanAdvanceTurn(bool b){
		turnManager.canAdvanceTurn = b;
	}
	
	public void Harvest(){
		player.HarvestComet(comet.gameObject);
		gameObject.SetActive(false);
		SetCanAdvanceTurn(true);
	}
	
	public void Redirect(int direction){
		comet.direction = direction;
		comet.Move();
		gameObject.SetActive(false);
		SetCanAdvanceTurn(true);
	}
}

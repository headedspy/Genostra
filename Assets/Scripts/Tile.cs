using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public Occupant occupant = null;
	public int x;
	public int y;
	
	public bool playerCanMove = false;
	
	public bool colored = false;
	
	public void SetCoordinates(int x, int y){
		this.x = x;
		this.y = y;
	}
	
	public void SetColor(Color c, bool given_colored = true){
		if(occupant == null || occupant is Comet){
			transform.GetChild(0).GetComponent<Renderer>().material.color = c;
			colored = given_colored;
		}
	}
	
	public void SetWhite(){
		transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
		colored = false;
	}
	
	public bool isColored(){
		//return transform.GetChild(0).GetComponent<Renderer>().material.color != Color.white;
		return colored;
	}
}

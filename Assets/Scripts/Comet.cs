using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : Occupant{
	public int direction;
	
	public GameObject cometPanel;
	
	public void Init(){
		direction = Random.Range(0, 6);
		transform.Rotate(0f, direction*60f,0f);
	}
	
	public void HitPlanet(Planet planet){
		if(planet.civilization != null){
			int casualties = (int)((Random.Range(15f, 75f)/100f)*planet.population);

			planet.Kill(casualties);
			float integrityDamage = Random.Range(0f, 10f);
			planet.Damage(integrityDamage);

			Debug.Log(planet.name + " got hit by asteroid. Casualties:" + casualties.ToString() + " and integrity -" + integrityDamage.ToString());
		}else{
			float integrityDamage = Random.Range(0f, 10f);
			planet.Damage(integrityDamage);

			Debug.Log(planet.name + " got hit by asteroid. Integrity -" + integrityDamage.ToString());
		}

		Destroy(gameObject);
	}
	
	public void Move(){
		if(direction == 0){
			x += 1;
		}else if(direction == 1){
			if(y%2==0){
				y += 1;
			}else{
				x += 1;
				y += 1;
			}
		}else if(direction == 2){
			if(y%2==0){
				x -= 1;
				y += 1;
			}else{
				y += 1;
			}
		}else if(direction == 3){
			x -= 1;
		}else if(direction == 4){
			if(y%2==0){
				x -= 1;
				y -= 1;
			}else{
				y -= 1;
			}
		}else if(direction == 5){
			if(y%2==0){
				y -= 1;
			}else{
				x += 1;
				y -= 1;
			}
		}
		
		tile.occupant = null;
		
		try{
			Tile newTile = GameObject.Find(x.ToString() + "," + y.ToString()).GetComponent<Tile>();
			if(newTile.occupant != null && newTile.occupant is Player){
				//if comet hits player
				Debug.Log("Player hit");
				GameObject panel = GameObject.Find("GamePlayCanvas").transform.GetChild(0).gameObject;
				
				panel.SetActive(true);
				panel.GetComponent<CometPanel>().comet = this;
				panel.GetComponent<CometPanel>().player = (newTile.occupant as Player);
				panel.GetComponent<CometPanel>().SetCanAdvanceTurn(false);
			}else if(newTile.occupant is Star){
				Debug.Log("Star hit");
				//if comet hits star
				Destroy(gameObject);
			}else if(newTile.occupant is Planet){
				Debug.Log("Planet hit");
				//if comet hits planet
				Planet planet = (newTile.occupant as Planet);
				HitPlanet(planet);
			}else if(newTile.occupant is Comet){
				//if comet hits comet
			}
			newTile.occupant = this;
			tile = newTile;
			transform.position = newTile.transform.position;
		}catch(System.NullReferenceException e){
			Debug.Log("OOB");
			//comet out of bounds
			Destroy(gameObject);
		}
	}
}

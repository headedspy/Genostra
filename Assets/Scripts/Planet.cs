using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Planet : Occupant {
	public string name;
	
	// 0-top position clockwise rotation
	public Star star;
	public int pos;
	
	public int distance;
	
	public Civilization civilization = null;
	public int population = 0;
	public int populationPerTurn;
	
	public float temperature;
	public float ozone = 100.0f;
	public float stucturalIntegrity = 100.0f;
	
	public GameObject civilizationPanel;
	public TMPro.TextMeshProUGUI planetNameLabel;
	public TMPro.TextMeshProUGUI structuralIntegrityLabel;
	public TMPro.TextMeshProUGUI temperatureLabel;
	public TMPro.TextMeshProUGUI nameLabel;
	public TMPro.TextMeshProUGUI populationLabel;
	public TMPro.TextMeshProUGUI typeLabel;
	public TMPro.TextMeshProUGUI tempResistanceLabel;
	public TMPro.TextMeshProUGUI IQLabel;
	public TMPro.TextMeshProUGUI agressionLabel;
	public TMPro.TextMeshProUGUI evolveSpeedLabel;
	
	public bool reverseOrbit = false;
	
	public void SetTemperature(){
		// set initial temperature
		temperature = (4-distance)*33.3f;
		temperature += Random.Range(-10.0f, 10.0f);
		
		reverseOrbit = Random.Range(0, 2) == 1;
	}
	
	public void Kill(int amount){
		population -= amount;
		civilization.UpdatePopulation();
	}
	
	public void Damage(float damage){
		stucturalIntegrity -= damage;
		
		if(stucturalIntegrity <= 0f){
			Debug.Log(name + " has been destroyed!");
			population = 0;
			populationPerTurn = 0;
			if(civilization != null){
				civilization.UpdatePopulation();
				Destroy(gameObject);
			}
		}
	}
	
	[ContextMenu("Orbit")]
	public void Orbit(bool movePointer = false){
		if(reverseOrbit){
			if(distance == 1){
				if(star.y%2==0){
					if(pos == 0) MovePlanet(-1, -1, movePointer);
					else if(pos == 1) MovePlanet(-1, 0, movePointer);
					else if(pos == 2) MovePlanet(0, +1, movePointer);
					else if(pos == 3) MovePlanet(0, +1, movePointer);
					else if(pos == 4) MovePlanet(+1, 0, movePointer);
					else if(pos == 5) MovePlanet(+1, -1, movePointer);
				}else{
					if(pos == 0) MovePlanet(0, -1, movePointer);
					else if(pos == 1) MovePlanet(-1, 0, movePointer);
					else if(pos == 2) MovePlanet(-1, +1, movePointer);
					else if(pos == 3) MovePlanet(+1, +1, movePointer);
					else if(pos == 4) MovePlanet(+1, 0, movePointer);
					else if(pos == 5) MovePlanet(0, -1, movePointer);
				}
			}else if(distance==2){
				if(star.y%2==0){
					if(pos == 0) MovePlanet(-1, -1, movePointer);
					else if(pos == 1) MovePlanet(0, -1, movePointer);
					else if(pos == 2) MovePlanet(-1, 0, movePointer);
					else if(pos == 3) MovePlanet(-1, 0, movePointer);
					else if(pos == 4) MovePlanet(-1, +1, movePointer);
					else if(pos == 5) MovePlanet(0, +1, movePointer);
					else if(pos == 6) MovePlanet(0, +1, movePointer);
					else if(pos == 7) MovePlanet(+1, +1, movePointer);
					else if(pos == 8) MovePlanet(+1, 0, movePointer);
					else if(pos == 9) MovePlanet(+1, 0, movePointer);
					else if(pos == 10) MovePlanet(0, +1, movePointer);
					else if(pos == 11) MovePlanet(+1, -1, movePointer);
				}else{
					if(pos == 0) MovePlanet(0, -1, movePointer);
					else if(pos == 1) MovePlanet(-1, -1, movePointer);
					else if(pos == 2) MovePlanet(-1, 0, movePointer);
					else if(pos == 3) MovePlanet(-1, 0, movePointer);
					else if(pos == 4) MovePlanet(0, +1, movePointer);
					else if(pos == 5) MovePlanet(-1, +1, movePointer);
					else if(pos == 6) MovePlanet(+1, +1, movePointer);
					else if(pos == 7) MovePlanet(0, +1, movePointer);
					else if(pos == 8) MovePlanet(+1, 0, movePointer);
					else if(pos == 9) MovePlanet(+1, 0, movePointer);
					else if(pos == 10) MovePlanet(+1, -1, movePointer);
					else if(pos == 11) MovePlanet(0, -1, movePointer);
				}
			}else if(distance==3){
				if(star.y%2==0){
					if(pos == 0) MovePlanet(-1, -1, movePointer);
					else if(pos == 1) MovePlanet(0, -1, movePointer);
					else if(pos == 2) MovePlanet(-1, -1, movePointer);
					else if(pos == 3) MovePlanet(-1, 0, movePointer);
					else if(pos == 4) MovePlanet(-1, 0, movePointer);
					else if(pos == 5) MovePlanet(-1, 0, movePointer);
					else if(pos == 6) MovePlanet(0, +1, movePointer);
					else if(pos == 7) MovePlanet(-1, +1, movePointer);
					else if(pos == 8) MovePlanet(0, +1, movePointer);
					else if(pos == 9) MovePlanet(0, +1, movePointer);
					else if(pos == 10) MovePlanet(+1, +1, movePointer);
					else if(pos == 11) MovePlanet(0, +1, movePointer);
					else if(pos == 12) MovePlanet(+1, 0, movePointer);
					else if(pos == 13) MovePlanet(+1, 0, movePointer);
					else if(pos == 14) MovePlanet(+1, 0, movePointer);
					else if(pos == 15) MovePlanet(+1, -1, movePointer);
					else if(pos == 16) MovePlanet(0, -1, movePointer);
					else if(pos == 17) MovePlanet(+1, -1, movePointer);
				}else{
					if(pos == 0) MovePlanet(0, -1, movePointer);
					else if(pos == 1) MovePlanet(-1, -1, movePointer);
					else if(pos == 2) MovePlanet(0, -1, movePointer);
					else if(pos == 3) MovePlanet(-1, 0, movePointer);
					else if(pos == 4) MovePlanet(-1, 0, movePointer);
					else if(pos == 5) MovePlanet(-1, 0, movePointer);
					else if(pos == 6) MovePlanet(-1, +1, movePointer);
					else if(pos == 7) MovePlanet(0, +1, movePointer);
					else if(pos == 8) MovePlanet(-1, +1, movePointer);
					else if(pos == 9) MovePlanet(+1, +1, movePointer);
					else if(pos == 10) MovePlanet(0, +1, movePointer);
					else if(pos == 11) MovePlanet(+1, +1, movePointer);
					else if(pos == 12) MovePlanet(+1, 0, movePointer);
					else if(pos == 13) MovePlanet(+1, 0, movePointer);
					else if(pos == 14) MovePlanet(+1, 0, movePointer);
					else if(pos == 15) MovePlanet(0, -1, movePointer);
					else if(pos == 16) MovePlanet(-1, -1, movePointer);
					else if(pos == 17) MovePlanet(0, -1, movePointer);
				}
			}else if(distance==4){
				if(star.y%2==0){
					if(pos == 0) MovePlanet(-1, -1, movePointer);
					else if(pos == 1) MovePlanet(0, -1, movePointer);
					else if(pos == 2) MovePlanet(-1, -1, movePointer);
					else if(pos == 3) MovePlanet(0, -1, movePointer);
					else if(pos == 4) MovePlanet(-1, 0, movePointer);
					else if(pos == 5) MovePlanet(-1, 0, movePointer);
					else if(pos == 6) MovePlanet(-1, 0, movePointer);
					else if(pos == 7) MovePlanet(-1, 0, movePointer);
					else if(pos == 8) MovePlanet(-1, +1, movePointer);
					else if(pos == 9) MovePlanet(0, +1, movePointer);
					else if(pos == 10) MovePlanet(-1, +1, movePointer);
					else if(pos == 11) MovePlanet(0, +1, movePointer);
					else if(pos == 12) MovePlanet(0, +1, movePointer);
					else if(pos == 13) MovePlanet(+1, +1, movePointer);
					else if(pos == 14) MovePlanet(0, +1, movePointer);
					else if(pos == 15) MovePlanet(+1, +1, movePointer);
					else if(pos == 16) MovePlanet(+1, 0, movePointer);
					else if(pos == 17) MovePlanet(+1, 0, movePointer);
					else if(pos == 18) MovePlanet(+1, 0, movePointer);
					else if(pos == 19) MovePlanet(+1, 0, movePointer);
					else if(pos == 20) MovePlanet(0, -1, movePointer);
					else if(pos == 21) MovePlanet(+1, -1, movePointer);
					else if(pos == 22) MovePlanet(0, -1, movePointer);
					else if(pos == 23) MovePlanet(+1, -1, movePointer);
				}else{
					if(pos == 0) MovePlanet(0, -1, movePointer);
					else if(pos == 1) MovePlanet(-1, -1, movePointer);
					else if(pos == 2) MovePlanet(0, -1, movePointer);
					else if(pos == 3) MovePlanet(-1, -1, movePointer);
					else if(pos == 4) MovePlanet(-1, 0, movePointer);
					else if(pos == 5) MovePlanet(-1, 0, movePointer);
					else if(pos == 6) MovePlanet(-1, 0, movePointer);
					else if(pos == 7) MovePlanet(-1, 0, movePointer);
					else if(pos == 8) MovePlanet(0, +1, movePointer);
					else if(pos == 9) MovePlanet(-1, +1, movePointer);
					else if(pos == 10) MovePlanet(0, +1, movePointer);
					else if(pos == 11) MovePlanet(-1, +1, movePointer);
					else if(pos == 12) MovePlanet(+1, +1, movePointer);
					else if(pos == 13) MovePlanet(0, +1, movePointer);
					else if(pos == 14) MovePlanet(+1, +1, movePointer);
					else if(pos == 15) MovePlanet(0, +1, movePointer);
					else if(pos == 16) MovePlanet(+1, 0, movePointer);
					else if(pos == 17) MovePlanet(+1, 0, movePointer);
					else if(pos == 18) MovePlanet(+1, 0, movePointer);
					else if(pos == 19) MovePlanet(+1, 0, movePointer);
					else if(pos == 20) MovePlanet(+1, -1, movePointer);
					else if(pos == 21) MovePlanet(0, -1, movePointer);
					else if(pos == 22) MovePlanet(+1, -1, movePointer);
					else if(pos == 23) MovePlanet(0, -1, movePointer);
				}
			}

		}else{
			if(distance == 1){
				if(star.y%2==0){
					if(pos == 0) MovePlanet(-1, +1, movePointer);
					else if(pos == 1) MovePlanet(-1, 0, movePointer);
					else if(pos == 2) MovePlanet(0, -1, movePointer);
					else if(pos == 3) MovePlanet(0, -1, movePointer);
					else if(pos == 4) MovePlanet(+1, 0, movePointer);
					else if(pos == 5) MovePlanet(+1, +1, movePointer);
				}else{
					if(pos == 0) MovePlanet(0, +1, movePointer);
					else if(pos == 1) MovePlanet(-1, 0, movePointer);
					else if(pos == 2) MovePlanet(-1, -1, movePointer);
					else if(pos == 3) MovePlanet(+1, -1, movePointer);
					else if(pos == 4) MovePlanet(+1, 0, movePointer);
					else if(pos == 5) MovePlanet(0, +1, movePointer);
				}
			}else if(distance==2){
				if(star.y%2==0){
					if(pos == 0) MovePlanet(-1, +1, movePointer);
					else if(pos == 1) MovePlanet(0, +1, movePointer);
					else if(pos == 2) MovePlanet(-1, 0, movePointer);
					else if(pos == 3) MovePlanet(-1, 0, movePointer);
					else if(pos == 4) MovePlanet(-1, -1, movePointer);
					else if(pos == 5) MovePlanet(0, -1, movePointer);
					else if(pos == 6) MovePlanet(0, -1, movePointer);
					else if(pos == 7) MovePlanet(+1, -1, movePointer);
					else if(pos == 8) MovePlanet(+1, 0, movePointer);
					else if(pos == 9) MovePlanet(+1, 0, movePointer);
					else if(pos == 10) MovePlanet(0, +1, movePointer);
					else if(pos == 11) MovePlanet(+1, +1, movePointer);
				}else{
					if(pos == 0) MovePlanet(0, +1, movePointer);
					else if(pos == 1) MovePlanet(-1, +1, movePointer);
					else if(pos == 2) MovePlanet(-1, 0, movePointer);
					else if(pos == 3) MovePlanet(-1, 0, movePointer);
					else if(pos == 4) MovePlanet(0, -1, movePointer);
					else if(pos == 5) MovePlanet(-1, -1, movePointer);
					else if(pos == 6) MovePlanet(+1, -1, movePointer);
					else if(pos == 7) MovePlanet(0, -1, movePointer);
					else if(pos == 8) MovePlanet(+1, 0, movePointer);
					else if(pos == 9) MovePlanet(+1, 0, movePointer);
					else if(pos == 10) MovePlanet(+1, +1, movePointer);
					else if(pos == 11) MovePlanet(0, +1, movePointer);
				}
			}else if(distance==3){
				if(star.y%2==0){
					if(pos == 0) MovePlanet(-1, +1, movePointer);
					else if(pos == 1) MovePlanet(0, +1, movePointer);
					else if(pos == 2) MovePlanet(-1, +1, movePointer);
					else if(pos == 3) MovePlanet(-1, 0, movePointer);
					else if(pos == 4) MovePlanet(-1, 0, movePointer);
					else if(pos == 5) MovePlanet(-1, 0, movePointer);
					else if(pos == 6) MovePlanet(0, -1, movePointer);
					else if(pos == 7) MovePlanet(-1, -1, movePointer);
					else if(pos == 8) MovePlanet(0, -1, movePointer);
					else if(pos == 9) MovePlanet(0, -1, movePointer);
					else if(pos == 10) MovePlanet(+1, -1, movePointer);
					else if(pos == 11) MovePlanet(0, -1, movePointer);
					else if(pos == 12) MovePlanet(+1, 0, movePointer);
					else if(pos == 13) MovePlanet(+1, 0, movePointer);
					else if(pos == 14) MovePlanet(+1, 0, movePointer);
					else if(pos == 15) MovePlanet(+1, +1, movePointer);
					else if(pos == 16) MovePlanet(0, +1, movePointer);
					else if(pos == 17) MovePlanet(+1, +1, movePointer);
				}else{
					if(pos == 0) MovePlanet(0, +1, movePointer);
					else if(pos == 1) MovePlanet(-1, +1, movePointer);
					else if(pos == 2) MovePlanet(0, +1, movePointer);
					else if(pos == 3) MovePlanet(-1, 0, movePointer);
					else if(pos == 4) MovePlanet(-1, 0, movePointer);
					else if(pos == 5) MovePlanet(-1, 0, movePointer);
					else if(pos == 6) MovePlanet(-1, -1, movePointer);
					else if(pos == 7) MovePlanet(0, -1, movePointer);
					else if(pos == 8) MovePlanet(-1, -1, movePointer);
					else if(pos == 9) MovePlanet(+1, -1, movePointer);
					else if(pos == 10) MovePlanet(0, -1, movePointer);
					else if(pos == 11) MovePlanet(+1, -1, movePointer);
					else if(pos == 12) MovePlanet(+1, 0, movePointer);
					else if(pos == 13) MovePlanet(+1, 0, movePointer);
					else if(pos == 14) MovePlanet(+1, 0, movePointer);
					else if(pos == 15) MovePlanet(0, +1, movePointer);
					else if(pos == 16) MovePlanet(+1, +1, movePointer);
					else if(pos == 17) MovePlanet(0, +1, movePointer);
				}
			}else if(distance==4){
				if(star.y%2==0){
					if(pos == 0) MovePlanet(-1, +1, movePointer);
					else if(pos == 1) MovePlanet(0, +1, movePointer);
					else if(pos == 2) MovePlanet(-1, +1, movePointer);
					else if(pos == 3) MovePlanet(0, +1, movePointer);
					else if(pos == 4) MovePlanet(-1, 0, movePointer);
					else if(pos == 5) MovePlanet(-1, 0, movePointer);
					else if(pos == 6) MovePlanet(-1, 0, movePointer);
					else if(pos == 7) MovePlanet(-1, 0, movePointer);
					else if(pos == 8) MovePlanet(-1, -1, movePointer);
					else if(pos == 9) MovePlanet(0, -1, movePointer);
					else if(pos == 10) MovePlanet(-1, -1, movePointer);
					else if(pos == 11) MovePlanet(0, -1, movePointer);
					else if(pos == 12) MovePlanet(0, -1, movePointer);
					else if(pos == 13) MovePlanet(+1, -1, movePointer);
					else if(pos == 14) MovePlanet(0, -1, movePointer);
					else if(pos == 15) MovePlanet(+1, -1, movePointer);
					else if(pos == 16) MovePlanet(+1, 0, movePointer);
					else if(pos == 17) MovePlanet(+1, 0, movePointer);
					else if(pos == 18) MovePlanet(+1, 0, movePointer);
					else if(pos == 19) MovePlanet(+1, 0, movePointer);
					else if(pos == 20) MovePlanet(0, +1, movePointer);
					else if(pos == 21) MovePlanet(+1, +1, movePointer);
					else if(pos == 22) MovePlanet(0, +1, movePointer);
					else if(pos == 23) MovePlanet(+1, +1, movePointer);
				}else{
					if(pos == 0) MovePlanet(0, +1, movePointer);
					else if(pos == 1) MovePlanet(-1, +1, movePointer);
					else if(pos == 2) MovePlanet(0, +1, movePointer);
					else if(pos == 3) MovePlanet(-1, +1, movePointer);
					else if(pos == 4) MovePlanet(-1, 0, movePointer);
					else if(pos == 5) MovePlanet(-1, 0, movePointer);
					else if(pos == 6) MovePlanet(-1, 0, movePointer);
					else if(pos == 7) MovePlanet(-1, 0, movePointer);
					else if(pos == 8) MovePlanet(0, -1, movePointer);
					else if(pos == 9) MovePlanet(-1, -1, movePointer);
					else if(pos == 10) MovePlanet(0, -1, movePointer);
					else if(pos == 11) MovePlanet(-1,-1, movePointer);
					else if(pos == 12) MovePlanet(+1, -1, movePointer);
					else if(pos == 13) MovePlanet(0, -1, movePointer);
					else if(pos == 14) MovePlanet(+1, -1, movePointer);
					else if(pos == 15) MovePlanet(0, -1, movePointer);
					else if(pos == 16) MovePlanet(+1, 0, movePointer);
					else if(pos == 17) MovePlanet(+1, 0, movePointer);
					else if(pos == 18) MovePlanet(+1, 0, movePointer);
					else if(pos == 19) MovePlanet(+1, 0, movePointer);
					else if(pos == 20) MovePlanet(+1, +1, movePointer);
					else if(pos == 21) MovePlanet(0, +1, movePointer);
					else if(pos == 22) MovePlanet(+1, +1, movePointer);
					else if(pos == 23) MovePlanet(0, +1, movePointer);
				}
			}
		}
	}
	
	private void MovePlanet(int dx, int dy, bool movePointer){
		int new_x = x + dx;
		int new_y = y + dy;
		GameObject newTile = GameObject.Find(new_x.ToString()+","+new_y.ToString());
		
		if(!movePointer){
			x = new_x;
			y = new_y;
			if(newTile.GetComponent<Tile>().occupant != null && newTile.GetComponent<Tile>().occupant is Comet){
				(newTile.GetComponent<Tile>().occupant as Comet).HitPlanet(this);
			}
			tile.GetComponent<Tile>().occupant = null;
			tile = newTile.GetComponent<Tile>();
			tile.GetComponent<Tile>().occupant = this;
			gameObject.transform.position = newTile.transform.position;
			
			if(++pos == 6*distance) pos = 0;
			Orbit(true);
		}else{
			transform.GetChild(1).gameObject.transform.position = newTile.transform.position;
			transform.GetChild(1).LookAt(gameObject.transform);
		}
	}
	
	public void OnMouseOver(){
		planetNameLabel.text = name;
		structuralIntegrityLabel.text = stucturalIntegrity.ToString() + "%";
		temperatureLabel.text = temperature.ToString() + "°";
		if(civilization != null){
			nameLabel.text = civilization.name;
			populationLabel.text = population.ToString() + "/" + civilization.population.ToString() + " (" + populationPerTurn.ToString() + ")";
			typeLabel.text = civilization.type.ToString();
			tempResistanceLabel.text = civilization.genes.tempResistance.ToString();
			IQLabel.text = civilization.genes.IQ.ToString();
			agressionLabel.text = civilization.genes.agression.ToString();
			evolveSpeedLabel.text = civilization.genes.evolutionSpeed.ToString();
			civilizationPanel.SetActive(true);
		}
	}
	
	public void OnMouseExit(){
		planetNameLabel.text = "";
		structuralIntegrityLabel.text = "";
		temperatureLabel.text = "";
		if(civilization != null){
			civilizationPanel.SetActive(false);
		}
	}
}

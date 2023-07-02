using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilization : MonoBehaviour{
	public string name = "";
    public int population;
	public float type;
	public Genes genes;
	public Color color;
	
	public List<Planet> planets;
	
	static List<string> names = new List<string>{"Koris", "Hyraxlians", "Rigexes", "Larkins", "Kurts", "Kharis", "Raleighs", "Sevyns", "Rollins", "Aquilaterals", "Scipios", "Santinos", "Onyxes", "Xenollas", "Orions", "Skylers", "Rextons", "Oberonines", "Thorins", "Forfaxes", "Quentrels", "Zions", "Abraxos", "Malachis", "Xerxes", "Kaidans"};
	
	private string GenerateRandomName(){
		string t_name = "";
		int index = Random.Range(0, names.Count);
		t_name = names[index];
		names.RemoveAt(index);
		
		return t_name;
	}
	
	public void Init(Planet planet, Color color){
		name = GenerateRandomName();
		
		population = Random.Range(1000, 2000);
		planet.populationPerTurn = Random.Range(10, 50);
		type = 0.0f;
		
		planets = new List<Planet>();
		planets.Add(planet);
		planet.population = population;
		
		genes.tempResistance = planet.temperature;
		genes.IQ = 0;
		genes.agression = Random.Range(1, 5);
		genes.evolutionSpeed = Random.Range(0.0003f, 0.0008f);
		
		this.color = color;
		planet.gameObject.transform.GetChild(0).gameObject.SetActive(true);
		planet.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = color;
	}
	
	public void UpdatePopulation(){
		population = 0;
		foreach(Planet planet in planets){
			population += planet.population;
		}
	}
	
	public void Step(){
		type += genes.evolutionSpeed;
		population = 0;
		foreach(Planet planet in planets){
			planet.population += planet.populationPerTurn;
			population += planet.population;
		}
		
		//agression death
		int perc = Random.Range(0, 101);
		if(perc < genes.agression){
			float dead = Random.Range(0.0f, genes.agression+(genes.agression>65?10f:0f));
			Planet planet = planets[Random.Range(0, planets.Count)];
			
			Debug.Log(name + " self-killed " + (int)(planet.population*(dead/100)) + " on " + planet.name);
			planet.population -= (int)(planet.population*(dead/100));
			
			if(genes.agression >= 65f){
				planet.population -= Random.Range(50, 100);
			}
			
			if(planet.population <= 0){
				Debug.Log(name + " eradicated themselves out of " + planet.name);
				planet.civilization = null;
				planet.population = 0;
				planet.populationPerTurn = 0;
				planet.gameObject.transform.GetChild(0).gameObject.SetActive(false);
				planets.Remove(planet);
			}
			
			UpdatePopulation();
		}
		
		//spread to other planets
		if(type > 1.0f){
			if(Random.Range(0f, 100f) < (30f*(type)-30f)-((planets.Count-1)*20f)){
				if(planets[0].star.planets.Count > 1){
					Planet newPlanet = null;
					foreach(Planet planet in planets[0].star.planets){
						if(planet.civilization == null){
							newPlanet = planet;
							break;
						}
					}
					if(newPlanet != null){
						Debug.Log(name + " has spread out to " + newPlanet.name);
						newPlanet.civilization = this;
						newPlanet.population = Random.Range(10, 100);
						newPlanet.populationPerTurn = Random.Range(10, 50);
						newPlanet.populationPerTurn = (int)((Mathf.Abs(newPlanet.temperature-genes.tempResistance)/100f)*50f);
						
						newPlanet.gameObject.transform.GetChild(0).gameObject.SetActive(true);
						newPlanet.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = color;
						
						planets.Add(newPlanet);
					}
				}
			}
		}
	}
}

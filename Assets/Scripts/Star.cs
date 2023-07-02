using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Occupant{
	public List<Planet> planets;
	public bool hasCivilization = false;
	
	[ContextMenu("OrbitAll")]
	public void OrbitAll(){
		foreach(Planet planet in planets){
			planet.Orbit();
		}
	}
	
	[ContextMenu("RandomizeOrbits")]
	public void RandomizeOrbits(){
		foreach(Planet planet in planets){
			int rand = Random.Range(0, 18);
			for(int i=0; i<rand; i++)
				planet.Orbit();
		}
	}
	
	void Start(){
		//InvokeRepeating("OrbitAll", 1, .25f);
	}
}

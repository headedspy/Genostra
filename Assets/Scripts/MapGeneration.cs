using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapGeneration : MonoBehaviour {
	public GameObject gameCamera;
	
    public GameObject tilePrefab;
	public GameObject playerPrefab;
	public GameObject starPrefab;
	public GameObject planetPrefab;
	public GameObject civilizationPrefab;
	
	public Transform playerParent;
	public Transform starParent;
	public Transform planetParent;
	public Transform civilizationParent;
	
	public TurnManager turnManager;
	
	public TMP_InputField name1;
	public TMP_InputField name2;
	public TMP_InputField name3;
	public TMP_InputField name4;
	
	public TMPro.TextMeshProUGUI currentTurnText;
	public GameObject gameCanvas;
	
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
	
	private Color[] civilizationColors = {Color.red, Color.blue, Color.green, Color.magenta};
	private int iColor = 0;
	
	public int x = 0;
	public int y = 0;
	
	private int playersCount = 0;
	public int starsCount;
	
	private string GetRandomPlanetName(){
		string[] name1 = {"Bogru", "Vuzi", "Distu", "Lavo", "Re", "Chao", "Giu", "Cri", "Zundo", "Gicho", "Men", "On", "Ati", "Vooga", "Deseu", "Cruhe", "Kece"};
		string[] name2 = {"nope", "onus", "rina", "lla", "vis", "zuno", "nia", "vyke", "rosie", "wei", "wa", "bos", "pra", "troth", "tania", "tera", "ceron"};
		
		string name;
		bool exists = false;
		do{
			name = "";
			name += name1[Random.Range(0, name1.Length)];
			name += name2[Random.Range(0, name2.Length)];
			
			foreach(Transform planet in planetParent){
				if(planet.gameObject.GetComponent<Planet>().name == name){
					exists = true;
					break;
				}
			}
		}while(exists);
		
		return name;
	}
	
	[ContextMenu("BuildMap")]
	public void BuildMap(){
		Debug.Log("Map build");
		//get player count
		if(name1.text != "") playersCount++;
		if(name2.text != "") playersCount++;
		if(name3.text != "") playersCount++;
		if(name4.text != "") playersCount++;
		
		List<int> playerX = new List<int>();
		List<int> starX = new List<int>();
		
		for(int i=0; i<playersCount; i++){
			int rand;
			do{
				rand = Random.Range(0, x);
			}while(playerX.Contains(rand));
			
			playerX.Add(rand);
		}
		
		for(int i=0; i<starsCount; i++){
			starX.Add(5+(i*10));
		}
		
		int playerY = -1;
		int starY = -1;
		for (int i=0; i<x; i++){
			if(playerX.Contains(i)){
				playerY = Random.Range(0, y);
			}
			
			
			if(starX.Contains(i)){
				do{
					starY = Random.Range(4, y-4);
				}while(playerY == starY);
			}
			
			
			for(int ii=0; ii<y; ii++){
				GameObject newTile = Instantiate(tilePrefab);
				newTile.transform.parent = gameObject.transform;
				newTile.transform.position += new Vector3(ii*1.8f, 0f, (i*2f)+((ii%2)*1f));
				
				newTile.name = i.ToString() + "," + ii.ToString();
				newTile.GetComponent<Tile>().SetCoordinates(i, ii);
				
				// spawn players
				if(ii == playerY){
					GameObject player = Instantiate(playerPrefab);
					player.transform.parent = playerParent;
					player.transform.position = newTile.transform.position;
					playerY = -1;
					
					newTile.GetComponent<Tile>().occupant = player.GetComponent<Player>();
					
					player.GetComponent<Player>().SetCoordinates(i, ii);
					player.GetComponent<Player>().tile = newTile.GetComponent<Tile>();
					
					player.GetComponent<Player>().color = civilizationColors[iColor];
					player.GetComponent<Renderer>().material.color = civilizationColors[iColor];
					player.GetComponent<Player>().Init();
					iColor++;
				}
				//spawn stars
				if(ii == starY){
					GameObject star = Instantiate(starPrefab);
					star.transform.parent = starParent;
					star.transform.position = newTile.transform.position;
					starY = -1;
					
					newTile.GetComponent<Tile>().occupant = star.GetComponent<Star>();
					star.GetComponent<Star>().SetCoordinates(i, ii);
					star.GetComponent<Star>().tile = newTile.GetComponent<Tile>();
					
					int nrOfPlanets = Random.Range(1, 5);
					
					//spawn planets
					//closest
					GameObject planetTile = GameObject.Find((i-1).ToString()+","+ii.ToString());
					GameObject planet = Instantiate(planetPrefab);
					planet.transform.parent = planetParent;
					planet.GetComponent<Planet>().SetCoordinates(i-1, ii);
					planet.GetComponent<Planet>().tile = planetTile.GetComponent<Tile>();
					planetTile.GetComponent<Tile>().occupant = planet.GetComponent<Planet>();
					planet.transform.position = planetTile.transform.position;
					planet.GetComponent<Planet>().star = star.GetComponent<Star>();
					planet.GetComponent<Planet>().distance = 1;
					planet.GetComponent<Planet>().pos = 3;
					planet.GetComponent<Planet>().name = GetRandomPlanetName();
					planet.GetComponent<Planet>().SetTemperature();
					star.GetComponent<Star>().planets.Add(planet.GetComponent<Planet>());
					
					planet.GetComponent<Planet>().civilizationPanel = civilizationPanel;
					planet.GetComponent<Planet>().nameLabel = nameLabel;
					planet.GetComponent<Planet>().structuralIntegrityLabel = structuralIntegrityLabel;
					planet.GetComponent<Planet>().temperatureLabel = temperatureLabel;
					planet.GetComponent<Planet>().planetNameLabel = planetNameLabel;
					planet.GetComponent<Planet>().populationLabel = populationLabel;
					planet.GetComponent<Planet>().typeLabel = typeLabel;
					planet.GetComponent<Planet>().tempResistanceLabel = tempResistanceLabel;
					planet.GetComponent<Planet>().IQLabel = IQLabel;
					planet.GetComponent<Planet>().agressionLabel = agressionLabel;
					planet.GetComponent<Planet>().evolveSpeedLabel = evolveSpeedLabel;
					
					//middle
					if(nrOfPlanets > 1){
						planetTile = GameObject.Find((i-2).ToString()+","+ii.ToString());
						planet = Instantiate(planetPrefab);
						planet.transform.parent = planetParent;
						planet.GetComponent<Planet>().SetCoordinates(i-2, ii);
						planet.GetComponent<Planet>().tile = planetTile.GetComponent<Tile>();
						planetTile.GetComponent<Tile>().occupant = planet.GetComponent<Planet>();
						planet.transform.position = planetTile.transform.position;
						planet.GetComponent<Planet>().star = star.GetComponent<Star>();
						planet.GetComponent<Planet>().distance = 2;
						planet.GetComponent<Planet>().pos = 6;
						planet.GetComponent<Planet>().name = GetRandomPlanetName();
						planet.GetComponent<Planet>().SetTemperature();
						star.GetComponent<Star>().planets.Add(planet.GetComponent<Planet>());
						
						planet.GetComponent<Planet>().civilizationPanel = civilizationPanel;
						planet.GetComponent<Planet>().nameLabel = nameLabel;
						planet.GetComponent<Planet>().structuralIntegrityLabel = structuralIntegrityLabel;
						planet.GetComponent<Planet>().temperatureLabel = temperatureLabel;
						planet.GetComponent<Planet>().planetNameLabel = planetNameLabel;
						planet.GetComponent<Planet>().populationLabel = populationLabel;
						planet.GetComponent<Planet>().typeLabel = typeLabel;
						planet.GetComponent<Planet>().tempResistanceLabel = tempResistanceLabel;
						planet.GetComponent<Planet>().IQLabel = IQLabel;
						planet.GetComponent<Planet>().agressionLabel = agressionLabel;
						planet.GetComponent<Planet>().evolveSpeedLabel = evolveSpeedLabel;
					}
					
					//far
					if(nrOfPlanets > 2){
						planetTile = GameObject.Find((i-3).ToString()+","+ii.ToString());
						planet = Instantiate(planetPrefab);
						planet.transform.parent = planetParent;
						planet.GetComponent<Planet>().SetCoordinates(i-3, ii);
						planet.GetComponent<Planet>().tile = planetTile.GetComponent<Tile>();
						planetTile.GetComponent<Tile>().occupant = planet.GetComponent<Planet>();
						planet.transform.position = planetTile.transform.position;
						planet.GetComponent<Planet>().star = star.GetComponent<Star>();
						planet.GetComponent<Planet>().distance = 3;
						planet.GetComponent<Planet>().pos = 9;
						planet.GetComponent<Planet>().name = GetRandomPlanetName();
						planet.GetComponent<Planet>().SetTemperature();
						star.GetComponent<Star>().planets.Add(planet.GetComponent<Planet>());
						
						planet.GetComponent<Planet>().civilizationPanel = civilizationPanel;
						planet.GetComponent<Planet>().nameLabel = nameLabel;
						planet.GetComponent<Planet>().structuralIntegrityLabel = structuralIntegrityLabel;
						planet.GetComponent<Planet>().temperatureLabel = temperatureLabel;
						planet.GetComponent<Planet>().planetNameLabel = planetNameLabel;
						planet.GetComponent<Planet>().populationLabel = populationLabel;
						planet.GetComponent<Planet>().typeLabel = typeLabel;
						planet.GetComponent<Planet>().tempResistanceLabel = tempResistanceLabel;
						planet.GetComponent<Planet>().IQLabel = IQLabel;
						planet.GetComponent<Planet>().agressionLabel = agressionLabel;
						planet.GetComponent<Planet>().evolveSpeedLabel = evolveSpeedLabel;
					}
					
					//farthest
					if(nrOfPlanets > 3){
						planetTile = GameObject.Find((i-4).ToString()+","+ii.ToString());
						planet = Instantiate(planetPrefab);
						planet.transform.parent = planetParent;
						planet.GetComponent<Planet>().SetCoordinates(i-4, ii);
						planet.GetComponent<Planet>().tile = planetTile.GetComponent<Tile>();
						planetTile.GetComponent<Tile>().occupant = planet.GetComponent<Planet>();
						planet.transform.position = planetTile.transform.position;
						planet.GetComponent<Planet>().star = star.GetComponent<Star>();
						planet.GetComponent<Planet>().distance = 4;
						planet.GetComponent<Planet>().pos = 12;
						planet.GetComponent<Planet>().name = GetRandomPlanetName();
						planet.GetComponent<Planet>().SetTemperature();
						star.GetComponent<Star>().planets.Add(planet.GetComponent<Planet>());
						
						planet.GetComponent<Planet>().civilizationPanel = civilizationPanel;
						planet.GetComponent<Planet>().nameLabel = nameLabel;
						planet.GetComponent<Planet>().structuralIntegrityLabel = structuralIntegrityLabel;
						planet.GetComponent<Planet>().temperatureLabel = temperatureLabel;
						planet.GetComponent<Planet>().planetNameLabel = planetNameLabel;
						planet.GetComponent<Planet>().populationLabel = populationLabel;
						planet.GetComponent<Planet>().typeLabel = typeLabel;
						planet.GetComponent<Planet>().tempResistanceLabel = tempResistanceLabel;
						planet.GetComponent<Planet>().IQLabel = IQLabel;
						planet.GetComponent<Planet>().agressionLabel = agressionLabel;
						planet.GetComponent<Planet>().evolveSpeedLabel = evolveSpeedLabel;
					}
				}
			}
		}
	
		Debug.Log("Map build done");
	}
	
	[ContextMenu("StartGame")]
	public void StartGame(){
		//randomize planets
		foreach(Transform star in starParent){
			star.GetComponent<Star>().RandomizeOrbits();
		}
		
		//init civilizations
		iColor = 0;
		foreach(Transform player in playerParent){
			Planet planet;
			do{
				planet = planetParent.transform.GetChild(Random.Range(0, planetParent.transform.childCount)).GetComponent<Planet>();
			}while(planet.star.hasCivilization);
			
			planet.star.hasCivilization = true;
			GameObject civ = Instantiate(civilizationPrefab);
			civ.transform.parent = civilizationParent;
			player.GetComponent<Player>().civilization = civ.GetComponent<Civilization>();
			planet.civilization = civ.GetComponent<Civilization>();
			civ.GetComponent<Civilization>().Init(planet, civilizationColors[iColor]);
			iColor++;
		}
		
		//init turn manager
		foreach(Transform player in playerParent){
			turnManager.players.Add(player.GetComponent<Player>());
		}
		foreach(Transform star in starParent){
			turnManager.stars.Add(star.GetComponent<Star>());
		}
		foreach(Transform civilization in civilizationParent){
			turnManager.civilizations.Add(civilization.GetComponent<Civilization>());
		}
		
		playerParent.GetChild(0).GetComponent<Player>().canMove = true;
		currentTurnText.text = name1.text;
		
		//init players
		if(name1.text != ""){
			playerParent.GetChild(0).GetComponent<Player>().name = name1.text;
			playerParent.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshPro>().text = name1.text;
		}
		if(name2.text != ""){
			playerParent.GetChild(1).GetComponent<Player>().name = name2.text;
			playerParent.GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshPro>().text = name2.text;
		}
		if(name3.text != ""){
			playerParent.GetChild(2).GetComponent<Player>().name = name3.text;
			playerParent.GetChild(2).GetChild(0).GetComponent<TMPro.TextMeshPro>().text = name3.text;
		}
		if(name4.text != ""){
			playerParent.GetChild(3).GetComponent<Player>().name = name4.text;
			playerParent.GetChild(3).GetChild(0).GetComponent<TMPro.TextMeshPro>().text = name4.text;
		}
		
		name1.transform.parent.gameObject.SetActive(false);
		gameCanvas.SetActive(true);
	}
	
	public void FindPlayer(){
		foreach(Transform player in playerParent){
			if(player.GetComponent<Player>().canMove){
				gameCamera.transform.position = new Vector3(player.transform.position.x, gameCamera.transform.position.y, player.transform.position.z);
				break;
			}
		}
	}
	
	public void FindCivilization(){
		foreach(Transform player in playerParent){
			if(player.GetComponent<Player>().canMove){
				Vector3 sunPos = player.GetComponent<Player>().civilization.planets[0].star.transform.position;
				gameCamera.transform.position = new Vector3(sunPos.x, gameCamera.transform.position.y, sunPos.z);
				break;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour{
    public int turn = 0;
	
	private int currentPlayerTurn = 0;
	
	public TMPro.TextMeshProUGUI currentTurnText;
	public GameObject colorPanel;
	
	public List<Player> players;
	public List<Star> stars;
	public List<Civilization> civilizations;
	
	public MapGeneration map;
	
	public GameObject cometPrefab;
	public Transform cometParent;
	
	public TMPro.TextMeshProUGUI hydrogenValue;
	public TMPro.TextMeshProUGUI oxygenValue;
	public TMPro.TextMeshProUGUI carbonValue;
	public TMPro.TextMeshProUGUI ironValue;
	public TMPro.TextMeshProUGUI antimatterValue;
	
	public bool canAdvanceTurn = true;
	
	void Start(){
		players = new List<Player>();
		stars = new List<Star>();
		civilizations = new List<Civilization>();
	}
	
	void LateUpdate(){
		if(Input.GetKeyDown("space")){
			NextTurn();
		}
	}
	
	public void NextTurn(){
		if(!canAdvanceTurn) return;
		
		foreach(Player player in players){
			if(player.movesVisualized) return;
		}
		players[currentPlayerTurn].canMove = false;
		if(++currentPlayerTurn >= players.Count){
			// move comets
			foreach(Transform comet in cometParent){
				comet.GetComponent<Comet>().Move();
			}
			
			// move planets
			currentPlayerTurn = 0;
			foreach(Star star in stars){
				star.OrbitAll();
			}
			
			// advance civilizations
			foreach(Civilization civilization in civilizations){
				civilization.Step();
			}
			
			// spawn coments
			if(Random.Range(0,11)<4){
				GameObject comet = Instantiate(cometPrefab);
				comet.transform.parent = cometParent;
				
				int spawnSide = Random.Range(0,4);
				//bottom
				if(spawnSide == 0){
					comet.GetComponent<Comet>().x = 0;
					comet.GetComponent<Comet>().y = Random.Range(0, map.y);
					
					List<int> directions = new List<int>(){0,1,5};
					comet.GetComponent<Comet>().direction = directions[Random.Range(0,2)];
				//right
				}else if(spawnSide == 1){
					comet.GetComponent<Comet>().x = Random.Range(0, map.x);
					comet.GetComponent<Comet>().y = map.y-1;
					
					List<int> directions = new List<int>(){4,5};
					comet.GetComponent<Comet>().direction = directions[Random.Range(0,1)];
				//left
				}else if(spawnSide == 2){
					comet.GetComponent<Comet>().x = Random.Range(0, map.x);
					comet.GetComponent<Comet>().GetComponent<Comet>().y = 0;
					
					List<int> directions = new List<int>(){1,2};
					comet.GetComponent<Comet>().direction = directions[Random.Range(0,1)];
				//top
				}else if(spawnSide == 3){
					comet.GetComponent<Comet>().x = map.x-1;
					comet.GetComponent<Comet>().y = Random.Range(0, map.y);
					
					List<int> directions = new List<int>(){2,3,4};
					comet.GetComponent<Comet>().direction = directions[Random.Range(0,2)];
				}
				
				GameObject tile = GameObject.Find(comet.GetComponent<Comet>().x.ToString() + "," + comet.GetComponent<Comet>().y.ToString());
				tile.GetComponent<Tile>().occupant = comet.GetComponent<Comet>();
				comet.GetComponent<Comet>().tile = tile.GetComponent<Tile>();
				comet.transform.position = tile.transform.position;
				comet.GetComponent<Comet>().Init();
			}
			
			turn++;
		}
		players[currentPlayerTurn].canMove = true;
		
		//display inventory
		hydrogenValue.text = players[currentPlayerTurn].hydrogen.ToString();
		carbonValue.text = players[currentPlayerTurn].carbon.ToString();
		oxygenValue.text = players[currentPlayerTurn].oxygen.ToString();
		ironValue.text = players[currentPlayerTurn].iron.ToString();
		antimatterValue.text = players[currentPlayerTurn].antimatter.ToString();
		
		currentTurnText.text = players[currentPlayerTurn].name;
		colorPanel.GetComponent<Image>().color = players[currentPlayerTurn].color;
	}
}

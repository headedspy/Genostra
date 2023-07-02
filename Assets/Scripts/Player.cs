using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Occupant{
	public string name = "";
	public Civilization civilization = null;
	public bool canMove = false;
	public bool movesVisualized = false;
	
	public Color color;
	
	private GameObject gameCamera;
	
	public int hydrogen = 0;
	public int carbon = 0;
	public int oxygen = 0;
	public int iron = 0;
	public int antimatter = 0;
	
	void Start(){
		gameCamera = GameObject.Find("Main Camera");
	}
	
	public void Init(){
		transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = color;
	}
	
	void VisualizeMoves(bool activate){
		if(canMove){
			List<Tile> list = NearbyTiles(tile, 2);
			
			foreach(Tile tile in list){
				if(activate) tile.SetColor(color);
				else tile.SetWhite();
			}
		}
	}
	
	void Update(){
		if(canMove)
			DetectObjectWithRaycast();
	}
	
	public void HarvestComet(GameObject comet){
		int harvest = Random.Range(0, 101);
		if(harvest <= 50){
			hydrogen++;
		}else if(harvest <= 65){
			carbon++;
		}else if(harvest <= 80){
			oxygen++;
		}else if(harvest <= 95){
			iron++;
		}else{
			antimatter++;
		}
		
		Destroy(comet);
	}
	
	public void DetectObjectWithRaycast(){
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = gameCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
				// if player is clicked visualize moves
				if(hit.collider.gameObject == gameObject){
					if(canMove){
						movesVisualized = !movesVisualized;
						VisualizeMoves(movesVisualized);
					}
				}
				//if tile is hit
				else if(hit.collider.gameObject.tag == "Tile" && canMove){
					Tile newTile = hit.collider.gameObject.GetComponent<Tile>();
					//if we catch a comet
					if(hit.collider.gameObject.GetComponent<Tile>().occupant is Comet && newTile.isColored()){
						//HarvestComet(hit.collider.gameObject.GetComponent<Tile>().occupant.gameObject);
						
						GameObject panel = GameObject.Find("GamePlayCanvas").transform.GetChild(0).gameObject;
						panel.SetActive(true);
						panel.GetComponent<CometPanel>().comet = hit.collider.gameObject.GetComponent<Tile>().occupant as Comet;
						panel.GetComponent<CometPanel>().player = this;
						panel.GetComponent<CometPanel>().SetCanAdvanceTurn(false);
					}
					
					if(newTile.isColored()){
						VisualizeMoves(false);
						movesVisualized = false;
						
						tile.occupant = null;
						newTile.occupant = this;
						tile = newTile;
						x = newTile.x;
						y = newTile.y;
						gameObject.transform.position = newTile.gameObject.transform.position;
						
						canMove = false;
					}
				}
            }
        }
    }
}

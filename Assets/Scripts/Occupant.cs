using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Occupant : MonoBehaviour{
    public int x;
	public int y;
	public Tile tile;
	
	public void SetCoordinates(int x, int y){
		this.x = x;
		this.y = y;
	}
	
	private List<Tile> ReturnNearbyTiles(Tile tile){
		List<Tile> nearbyTiles = new List<Tile>();
		
		GameObject tileObject;
		
		//top tile
		tileObject = GameObject.Find((tile.x+1).ToString()+","+(tile.y).ToString());
		if(tileObject != null)
			nearbyTiles.Add(tileObject.GetComponent<Tile>());
		
		//bottom tile
		tileObject = GameObject.Find((tile.x-1).ToString()+","+(tile.y).ToString());
		if(tileObject != null)
			nearbyTiles.Add(tileObject.GetComponent<Tile>());
		
		//left up tile
		tileObject = GameObject.Find((tile.x+(tile.y%2==0?0:1)).ToString()+","+(tile.y-1).ToString());
		if(tileObject != null)
			nearbyTiles.Add(tileObject.GetComponent<Tile>());
		
		//left down tile
		tileObject = GameObject.Find((tile.x-(tile.y%2==0?1:0)).ToString()+","+(tile.y-1).ToString());
		if(tileObject != null)
			nearbyTiles.Add(tileObject.GetComponent<Tile>());
		
		//right up tile
		tileObject = GameObject.Find((tile.x+(tile.y%2==0?0:1)).ToString()+","+(tile.y+1).ToString());
		if(tileObject != null)
			nearbyTiles.Add(tileObject.GetComponent<Tile>());
		
		//right down tile
		tileObject = GameObject.Find((tile.x-(tile.y%2==0?1:0)).ToString()+","+(tile.y+1).ToString());
		if(tileObject != null)
			nearbyTiles.Add(tileObject.GetComponent<Tile>());
		
		return nearbyTiles;
	}
	
	//return all nearby tiles, only works with radius of 1,2,3 and 4
	public List<Tile> NearbyTiles(Tile tile, int radius = 1){
		List<Tile> nearbyList1 = ReturnNearbyTiles(tile);
		if(radius == 1) return nearbyList1;
		List<Tile> nearbyListFinal = new List<Tile>(nearbyList1);
		
		if(radius > 1){
			foreach(Tile nearbyTile in nearbyList1){
				List<Tile> nearbyList2 = ReturnNearbyTiles(nearbyTile);
				foreach(Tile nearbyTile2 in nearbyList2){
					nearbyListFinal.Add(nearbyTile2);
				}
			}
		}
		
		if(radius == 2) return nearbyListFinal.Distinct().ToList();
		
		List<Tile> nearbyListFinal2 = new List<Tile>(nearbyList1);
		
		if(radius > 2){
			foreach(Tile nearbyTile in nearbyListFinal){
				List<Tile> nearbyList3 = ReturnNearbyTiles(nearbyTile);
				foreach(Tile nearbyTile3 in nearbyList3){
					nearbyListFinal2.Add(nearbyTile3);
				}
			}
		}
		
		if(radius == 3) return nearbyListFinal2.Distinct().ToList();
		
		List<Tile> nearbyListFinal3 = new List<Tile>(nearbyList1);
		
		if(radius == 4){
			foreach(Tile nearbyTile in nearbyListFinal2){
				List<Tile> nearbyList4 = ReturnNearbyTiles(nearbyTile);
				foreach(Tile nearbyTile4 in nearbyList4){
					nearbyListFinal3.Add(nearbyTile4);
				}
			}
		}
		
		return nearbyListFinal3.Distinct().ToList();
	}
}

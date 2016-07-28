using UnityEngine;
using System.Collections;

public class MLP_TeamManager : MonoBehaviour {

	MLP_Player mplayer;

	public int idx = 0;
	public void tw10(){ mplayer.UnitIdx=1;}
	public void mpk16(){ mplayer.UnitIdx=2;}
	public void yar09(){ mplayer.UnitIdx=3;}
	public void gro911(){ mplayer.UnitIdx=4;}
	public void shoi90(){ mplayer.UnitIdx=5;}
	public void exp3(){ mplayer.UnitIdx=6;}
	public void gra401(){ mplayer.UnitIdx=7;}
	public void zf10(){ mplayer.UnitIdx=8;}

	void Start(){
		mplayer = GameObject.FindWithTag ("MULTIPLAYER").GetComponent<MLP_Player> ();
	}
		
}

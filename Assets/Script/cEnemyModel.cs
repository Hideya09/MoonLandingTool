using UnityEngine;
using System.Collections;

public class cEnemyModel {

	public Vector3 m_Position;

	public bool move;

	public int number;

	public cEnemyModel( bool setMove , int setNumebr ){
		move = setMove;
		number = setNumebr;
	}
}

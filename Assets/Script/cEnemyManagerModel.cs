using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor;

public class cEnemyManagerModel : ScriptableObject {

	public int m_EnemyMax;

	public List< cEnemyModel > m_eModelList;

	public void Input(){
		string filename = "Enemy";

		TextAsset enemyFile = (TextAsset)Resources.Load (filename);

		StringReader enemyReader = new StringReader (enemyFile.text);

		//敵情報の読み込みと生成
		while (enemyReader.Peek () > -1) {

			string enemyString = enemyReader.ReadLine ();
			string[] enemyData = enemyString.Split (',');

			Vector3 enemyPosition;

			enemyPosition.x = float.Parse (enemyData [0]);
			enemyPosition.y = float.Parse (enemyData [1]);
			enemyPosition.z = 0;

			GameObject enemy;

			if (int.Parse (enemyData [2]) == 1) {
				enemy = (GameObject)Resources.Load ("Prefab/Enemy2");
			} else {
				enemy = (GameObject)Resources.Load ("Prefab/Enemy1");
			}

			enemy.transform.position = enemyPosition;
			GameObject.Instantiate (enemy);
		}

		enemyReader.Close ();
	}

	public void Output(){
		FileInfo file = new FileInfo( Application.dataPath + "/Resources/Enemy.csv");

		StreamWriter write = file.CreateText ();

		for (int i = 0; i < m_eModelList.Count; ++i) {
			Vector3 pos = m_eModelList [i].m_Position;
			bool move = m_eModelList [i].move;

			string str;

			if (move == true) {
				str = pos.x.ToString () + ',' + pos.y.ToString () + ",1";
			} else {
				str = pos.x.ToString () + ',' + pos.y.ToString () + ",0";
			}

			write.WriteLine(str);
		}

		write.Flush ();
		write.Close ();
	}

	void OnEnable(){
		m_EnemyMax = 0;

		m_eModelList = new List< cEnemyModel > ();
	}

	public int EnemyRegistration ( bool setMove ){


		m_eModelList.Add (new cEnemyModel (setMove , m_EnemyMax));

		++m_EnemyMax;

		return m_EnemyMax - 1;
	}

	public void SetPosition( int number , Vector3 pos ){
		for (int i = 0; i < m_eModelList.Count; ++i) {
			if (m_eModelList [i].number == number) {
				m_eModelList [i].m_Position = pos;
			}
		}
	}

	public void Delete( int number ){
		for (int i = 0; i < m_eModelList.Count; ++i) {
			if (m_eModelList [i].number == number) {
				m_eModelList.Remove (m_eModelList [i]);
			}
		}
	}
}

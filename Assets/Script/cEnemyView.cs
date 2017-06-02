using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class cEnemyView : MonoBehaviour {

	private int m_enemyNumber;

	public cEnemyManagerModel m_emanagerModel;

	public bool m_MoveFlag;

	void Awake(){
		m_enemyNumber = m_emanagerModel.EnemyRegistration (m_MoveFlag);
	}

	// Use this for initialization
	void Start () {
		//m_enemyNumber = m_emanagerModel.EnemyRegistration (m_MoveFlag);
	}
	
	// Update is called once per frame
	void Update () {
		m_emanagerModel.SetPosition( m_enemyNumber , transform.position );
	}

	public Vector3 GetPosition(){
		return transform.position;
	}

	void OnDestroy(){
		m_emanagerModel.Delete (m_enemyNumber);
	}
}

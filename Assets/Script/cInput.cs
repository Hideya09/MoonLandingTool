using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class cInput : MonoBehaviour {

	public bool Input;
	public bool Output;

	public cEnemyManagerModel manager;

	public Vector2[] pos; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Output == true) {
			manager.Output ();

			Output = false;
		} else if (Input == true) {
			manager.Input ();

			Input = false;
		}

		pos = new Vector2[manager.m_eModelList.Count];
		for (int i = 0; i < pos.Length; ++i) {
			pos [i] = manager.m_eModelList.ToArray () [i].m_Position;
		}
	}
}
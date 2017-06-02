using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

[ExecuteInEditMode]
public class Stage : MonoBehaviour{

	public EdgeCollider2D m_Stage;

	public Vector2[] m_Point;
	public Vector2[] m_BufPoint;

	public bool input;
	public bool output;

	void Update(){
		if (output == true) {
			FileInfo file = new FileInfo( Application.dataPath + "/Resources/StageInformation.csv");

			StreamWriter write = file.CreateText ();

			for (int i = 0; i < m_Stage.pointCount; ++i) {
				string str = m_Stage.points [i].x.ToString () + ',' + m_Stage.points [i].y.ToString ();

				write.WriteLine(str);
			}

			write.Flush ();
			write.Close ();

			output = false;
		} else if (input == true) {
			string filename = "StageInformation";

			TextAsset stageInformation = (TextAsset)Resources.Load (filename);

			StringReader stageReader = new StringReader (stageInformation.text);

			List< Vector2> pos = new List<Vector2>();

			while (stageReader.Peek () > -1) {
				string posLine = stageReader.ReadLine ();
				string[] posString = posLine.Split (',');

				pos.Add (new Vector2 (float.Parse (posString [0]), float.Parse (posString [1])));
			}

			m_Stage.points = pos.ToArray ();
			pos.ToArray ().CopyTo (m_Stage.points, 0);
			pos.ToArray ().CopyTo (m_Stage.points, 0);

			input = false;
		}

		int count = 0;

		if (m_BufPoint == null || m_Point == null) {
			m_Stage.points.CopyTo (m_Point, 0);
			m_Stage.points.CopyTo (m_BufPoint, 0);
		}

		if (m_BufPoint.Length < 2 || m_Point.Length < 2) {
			m_Stage.points.CopyTo (m_Point, 0);
			m_Stage.points.CopyTo (m_BufPoint, 0);
		}

		if (m_Point.Length != m_BufPoint.Length) {
			Vector2[] buf1 = new Vector2[m_Point.Length];

			m_Point.CopyTo( buf1 , 0 );

			m_Stage.points = buf1;

			m_Point.CopyTo( m_BufPoint , 0 );
		} else if (m_Stage.points.Length != m_BufPoint.Length) {
			m_Point.CopyTo( m_Stage.points , 0 );
			m_Point.CopyTo( m_BufPoint , 0 );
		} else {
			while (count < m_BufPoint.Length) {
				if (m_Point [count] != m_Stage.points [count]) {
					if (m_Point [count] == m_BufPoint [count]) {
						m_Stage.points.CopyTo (m_Point, 0);
						m_Stage.points.CopyTo (m_BufPoint, 0);
					} else if (m_Stage.points [count] == m_BufPoint [count]) {

						Vector2[] buf2 = new Vector2[m_Point.Length];

						m_Point.CopyTo( buf2 , 0 );

						m_Stage.points = buf2;

						m_Point.CopyTo( m_BufPoint , 0 );
					}
					break;
				}
				++count;
			}
		}

		for (int i = 0; i < m_Point.Length; ++i) {
			m_Point [i].x = Mathf.Round (m_Point [i].x);
			m_Point [i].y = Mathf.Round (m_Point [i].y);
		}

		Vector2[] buf3 = new Vector2[m_Point.Length];

		m_Point.CopyTo( buf3 , 0 );

		m_Stage.points = buf3;
		m_Point.CopyTo( m_BufPoint , 0 );

		List<Vector3> vertex = new List<Vector3>();
		List<Vector2> uv = new List<Vector2> ();
		List<int> triangle = new List<int> ();

		for( int i = 0 ; i < m_Stage.pointCount ; ++i ){
			Vector3 vec = m_Stage.points [i];
			vertex.Add (vec);
			uv.Add (new Vector2 (0.0f, 0.0f));

			vec.y = -10.0f;
			vertex.Add (vec);
			uv.Add (new Vector2 (0.0f, 0.0f));

			if ((i * 2) >= 2) {
				triangle.Add (i * 2);
				triangle.Add ((i * 2) - 1);
				triangle.Add ((i * 2) - 2);

				triangle.Add ((i * 2) + 1);
				triangle.Add ((i * 2) - 1);
				triangle.Add (i * 2);
			}
		}

		Mesh mesh = new Mesh ();

		mesh.vertices = vertex.ToArray ();
		mesh.uv = uv.ToArray ();
		mesh.triangles = triangle.ToArray ();

		vertex.Clear ();
		uv.Clear ();
		triangle.Clear ();

		GetComponent< MeshFilter > ().mesh = mesh;
	}
}

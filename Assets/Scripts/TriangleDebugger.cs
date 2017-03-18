// This script draws a debug line around mesh triangles 
// as you move the mouse over them.
using UnityEngine;
using System.Collections;

public class TriangleDebugger : MonoBehaviour {
	Camera camera;

	void Start() {
		camera = GetComponent<Camera>();
	}

	void Update() {
		RaycastHit hit;
		if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
			return;

		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (meshCollider == null || meshCollider.sharedMesh == null)
			return;

		Mesh mesh = meshCollider.sharedMesh;
		Vector3[] vertices = mesh.vertices;

		// emile
		Color[] colors = new Color[vertices.Length];
		// emile
		for (int i = 0; i < vertices.Length; i++)
            colors[i] = Color.green;

		int[] triangles = mesh.triangles;
		Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
		Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
		Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];

		// emile
		colors[triangles[hit.triangleIndex * 3 + 0]] = Color.red;
		colors[triangles[hit.triangleIndex * 3 + 1]] = Color.red;
		colors[triangles[hit.triangleIndex * 3 + 2]] = Color.red;

		Transform hitTransform = hit.collider.transform;
		p0 = hitTransform.TransformPoint(p0);
		p1 = hitTransform.TransformPoint(p1);
		p2 = hitTransform.TransformPoint(p2);
		Debug.DrawLine(p0, p1);
		Debug.DrawLine(p1, p2);
		Debug.DrawLine(p2, p0);

		mesh.colors = colors;

	}
}
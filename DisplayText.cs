using UnityEngine;
using System.Collections;

public class DisplayText : MonoBehaviour {

	public string content = "Hello";
	public string respondsTo;
	public float x = 0f;
	public float y = 0f;
	public Font font;
	public int size;
	public bool show;
	public Color color = new Color(1f, 1f, 1f, 1f);

	private GameObject obj;

	void Start () {
		obj = new GameObject();
		obj.AddComponent<GUIText>();
		obj.transform.position = Camera.main.ScreenToViewportPoint(new Vector3(x,y,0.0f));
		obj.guiText.text = content;
		obj.guiText.color = color;
		obj.guiText.font = font;
		obj.guiText.fontSize = size;
	}

	public void setContent(string key, string s) {
		if (key == respondsTo) content = s;
	}

	public void setContentAsArray(object[] args) {
		if ((string)args[0] == respondsTo) content = (string) args[1];
	}
	
	void OnGUI() {
		obj.guiText.text = content;
	}

}

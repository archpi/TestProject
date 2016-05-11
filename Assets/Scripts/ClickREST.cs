using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;

public class ClickREST : MonoBehaviour {

	public TextMesh charNameMESH;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			UnityWebRequest pathParamReq = UnityWebRequest.Get ("http://localhost:9999/sample/hello-with-path-param/archpi");
			UnityWebRequest queryParamReq = UnityWebRequest.Get ("http://localhost:9999/sample/hello-with-query-param?name=archpi");

			pathParamReq.SetRequestHeader ("Accept", "application/json");
			queryParamReq.SetRequestHeader ("Accept", "application/json");

			AsyncOperation pathParamOp = pathParamReq.Send ();
			while (!pathParamOp.isDone);

			AsyncOperation queryParamOp = queryParamReq.Send ();
			while (!queryParamOp.isDone);

			SamplePoco pathPoco = getPocoFromJson (pathParamReq.downloadHandler.text);
			SamplePoco queryPoco = getPocoFromJson (queryParamReq.downloadHandler.text);

			Debug.Log (pathPoco.name + pathPoco.value);
			Debug.Log (queryPoco.name + queryPoco.value);

			charNameMESH.text = pathPoco.name;
		}
	}

	private SamplePoco getPocoFromJson(string jsonString) {
		return JsonUtility.FromJson<SamplePoco> (jsonString);
	}
}

using UnityEngine;
using System.Collections;

public class DestroyExplode : MonoBehaviour {

	private IEnumerator KillOnAnimationEnd() {
		yield return new WaitForSeconds (0.511f);
		Destroy (gameObject);
	}
		
	void Update () {
		StartCoroutine (KillOnAnimationEnd ());
	}
}

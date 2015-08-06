using UnityEngine;
using System.Collections;

public class RigidCow : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider){
		Destroy(this.gameObject);
	}
}

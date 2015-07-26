using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class AudioPlayer : MonoBehaviour {
	public List<AudioClip> clips = new List<AudioClip>();

	public void PlayRandomMoo(){
		AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Count)], new Vector3(5, 1, 2));
	}
}

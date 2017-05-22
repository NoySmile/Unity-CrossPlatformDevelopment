using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlide : MonoBehaviour
{ 
    public IEnumerator SlideCamera(GameObject go)
    {
        var startpos = transform.position;
        var endpos = go.transform.position;
        float timer = 0f;
        while (Vector3.Distance(transform.position, endpos) < 2f)
        {
            transform.position = Vector3.Lerp(startpos, endpos, timer / 3f);
            timer += Time.deltaTime;
            yield return null;
        }
    }
	
}

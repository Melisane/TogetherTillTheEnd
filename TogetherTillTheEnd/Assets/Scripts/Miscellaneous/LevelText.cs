using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{

    public GameObject TextObject;

    Image TextImage;

    float alpha = 0.0f;
    float time = 0.0f;

    void Start ()
    {
        TextObject = gameObject;
        TextImage = TextObject.GetComponent<Image>();
    }
	
	void Update ()
    {
        time += Time.deltaTime;

        if (time <= 2.0f)
            alpha += Time.deltaTime / 2.0f;

        if (time >= 4.0f)
            alpha -= Time.deltaTime;

        if (time >= 6.0f)
            Destroy(gameObject);

        TextImage.color = new Color(1f, 1f, 1f, alpha);
    }
}

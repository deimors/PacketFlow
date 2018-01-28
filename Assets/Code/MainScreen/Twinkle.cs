using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Twinkle : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var myimages = new List<Image>()
        {
            GameObject.Find("TwinkleP").GetComponent<Image>(),
            GameObject.Find("TwinkleC").GetComponent<Image>(),
            GameObject.Find("TwinkleF").GetComponent<Image>()
        };

        myimages.ForEach(image => image.enabled = false);

        StartCoroutine(TwinkleCoroutine(myimages));
    }

    IEnumerator TwinkleCoroutine(IReadOnlyList<Image> myimages)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            myimages[0].GetComponent<AudioSource>().Play();
            myimages[0].enabled = true;
            yield return new WaitForSeconds(0.2f);
            myimages[0].enabled = false;

            yield return new WaitForSeconds(1f);
            myimages[1].GetComponent<AudioSource>().Play();
            myimages[1].enabled = true;
            yield return new WaitForSeconds(0.2f);
            myimages[1].enabled = false;

            yield return new WaitForSeconds(1f);
            myimages[2].GetComponent<AudioSource>().Play();
            myimages[2].enabled = true;
            yield return new WaitForSeconds(0.2f);
            myimages[2].enabled = false;

            yield return new WaitForSeconds(3);
        }
    }
}

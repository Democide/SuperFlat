using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAnimator : MonoBehaviour
{
    Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("spelunky_animations");
        StartCoroutine("CoChangeSprite");
    }

    // Update is called once per frame
    IEnumerator CoChangeSprite()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        int r = Mathf.RoundToInt(Random.Range(0, sprites.Length));
        GetComponent<SpriteRenderer>().sprite = sprites[r];
        StartCoroutine("CoChangeSprite");
    }
}

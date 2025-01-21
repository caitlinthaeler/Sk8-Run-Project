using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject board;
    // Start is called before the first frame update

    private Vector2 offset;


    void Start()
    {
        board = gameObject.transform.parent.gameObject;
        offset = GetOffset();
        SetPosition();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetPosition();
    }

    public void SetPosition()
    {
        Vector2 boardPos = board.transform.position;
        gameObject.transform.position = boardPos + offset;
    }


    public Vector2 GetOffset()
    {

        float boardHeight = board.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        float skaterHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        return new Vector2(0, boardHeight+skaterHeight);
    }
}

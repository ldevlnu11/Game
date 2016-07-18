using UnityEngine;
using System.Collections;

public class cam : MonoBehaviour {

	public Transform player1;
    public Transform player2;
    public Transform player3;
    public Transform player4;
    private float mid2x;
    private float mid2y;
    private float mid3x;
    private float mid3y;
    private float mid4x;
    private float mid4y;
    private int countOfPlayers = 4;
    private float scaleX;
    private float scaleY;

    void Start ()
    {
        foreach (Transform t in getArr())
        {
            if (t == null) countOfPlayers--;
        }
        scaleX = gameObject.transform.localScale.x;
        scaleY = gameObject.transform.localScale.y;
    }
    void Update () {
       


        switch (countOfPlayers)
        {
            
            case 1:
                gameObject.transform.position = new Vector3(getArr()[0].position.x, getArr()[0].position.y, -10);
                break;
            case 2:
                mid2x = (getArr()[0].position.x + getArr()[1].position.x) / 2;
                mid2y = (getArr()[0].position.y + getArr()[1].position.y) / 2;
                gameObject.transform.position = new Vector3(mid2x,mid2y,-10);
                break;
            case 3:
                mid3x = (getArr()[0].position.x + getArr()[1].position.x + getArr()[2].position.x) / 3;
                mid3y = (getArr()[0].position.y + getArr()[1].position.y + getArr()[2].position.y) / 3;
                gameObject.transform.position = new Vector3(mid3x, mid3y,-10);
                break;
            case 4:
                mid4x = (getArr()[0].position.x + getArr()[1].position.x + getArr()[2].position.x + getArr()[3].position.x) / 4;
                mid4y = (getArr()[0].position.y + getArr()[1].position.y + getArr()[2].position.y + getArr()[3].position.x) / 4;
                gameObject.transform.position = new Vector3(mid3x, mid3y, -10);
                break;
        }

	}

    Transform[] getArr()
    {
        Transform[] arr = { player1, player2, player3, player4 };
        //ArrayList arr = new ArrayList();
        //arr.Add(player1);
        //arr.Add(player2);
        //arr.Add(player3);
        //arr.Add(player4);
        return arr;
    }
    
}

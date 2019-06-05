using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileGenerator : generalManager
{

    // Lista de todos los posibles cubos coloreados existentes
    public List<GameObject> neonTiles;

    private generalManager genManager;

    public tileL Ltiles;
 
    private GameObject parent;
    private GameObject tileElement;   

    public void Awake()
    {
        genManager = new generalManager();

        Ltiles = new tileL();

        // Resto de piezas
    

    }


    // 
    public IEnumerator BuildL(Vector3 origin, tileManagerMode mode)
    {
        Ltiles.buildL(origin);

        parent = (GameObject)Instantiate(neonTiles[0], Ltiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;

        foreach (Vector3 v in Ltiles.attachPoints)
        {
            tileElement = new GameObject();
            tileElement.transform.SetParent(parent.transform);
            tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
            tileElement.transform.localPosition = v;
            tileElement.name = " attach " + v.x;
        }

        foreach (Vector3 v in Ltiles.tileBricks)
        {
            int rnd = Random.Range(0,5);
            if (!v.Equals(Ltiles.tileBricks[0]))
            {          
                tileElement = (GameObject)Instantiate(neonTiles[rnd], Vector3.zero, Quaternion.identity) as GameObject;

                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                tileElement.transform.localPosition = v;

                tileElement.name = "L " + v.x;       
            }
        }

        BoxCollider triggerBox = parent.AddComponent<BoxCollider>();
        triggerBox.size = Ltiles.triggerSize;
        triggerBox.center = Ltiles.triggerCenter;
        triggerBox.isTrigger = true;

        tileElement = new GameObject();
        tileElement.transform.SetParent(parent.transform);
        tileElement.transform.localRotation = Quaternion.Euler(Ltiles.powerUp[2]);
        tileElement.transform.localPosition = Ltiles.powerUp[0];
        tileElement.transform.localScale = Ltiles.powerUp[1];

        Debug.Log("Jauja");

        yield return parent;
    }


    // Aquí irán el resto de piezas


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileGenerator : generalManager
{

    // Lista de todos los posibles cubos coloreados existentes
    public List<GameObject> neonTiles;

    private generalManager genManager;

    public tileL Ltiles;
    public tileI Itiles;
    public tileBox Btiles;
    public tileT Ttiles;
    public tileS Stiles;
 
    private GameObject parent;
    private GameObject tileElement;   

    public void Awake()
    {
        genManager = new generalManager();

        Ltiles = new tileL();
        Ltiles.buildL();
        Itiles = new tileI();
        Itiles.buildI();
        Btiles = new tileBox();
        Btiles.buildBox();
        Ttiles = new tileT();
        Ttiles.buildT();
        Stiles = new tileS();
        Stiles.buildS();
        // Resto de piezas  

    }


    // 
    public IEnumerator BuildL(Vector3 origin, tileManagerMode mode)
    {

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

    public IEnumerator BuildI(Vector3 origin, tileManagerMode mode)
    {

        parent = (GameObject)Instantiate(neonTiles[0], Itiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;

        foreach (Vector3 v in Itiles.attachPoints)
        {
            tileElement = new GameObject();
            tileElement.transform.SetParent(parent.transform);
            tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
            tileElement.transform.localPosition = v;
            tileElement.name = " attach " + v.x;
        }

        foreach (Vector3 v in Itiles.tileBricks)
        {
            int rnd = Random.Range(0,5);
            if (!v.Equals(Itiles.tileBricks[0]))
            {          
                tileElement = (GameObject)Instantiate(neonTiles[rnd], Vector3.zero, Quaternion.identity) as GameObject;

                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                tileElement.transform.localPosition = v;

                tileElement.name = "I " + v.x;       
            }
        }

        BoxCollider triggerBox = parent.AddComponent<BoxCollider>();
        triggerBox.size = Itiles.triggerSize;
        triggerBox.center = Itiles.triggerCenter;
        triggerBox.isTrigger = true;

        tileElement = new GameObject();
        tileElement.transform.SetParent(parent.transform);
        tileElement.transform.localRotation = Quaternion.Euler(Itiles.powerUp[2]);
        tileElement.transform.localPosition = Itiles.powerUp[0];
        tileElement.transform.localScale = Itiles.powerUp[1];

        yield return parent;
    }


    // Aquí irán el resto de piezas

     public IEnumerator BuildBox(Vector3 origin, tileManagerMode mode)
    {

        parent = (GameObject)Instantiate(neonTiles[0], Btiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;

        foreach (Vector3 v in Btiles.attachPoints)
        {
            tileElement = new GameObject();
            tileElement.transform.SetParent(parent.transform);
            tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
            tileElement.transform.localPosition = v;
            tileElement.name = " attach " + v.x;
        }

        foreach (Vector3 v in Btiles.tileBricks)
        {
            int rnd = Random.Range(0,5);
            if (!v.Equals(Btiles.tileBricks[0]))
            {          
                tileElement = (GameObject)Instantiate(neonTiles[rnd], Vector3.zero, Quaternion.identity) as GameObject;

                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                tileElement.transform.localPosition = v;

                tileElement.name = "I " + v.x;       
            }
        }

        BoxCollider triggerBox = parent.AddComponent<BoxCollider>();
        triggerBox.size = Btiles.triggerSize;
        triggerBox.center = Btiles.triggerCenter;
        triggerBox.isTrigger = true;

        tileElement = new GameObject();
        tileElement.transform.SetParent(parent.transform);
        tileElement.transform.localRotation = Quaternion.Euler(Btiles.powerUp[2]);
        tileElement.transform.localPosition = Btiles.powerUp[0];
        tileElement.transform.localScale = Btiles.powerUp[1];

        yield return parent;
    }

    public IEnumerator BuildT(Vector3 origin, tileManagerMode mode)
    {

        parent = (GameObject)Instantiate(neonTiles[0], Ttiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;

        foreach (Vector3 v in Ttiles.attachPoints)
        {
            tileElement = new GameObject();
            tileElement.transform.SetParent(parent.transform);
            tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
            tileElement.transform.localPosition = v;
            tileElement.name = " attach " + v.x;
        }

        foreach (Vector3 v in Ttiles.tileBricks)
        {
            int rnd = Random.Range(0,5);
            if (!v.Equals(Ttiles.tileBricks[0]))
            {          
                tileElement = (GameObject)Instantiate(neonTiles[rnd], Vector3.zero, Quaternion.identity) as GameObject;

                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                tileElement.transform.localPosition = v;

                tileElement.name = "I " + v.x;       
            }
        }

        BoxCollider triggerBox = parent.AddComponent<BoxCollider>();
        triggerBox.size = Ttiles.triggerSize;
        triggerBox.center = Ttiles.triggerCenter;
        triggerBox.isTrigger = true;

        tileElement = new GameObject();
        tileElement.transform.SetParent(parent.transform);
        tileElement.transform.localRotation = Quaternion.Euler(Ttiles.powerUp[2]);
        tileElement.transform.localPosition = Ttiles.powerUp[0];
        tileElement.transform.localScale = Ttiles.powerUp[1];

        yield return parent;
    }

    public IEnumerator BuildS(Vector3 origin, tileManagerMode mode)
    {

        parent = (GameObject)Instantiate(neonTiles[0], Stiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;

        foreach (Vector3 v in Stiles.attachPoints)
        {
            tileElement = new GameObject();
            tileElement.transform.SetParent(parent.transform);
            tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
            tileElement.transform.localPosition = v;
            tileElement.name = " attach " + v.x;
        }

        foreach (Vector3 v in Stiles.tileBricks)
        {
            int rnd = Random.Range(0,5);
            if (!v.Equals(Stiles.tileBricks[0]))
            {          
                tileElement = (GameObject)Instantiate(neonTiles[rnd], Vector3.zero, Quaternion.identity) as GameObject;

                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                tileElement.transform.localPosition = v;

                tileElement.name = "I " + v.x;       
            }
        }

        BoxCollider triggerBox = parent.AddComponent<BoxCollider>();
        triggerBox.size = Stiles.triggerSize;
        triggerBox.center = Stiles.triggerCenter;
        triggerBox.isTrigger = true;

        tileElement = new GameObject();
        tileElement.transform.SetParent(parent.transform);
        tileElement.transform.localRotation = Quaternion.Euler(Stiles.powerUp[2]);
        tileElement.transform.localPosition = Stiles.powerUp[0];
        tileElement.transform.localScale = Stiles.powerUp[1];

        yield return parent;
    }



}

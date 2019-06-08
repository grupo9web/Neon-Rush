using System;
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
    private BoxCollider triggerBox;

    private CoroutineWithData coroutineGO;
    private Queue<int> colorines;

    public void Awake()
    {
        genManager = new generalManager();
        colorines = new Queue<int>();

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

    }


    public IEnumerator tileBuilder(Vector3 origin, tileManagerMode mode, int selector, int count, Queue<int> colorTiles)
    {

        switch (selector)
        {
            case 0:

                parent = (GameObject)Instantiate(neonTiles[colorTiles.Peek()], Ltiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;
                parent.GetComponent<TileScript>().setColores(colorTiles);
                colorTiles.Dequeue();

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
                    int rnd = UnityEngine.Random.Range(0, 5);
                    if (!v.Equals(Ltiles.tileBricks[0]))
                    {
                        tileElement = (GameObject)Instantiate(neonTiles[colorTiles.Dequeue()], Vector3.zero, Quaternion.identity) as GameObject;

                        tileElement.transform.SetParent(parent.transform);
                        tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                        tileElement.transform.localPosition = v;

                        tileElement.name = "L " + v.x;
                    }
                }

                triggerBox = parent.AddComponent<BoxCollider>();
                triggerBox.size = Ltiles.triggerSize;
                triggerBox.center = Ltiles.triggerCenter;
                triggerBox.isTrigger = true;

                tileElement = new GameObject();
                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(Ltiles.powerUp[2]);
                tileElement.transform.localPosition = Ltiles.powerUp[0];
                tileElement.transform.localScale = Ltiles.powerUp[1];

                yield return parent;

                break;

            case 1:

                parent = (GameObject)Instantiate(neonTiles[colorTiles.Peek()], Itiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;
                parent.GetComponent<TileScript>().setColores(colorTiles);
                colorTiles.Dequeue();

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
                    Debug.Log(colorTiles.Count);

                    int rnd = UnityEngine.Random.Range(0, 5);
                    if (!v.Equals(Itiles.tileBricks[0]))
                    {
                        tileElement = (GameObject)Instantiate(neonTiles[colorTiles.Dequeue()], Vector3.zero, Quaternion.identity) as GameObject;

                        tileElement.transform.SetParent(parent.transform);
                        tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                        tileElement.transform.localPosition = v;

                        tileElement.name = "I " + v.x;
                    }
                }

                triggerBox = parent.AddComponent<BoxCollider>();
                triggerBox.size = Itiles.triggerSize;
                triggerBox.center = Itiles.triggerCenter;
                triggerBox.isTrigger = true;

                tileElement = new GameObject();
                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(Itiles.powerUp[2]);
                tileElement.transform.localPosition = Itiles.powerUp[0];
                tileElement.transform.localScale = Itiles.powerUp[1];

                yield return parent;

                break;

            case 2:

                parent = (GameObject)Instantiate(neonTiles[colorTiles.Peek()], Btiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;
                parent.GetComponent<TileScript>().setColores(colorTiles);
                colorTiles.Dequeue();

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
                    Debug.Log(colorTiles.Count);

                    int rnd = UnityEngine.Random.Range(0, 5);
                    if (!v.Equals(Btiles.tileBricks[0]))
                    {
                        tileElement = (GameObject)Instantiate(neonTiles[colorTiles.Dequeue()], Vector3.zero, Quaternion.identity) as GameObject;

                        tileElement.transform.SetParent(parent.transform);
                        tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                        tileElement.transform.localPosition = v;

                        tileElement.name = "B " + v.x;
                    }
                }

                triggerBox = parent.AddComponent<BoxCollider>();
                triggerBox.size = Btiles.triggerSize;
                triggerBox.center = Btiles.triggerCenter;
                triggerBox.isTrigger = true;

                tileElement = new GameObject();
                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(Btiles.powerUp[2]);
                tileElement.transform.localPosition = Btiles.powerUp[0];
                tileElement.transform.localScale = Btiles.powerUp[1];

                yield return parent;

                break;
         
            case 3:

                parent = (GameObject)Instantiate(neonTiles[colorTiles.Peek()], Stiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;
                parent.GetComponent<TileScript>().setColores(colorTiles);
                colorTiles.Dequeue();

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
                    Debug.Log(colorTiles.Count);

                    int rnd = UnityEngine.Random.Range(0, 5);
                    if (!v.Equals(Stiles.tileBricks[0]))
                    {
                        tileElement = (GameObject)Instantiate(neonTiles[colorTiles.Dequeue()], Vector3.zero, Quaternion.identity) as GameObject;

                        tileElement.transform.SetParent(parent.transform);
                        tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                        tileElement.transform.localPosition = v;

                        tileElement.name = "S " + v.x;
                    }
                }

                triggerBox = parent.AddComponent<BoxCollider>();
                triggerBox.size = Stiles.triggerSize;
                triggerBox.center = Stiles.triggerCenter;
                triggerBox.isTrigger = true;

                tileElement = new GameObject();
                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(Stiles.powerUp[2]);
                tileElement.transform.localPosition = Stiles.powerUp[0];
                tileElement.transform.localScale = Stiles.powerUp[1];

                yield return parent;

                break;

            case 4:

                parent = (GameObject)Instantiate(neonTiles[colorTiles.Peek()], Ttiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;
                parent.GetComponent<TileScript>().setColores(colorTiles);
                colorTiles.Dequeue();

                //parent = (GameObject)Instantiate(neonTiles[colorTiles.Dequeue()], Ttiles.tileBricks[0] + origin, Quaternion.Euler(mode.getBO())) as GameObject;

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
                    Debug.Log(colorTiles.Count);

                    int rnd = UnityEngine.Random.Range(0, 5);
                    if (!v.Equals(Ttiles.tileBricks[0]))
                    {
                        tileElement = (GameObject)Instantiate(neonTiles[colorTiles.Dequeue()], Vector3.zero, Quaternion.identity) as GameObject;

                        tileElement.transform.SetParent(parent.transform);
                        tileElement.transform.localRotation = Quaternion.Euler(mode.getBO());
                        tileElement.transform.localPosition = v;

                        tileElement.name = "T " + v.x;
                    }
                }

                triggerBox = parent.AddComponent<BoxCollider>();
                triggerBox.size = Ttiles.triggerSize;
                triggerBox.center = Ttiles.triggerCenter;
                triggerBox.isTrigger = true;

                tileElement = new GameObject();
                tileElement.transform.SetParent(parent.transform);
                tileElement.transform.localRotation = Quaternion.Euler(Ttiles.powerUp[2]);
                tileElement.transform.localPosition = Ttiles.powerUp[0];
                tileElement.transform.localScale = Ttiles.powerUp[1];

                // Las T se marcan como bifurcaciones
                parent.GetComponent<TileScript>().setSimonBifurcado(true);
                parent.GetComponent<TileScript>().setSimonBifurcado(true);

                yield return parent;

                break;

            default:
                yield return null;
                break;
        }
    }

    public GameObject MasterBuilder(Vector3 origin, tileManagerMode mode, int selector)
    {
        switch (selector)
        {
            case 0:
                coroutineGO = new CoroutineWithData(this, BuildS(origin, mode));

                return (GameObject)coroutineGO.result;

            case 1:
                coroutineGO = new CoroutineWithData(this, BuildL(origin, mode));

                return (GameObject)coroutineGO.result;

            case 2:
                coroutineGO = new CoroutineWithData(this, BuildBox(origin, mode));

                return (GameObject)coroutineGO.result;

            case 3:
                coroutineGO = new CoroutineWithData(this, BuildT(origin, mode));

                return (GameObject)coroutineGO.result;

            case 4:
                coroutineGO = new CoroutineWithData(this, BuildI(origin, mode));

                return (GameObject)coroutineGO.result;

            default: return null;
        }

    }

    // Llamandoles individualmente desde TileManager funca benne
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
            int rnd = UnityEngine.Random.Range(0, 5);
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
            int rnd = UnityEngine.Random.Range(0, 5);
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
            int rnd = UnityEngine.Random.Range(0, 5);
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
            int rnd = UnityEngine.Random.Range(0, 5);
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
            int rnd = UnityEngine.Random.Range(0, 5);
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

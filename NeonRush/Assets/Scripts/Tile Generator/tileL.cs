using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileL
{
    // Posiciones donde hay que ggenerar tile con neones
    public List<Vector3> tileBricks = new List<Vector3>();
    public List<Vector3> attachPoints = new List<Vector3>();
    public List<Vector3> powerUp = new List<Vector3>(); 

    // Solid tile blocks
    private Vector3 localL1pos = new Vector3( 0.0f, 0.0f, 0.0f);
    private Vector3 localL2pos = new Vector3( 0.0f, 0.0f, 1.0f);
    private Vector3 localL3pos = new Vector3( 0.0f, 0.0f, 2.0f);
    private Vector3 localL4pos = new Vector3(-1.0f, 0.0f, 2.0f);

    // Attach y powerUp pos
    private Vector3 localLeftAttachPointpos = new Vector3(-2.0f, 0.0f, 2.0f);
    private Vector3 localForwardAttachPointpos = new Vector3(-1.0f, 0.0f, 3.0f);
    private Vector3 localForward2AttachPointpos = new Vector3(-1.0f, 0.0f, 3.0f);       
    private Vector3 localLeftTopAttachPointpos = new Vector3(-2.0f, 1.0f, 2.0f);
    private Vector3 localForwardTopAttachPointpos = new Vector3(-1.0f, 1.0f, 3.0f);
    private Vector3 localForwardTop2AttachPointpos = new Vector3(-1.0f, 1.0f, 3.0f);

    private Vector3 localPowerUpPointpos = new Vector3(0.0f, 0.6f, 1.0f);
    private Vector3 localPowerUpPointScale = new Vector3(0.16f, 0.16f, 0.16f);
    private Vector3 localPowerUpPointRot = new Vector3(0.0f, 90.0f, 0.0f);

    public Vector3 triggerSize = new Vector3(2,0.1f,3);
    public Vector3 triggerCenter = new Vector3(-0.5f, 0.55f,1);


    public void buildL()
    {
        // Posiciones globales de cada bloque
        tileBricks.Add(localL1pos);
        tileBricks.Add(localL2pos);
        tileBricks.Add(localL3pos);
        tileBricks.Add(localL4pos);

        // Posiciones globales de cada attach
        attachPoints.Add(localLeftAttachPointpos);
        attachPoints.Add(localForwardAttachPointpos);
        attachPoints.Add(localForward2AttachPointpos);
        attachPoints.Add(localLeftTopAttachPointpos);
        attachPoints.Add(localForwardTopAttachPointpos);
        attachPoints.Add(localForwardTop2AttachPointpos);

        // Datos del attach del powerUp ( pos/ scale/ rotation)
        powerUp.Add(localPowerUpPointpos);
        powerUp.Add(localPowerUpPointScale);
        powerUp.Add(localPowerUpPointRot);
    }

}

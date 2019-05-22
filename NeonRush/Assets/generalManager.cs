﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalManager : MonoBehaviour
{

    /**
     * Creamos un diccionario que almacene los datos de
     * - Gravedad
     * - Rotación de los bloques 
     * - Dirección de los controles al hacer click
     * 
     * En el propio TileManager y el controlador del jugador 
     * se tendrán en cuenta estos para alterar el mundo
     * 
     **/

    protected Dictionary<string, tileManagerMode> mode = new Dictionary<string, tileManagerMode>();

    // Start is called before the first frame update
    void Awake()
    {

        mode.Add("horizontal", new tileManagerMode( new Vector3(0, -9.8f, 0),    
                                                    Vector3.zero,    
                                                    Vector3.forward, Vector3.left, 
                                                    Vector3.up, 
                                                    new Vector3( 0, 5, -12)));

        mode.Add("verticalZ" , new tileManagerMode( new Vector3( 9.8f, 0, 0), 
                                                    new Vector3( -90, 0, 0), 
                                                    Vector3.up, Vector3.left, 
                                                    Vector3.left   , 
                                                    new Vector3( 0, 5, -12)));

        mode.Add("verticalX" , new tileManagerMode( new Vector3(0, 0,  9.8f), 
                                                    new Vector3( 0, 0, -90), 
                                                    Vector3.up, Vector3.left, 
                                                    Vector3.back, 
                                                    new Vector3( 0, -12,  -5)));
    }

}
using UnityEngine;
using System;

public class MathParabola
{
    
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Vector3 gravedad = GameObject.Find("TileManager").GetComponent<TileManager>().getGrabity();

        

        

        
        if (gravedad.x == -9.8f || gravedad.y == -9.8f || gravedad.z == -9.8f){

            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
            Vector3 mid = Vector3.Lerp(start, end, t);


            if (gravedad.x != 0)
                return new Vector3(f(t) + Mathf.Lerp(start.x, end.x, t), mid.y, mid.z); //Funciona
            else if (gravedad.y != 0)
                return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z); //Funciona
            else
                return new Vector3(mid.x,  mid.y, f(t) + Mathf.Lerp(start.z, end.z, t));
        }else {
            height = - height;

            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

            Vector3 mid = Vector3.Lerp(start, end, t);

            if (gravedad.x != 0)
                return new Vector3(f(t) + Mathf.Lerp(start.x, end.x, t), mid.y, mid.z);
            else if (gravedad.y != 0)
                return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);//Funciona
            else
                return new Vector3(mid.x,  mid.y, f(t) + Mathf.Lerp(start.z, end.z, t));//Funciona
        }        


    }
    
    


    ////// Si la gravedad 
    // Pabajo --> (-Y)
    //      return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    // Parrriba --> (Y)
    //      el mismo pero con la altura signo diferente
    // Izquierda --> (-X)
    //      return new Vector3(f(t) + Mathf.Lerp(start.x, end.x, t), mid.y, mid.z);
    // Derecha --> (X) 
    //      la misma pero con la altura negada
    // Palante --> (Z)
    //      return new Vector3(mid.x,  mid.y, f(t) + Mathf.Lerp(start.z, end.z, t));
    // Patras --> (-Z)
    //      a misma pero con la altura negada

}
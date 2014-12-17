using UnityEngine;
using System.Collections;
using x16;

public class PanelPublicidad: PanelScript
{

    //Ya esta referenciada afuera, aca es donde se renderea la imagen que se jale de parse
    public UITexture ImagenPublicidad;
    public UIButton ButtonWebSite;
    public UIButton ButtonFacebook;
    
    public ActionOpenUrl LinkWebsite;
    public ActionOpenUrl LinkFacebook;

    // Like start
    void OnEnable()
    {
        // Seria bueno de entrada deshabilitar los botones de FB y WebSite y solo habilitarlos
        // cuando se hayan jalado de parse los url

       //Usar PARSE para conectarse y obtener la imagen de la publicidad Actual.
        // La cuestion es como jalarla y luego ponerla en una textura
        //https://parse.com/questions/unity-after-i-uploaded-a-jpeg-as-a-parsefile-using-the-data-browser-how-can-i-retrieve-and-display-it-from-unity-with-c-code
        //http://studiofive27.com/index.php/388/

        // Al jarlar la imagen, hay que setearla en ImagenPublicidad.renderer.material.mainTexture  
        // Al jalar de parse  el website url y fb url setearlos en
        LinkWebsite.url = "http://www.google.com";
        LinkFacebook.url = "http://www.facebook.com";




    }



    void OnDisable()
    {
        print("On disable");
    }
}

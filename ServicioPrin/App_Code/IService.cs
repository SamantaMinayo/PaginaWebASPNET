//*****************************************************************************************************************
//Codigo02
//Victoria Elizabeth Camacho Muncha
//Jennifer Samanta Minayo Yujato
//Fecha de entrega: 05/08/2018

//                  *****************************************************************************************
//                                      TEMA: Proyecto 02
//                  *****************************************************************************************
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
[ServiceContract]
public interface IService
{
    //Defnimos la operacionContract  que nos permitira mostrar la lista de todos los artistas
    [OperationContract]
    List<Artista> ListarArt();

    //Defnimos la operacionContract que nos permitira mostrar la lista de todos los disco
    [OperationContract]
    List<Disco> ListarDisco();

    //Defnimos la operacionCotract para el metodo que nos permitira mostrar la lista de todos las canciones
    //En este caso solo se ostraran las canciones correspondientes a un disco
    [OperationContract]
    List<Cancion> ListarCan(int idD);

    //Cada vez que se seleccione un artista de la lista para ver la informacion 
    //de ese artista se realizara una busqueda dentro de la tabla de artistas
    //en base al id del artista
    [OperationContract]
    Artista BuscarArt(int idD);


    //Cada vez que se seleccione un disco de la lista para ver la informacion 
    //de ese disco se realizara una busqueda dentro de la tabla de discos
    //en base al id del disco
    [OperationContract]
    Disco BuscarDisco(int idD);

    [OperationContract]
    Cancion BuscarCancion(int idC);

    //Defnimos la operacionCotract que nos permitira mostrar la lista de todos los disco
    //correspondientes a un artista
    [OperationContract]
    List<Disco> BuscarDiscoArt(int idA);

    //Defnimos la operacionCotract que nos permitira mostrar la lista de todos los disco
    //encontrados bajo un criterio de busqueda que se lo ingresara en el formulario principal
    [OperationContract]
    List<Disco> BusquedaDis(string disco);

    //Defnimos la operacionCotract que nos permitira mostrar la lista de todos los artistas
    //encontrados bajo un criterio de busqueda que se lo ingresara en el formulario principal
    [OperationContract]
    List<Artista> BusquedaArt(string artista);

    [OperationContract]
    void ActualizarArt(int idA, string nombre, string apellido, string Nartis, string nacion);

    [OperationContract]
    void ActualizarDis(int idC, string nombreD, int anio);

    [OperationContract]
    void ActualizarCan(int idC, string nombreD);
}

//Definimos las clases necesarias para interactuar con la base de datos y los formularios
//Definimos los correspondientes datamember (para cada atributo)
//y tambien sus gets y sets

//La clase artista nos permitira obtener los datos de la tabla artista
//para poder usarlos dentro de la logica de los Web forms de igual manera
//las clases Cancion y Disco con sus respectivos atributos.
[DataContract]
public class Artista
{
    
    [DataMember]
    public int idArtista { get; set; }
    [DataMember]
    public string nombre { get; set; }
    [DataMember]
    public string apellido { get; set; }
    [DataMember]
    public string nombreArtistico { get; set; }
    [DataMember]
    public string nacionalidad { get; set; }
    //Este atributo contendra la sumatoria de todas 
    //las calificaciones dadas por los usuarioa
    [DataMember]
    public int sumatoria { get; set; }
    //Aqui se almacenara el numero de usuarios
    //que han calificado y asi poder obtener un promedio 
    //global del artista
    [DataMember]
    public int puntuacion { get; set; }
}
[DataContract]
public class Cancion
{
    //Inicializamos los atributos del artista.
    [DataMember]
    public int idCancion { get; set; }
    [DataMember]
    public int idDisco { get; set; }
    [DataMember]
    public string nombreCancion { get; set; }
}
[DataContract]
public class Disco
{
    //Inicializamos los atributos del artista.
    [DataMember]
    public int idDisco { get; set; }
    [DataMember]
    public int idArtista { get; set; }
    [DataMember]
    public string nombreDisco { get; set; }
    [DataMember]
    public int anoPublicacion { get; set; }

    //Este atributo contendra la sumatoria de todas 
    //las calificaciones dadas por los usuarioa
    [DataMember]
    public int sumatoria { get; set; }
    //Aqui se almacenara el numero de usuarios
    //que han calificado y asi poder obtener un promedio 
    //global del artista
    [DataMember]
    public int puntuacion { get; set; }
}

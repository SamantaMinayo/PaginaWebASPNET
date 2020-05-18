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

    //Definimos la operacion que permitira buscar si un usuario
    //existe en la base de datos para que pueda logearse en la aplicacion
    [OperationContract]
    Usuario ValidarUsuario(string nombre, string contrasenia);

    //Definimos la operacion que buscar el usuario que ingreso a la
    //aplicacion, esta busqueda la hacemos en base al id que se lo 
    //para entre los web forms para registrar la actividad de calificacion que vaya haciendo cada usuario
    [OperationContract]
    Usuario BuscarUsuario(int idU);

    //Una vez que el usuario se encuentre dentro de la aplicacion
    //podra calificar al artista para ello necesitamos
    //saber que usuario califico a que artista y que
    //calificacion le dio
    [OperationContract]
    void CalificarArtista(int idU,int idA, int calificacion);

    //Una vez que el usuario se encuentre dentro de la aplicacion
    //podra calificar al disco para ello necesitamos
    //saber que usuario califico a que disco y que
    //calificacion le dio
    [OperationContract]
    void CalificarDisco(int idU, int idD, int calificacion);

    //Para mostrar si el usuario a realizado o no la calificacion del
    //artista buscamos en base al id del usuario y del artista para
    //que pueda agregar su calificacion o editar la existente
    [OperationContract]
    int BuscarCalArt(int idU, int idA);

    //Para mostrar si el usuario a realizado o no la calificacion del
    //disco buscamos en base al id del usuario y del disco para
    //que pueda agregar su calificacion o editar la existente
    [OperationContract]
    int BuscarCalDis(int idU, int idD);

    //Si si existe la calificacion se procedera a actualizar el registro de la tabla
    //CalificacionArtista
    [OperationContract]
    void AcCalifArtista(int idU, int idA, int calificacion);

    //Si si existe la calificacion se procedera a actualizar el registro de la tabla
    //CalificacionDisco
    [OperationContract]
    void AcCalifDisco(int idU, int idD, int calificacion);

    //Cada vez que se realiza una nueva calificacion o se 
    //actualiza alguna otra es necesario actualizar esos datos 
    //dentro de la tabla Artista para saber su puntaje total 
    //y cuantos usuarios lo han calificado y asi hallar el puntaje global 
    //del artista
    [OperationContract]
    void AcArtista(int idA, int sumato, int pun);

    //Cada vez que se realiza una nueva calificacion o se 
    //actualiza alguna otra es necesario actualizar esos datos 
    //dentro de la tabla Disco para saber su puntaje total 
    //y cuantos usuarios lo han calificado y asi hallar el puntaje global 
    //del disoc
    [OperationContract]
    void AcDisco(int idA, int sumato, int pun);

    // TODO: agregue aquí sus operaciones de servicio
}

//Definimos el contrato de los datos de usuario
//que nos permitiran interactuar con la base de datos
//para calificar tanto al artisca como al disco e ir almacenando 
//esa informacion en las respectivas tablas
[DataContract]
public class Usuario
{
    //Inicializamos los atributos del artista.
    [DataMember]
    public int idUsuario { get; set; }
    [DataMember]
    public string nombre { get; set; }
    [DataMember]
    public string apellido { get; set; }
    [DataMember]
    public string username { get; set; }
    [DataMember]
    public string password { get; set; }
}

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
using System.Data.SqlClient;
// NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
public class Service : IService
{
    //Cadena de conexion de la base de datos
    private static string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\work\Desktop\Codigo02_VictoriaC_SamantaM\ServicioPrin\App_Code\Database1.mdf;Integrated Security=True;Connect Timeout=30";

    public static SqlConnection obtenerConexion()
    {
        /*Se intancia el objeto de conexión a la base de datos */
        SqlConnection miconexion = new SqlConnection(cadenaConexion);
        //Abrimos la conexión
        miconexion.Open();
        return miconexion;
    }
    //Para saber s el usuario existe o no lo buscaremos en base a su
    //username y a su password por lo que este metodo nos devolvera 
    //un usuario null en caso de que no exista y si si existe 
    //obtendremos el usuario que se logeara en el sistemas
    public Usuario ValidarUsuario(string nombre, string contrasenia)
    {
        Usuario usuario = new Usuario();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT * FROM TblUsuarios where Usuario ='" + nombre + "' and " + "Contrasenia ='" + contrasenia + "'";
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        //"' and " + "Contrasenia ='" + contrasenia+
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                usuario.idUsuario = reader.GetInt32(0);
                usuario.nombre = reader.GetString(1);
                usuario.apellido = reader.GetString(2);
                usuario.username = reader.GetString(3);
                usuario.password = reader.GetString(4);
            }
        }
        return usuario;
    }
    //Ingresamos el dato de la calificacion del artista cuando aun no 
    //exita esta calificacion en la tabla, es decir cuando el usuario
    //no ha calificado a ese artista
    public void CalificarArtista(int idU, int idA, int calificacion)
    {
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "INSERT INTO tblArtistasCalificados values("+calificacion+","+idU+","+idA+")";
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
    //Ingresamos el dato de la calificacion del disco cuando aun no 
    //exita esta calificacion en la tabla, es decir cuando el usuario
    //no ha calificado a ese disco
    public void CalificarDisco(int idU, int idD, int calificacion)
    {
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "INSERT INTO tblDiscosCalificados values(" + idD + "," + idU + "," + calificacion + ")";
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
    //Cuando ya se a logeado el usuario para el paso de formularios se 
    //trabajara con el id de este, por lo que en cada formulario donde se requiera
    //la informacion del usuario se realizara una busqueda del mismo
    //en base a su id
    public Usuario BuscarUsuario(int idU) {

        Usuario usuario = new Usuario();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT * FROM TblUsuarios where idU = " + idU;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        //"' and " + "Contrasenia ='" + contrasenia+
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                usuario.idUsuario = reader.GetInt32(0);
                usuario.nombre = reader.GetString(1);
                usuario.apellido = reader.GetString(2);
                usuario.username = reader.GetString(3);
                usuario.password = reader.GetString(4);
            }
        }
        return usuario;
    }
    //Si ya existe una calificacion dada por el usuario a un artista
    //lo que haremos sera buscar esa calificacion dada por el usuario
    //mediante el id del usuario y artista calificado.
    //Para despues usar esa calificacion al actualizar los 
    //datos en la Tabla artista (Sumatoria y puntuacion), es 
    //decir la puntuacion no se alterara y la sumatoria si.
    public int BuscarCalArt(int idU, int idA)
    {
        int idCA=0;
         SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT * FROM TblArtistasCalificados where IdU =" + idU + " and " + "IdA =" + idA ;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        //"' and " + "Contrasenia ='" + contrasenia+
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                idCA = reader.GetInt32(1);
            }
        }
        return idCA;
    }
    //Si ya existe una calificacion dada por el usuario a un disco
    //lo que haremos sera buscar esa calificacion dada por el usuario
    //mediante el id del usuario y disco calificado.
    //Para despues usar esa calificacion al actualizar los 
    //datos en la Tabla disco (Sumatoria y puntuacion), es 
    //decir la puntuacion no se alterara y la sumatoria si.
    public int BuscarCalDis(int idU, int idD) {
        int idCD = 0;
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT * FROM TblDiscosCalificados where IdU =" + idU + " and " + "IdD =" + idD;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        //"' and " + "Contrasenia ='" + contrasenia+
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                idCD = reader.GetInt32(3);
            }
        }
        return idCD;
    }
    //Con la nueva calificacion dada por el artista lo que haremos sera
    //actualizar la tabla CalificacionArtista con la nueva calificacion
    public void AcCalifArtista(int idU, int idA, int calificacion) {
        int idCA = 0;
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT * FROM TblArtistasCalificados where IdU =" + idU + " and " + "IdA =" + idA;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        //"' and " + "Contrasenia ='" + contrasenia+
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                idCA = reader.GetInt32(0);
            }
        }
        reader.Close();
        consulta = "UPDATE TblArtistasCalificados SET Puntuación=" + calificacion + "where IdAC=" + idCA;
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
    //Con la nueva calificacion dada por el artista lo que haremos sera
    //actualizar la tabla CalificacionDisco con la nueva calificacion
    public void AcCalifDisco(int idU, int idD, int calificacion) {
        int idCA = 0;
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT * FROM TblDiscosCalificados where IdU =" + idU + " and " + "IdD =" + idD;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        //"' and " + "Contrasenia ='" + contrasenia+
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                idCA = reader.GetInt32(0);
            }
        }
        reader.Close();
        consulta = "UPDATE TblDiscosCalificados SET Puntuación=" + calificacion + "where IdDC=" + idCA;
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
    //Cada vez que se agrege una calificacion o se actualice una calificacion 
    //Actualizaremos la Tabla Artista o Tabla Disco con un nuevo valor en sumatorio
    //y un nevo valor en puntuacion,
    public void AcArtista(int idA,int sumato,int pun)
    {
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "UPDATE TblArtista SET Sumatoria="+sumato+"where IdA =" + idA;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
        consulta = "UPDATE TblArtista SET Puntuación=" + pun + "where IdA =" + idA;
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
    public void AcDisco(int idD, int sumato, int pun)
    {
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "UPDATE TblDisco SET Sumatoria=" + sumato + "where IdD =" + idD;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
        consulta = "UPDATE TblDisco SET Puntuación=" + pun + "where IdD =" + idD;
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
}


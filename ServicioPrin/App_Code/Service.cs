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
    private static string cadenaConexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\work\Desktop\ProyectoBim2\ServicioPrin\App_Code\Database1.mdf;Integrated Security=True;Connect Timeout=30";
    /*Creamos un metodo el cual sera usado para establecer la
     conexion estemetodo sera de tipo SqlConnection ya que
     nos retornara el objeto creado al establecer conexion*/
    public static SqlConnection obtenerConexion()
    {
        /*Se intancia el objeto de conexión a la base de datos */
        SqlConnection miconexion = new SqlConnection(cadenaConexion);
        //Abrimos la conexión
        miconexion.Open();
        return miconexion;
    }

    //Definimos lo que haran cada contrato declarado en 7l
    //IService.
    //Nos delvovera una lista de objetos tipo Artista
    //Pra mostrarlos en el formulario principal
    public List<Artista> ListarArt()
    {
        List<Artista> listaArtista = new List<Artista>();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblArtista";
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Obtenemos los datos cada fila y lo almacenamos
                //en los atributos del artista
                Artista artista = new Artista();
                artista.idArtista = reader.GetInt32(0);
                artista.nombre = reader.GetString(1);
                artista.apellido = reader.GetString(2);
                artista.nombreArtistico = reader.GetString(3);
                artista.nacionalidad = reader.GetString(4);
                artista.sumatoria = reader.GetInt32(5);
                artista.puntuacion = reader.GetInt32(6);
                //Cada artista obtenido lo agregamos a la 
                //lista de artistas que retornara este metodo
                listaArtista.Add(artista);
            }
        }
        return listaArtista;
    }

    //Nos delvovera una lista de objetos tipo ADisco
    //Pra mostrarlos en el formulario principal
    public List<Disco> ListarDisco()
    {
        List<Disco> listaDisco = new List<Disco>();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblDisco";
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Obtenemos los datos cada fila y lo almacenamos
                //en los atributos del artista
                Disco disco = new Disco();
                disco.idDisco = reader.GetInt32(0);
                disco.idArtista = reader.GetInt32(1);
                disco.nombreDisco = reader.GetString(2);
                disco.anoPublicacion = reader.GetInt32(3);
                disco.sumatoria = reader.GetInt32(4);
                disco.puntuacion = reader.GetInt32(5);
                listaDisco.Add(disco);
            }
        }
        return listaDisco;
    }
    //Nos delvovera una lista de objetos tipo Cancion
    //Pra mostrarlos en el formulario de un disco en particular
    public List<Cancion> ListarCan(int idD)
    {
        List<Cancion> listaCanciones = new List<Cancion>();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblCanciones where IdD=" + idD;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Obtenemos los datos cada fila y lo almacenamos
                //en los atributos del artista
                Cancion cancion = new Cancion();
                cancion.idCancion = reader.GetInt32(0);
                cancion.idDisco = reader.GetInt32(1);
                cancion.nombreCancion = reader.GetString(2);
                listaCanciones.Add(cancion);
            }
        }
        return listaCanciones;
    }
    //Como el id es unico en la consulta retornara unicamente un 
    //artista
    public Artista BuscarArt(int idA)
    {
        Artista artista = new Artista();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblArtista where IdA=" + idA;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Obtenemos los datos cada fila y lo almacenamos
                //en los atributos del artista
                artista.idArtista = reader.GetInt32(0);
                artista.nombre = reader.GetString(1);
                artista.apellido = reader.GetString(2);
                artista.nombreArtistico = reader.GetString(3);
                artista.nacionalidad = reader.GetString(4);
                artista.sumatoria = reader.GetInt32(5);
                artista.puntuacion = reader.GetInt32(6);
            }
        }

        return artista;
    }
    //Como el id es unico en la consulta retornara unicamente un 
    //disco
    public Disco BuscarDisco(int idD)
    {

        Disco disco = new Disco();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblDisco where IdD=" + idD;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Obtenemos los datos cada fila y lo almacenamos
                //en los atributos del artista
                disco.idDisco = reader.GetInt32(0);
                disco.idArtista = reader.GetInt32(1);
                disco.nombreDisco = reader.GetString(2);
                disco.anoPublicacion = reader.GetInt32(3);
                disco.sumatoria = reader.GetInt32(4);
                disco.puntuacion = reader.GetInt32(5);
            }
        }
        return disco;
    }
    //Buscamos la cancion para poder tomar su valor de nombre
    //y poder actualizarlo
    public Cancion BuscarCancion(int idC) {

        Cancion cancion = new Cancion();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblCanciones where IdC=" + idC;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                cancion.idCancion = reader.GetInt32(0);
                cancion.idDisco = reader.GetInt32(1);
                cancion.nombreCancion = reader.GetString(2);
            }
        }
        return cancion;
    }
    //Nos delvovera una lista de objetos tipo Disco
    //Pra mostrarlos en el formulario Artista
    //donde se mostraran unicamente los discos 
    //correspondientes a ese artista
    public List<Disco> BuscarDiscoArt(int idA)
    {
        List<Disco> listaDiscoporArt = new List<Disco>();
        //Abrimos la conexion a la base de datos  
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblDisco where IdA=" + idA;

        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();

        //Para que lea todas las filas usamos el HasRows
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Obtenemos los datos cada fila y lo almacenamos
                //en los atributos del artista
                Disco dis = new Disco();
                dis.idDisco = reader.GetInt32(0);
                dis.idArtista = reader.GetInt32(1);
                dis.nombreDisco = reader.GetString(2);
                dis.anoPublicacion = reader.GetInt32(3);
                dis.sumatoria = reader.GetInt32(4);
                dis.puntuacion = reader.GetInt32(5);
                listaDiscoporArt.Add(dis);

            }
        }
        return listaDiscoporArt;
    }
    //Nos delvovera una lista de objetos tipo Disco
    //Pra mostrarlos en el formulario principal
    //Los disco compatibles al parametro de busquesa
    public List<Disco> BusquedaDis(string disco)
    {
        List<Disco> listaDiscoporArt = new List<Disco>();
        //Abrimos la conexion a la base de datos  
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblDisco where NombreDisco LIKE '" + disco + "%'";

        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();

        //Para que lea todas las filas usamos el HasRows
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Obtenemos los datos cada fila y lo almacenamos
                //en los atributos del artista
                Disco dis = new Disco();
                dis.idDisco = reader.GetInt32(0);
                dis.idArtista = reader.GetInt32(1);
                dis.nombreDisco = reader.GetString(2);
                dis.anoPublicacion = reader.GetInt32(3);
                dis.sumatoria = reader.GetInt32(4);
                dis.puntuacion = reader.GetInt32(5);
                listaDiscoporArt.Add(dis);

            }
        }
        return listaDiscoporArt;
    }
    //Nos delvovera una lista de objetos tipo Artista
    //Pra mostrarlos en el formulario principal
    //Los artistas compatibles al parametro de busqueda
    public List<Artista> BusquedaArt(string artis)
    {
        List<Artista> listaArtista = new List<Artista>();
        //Abrimos la conexion a la base de datos 
        //para poder realizar la consulta
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "SELECT* FROM TblArtista WHERE NombreArtistico LIKE '" + artis + "%'";
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        SqlDataReader reader = comando.ExecuteReader();
        //Para que lea todas las filas usamos el HasRows
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                //Obtenemos los datos cada fila y lo almacenamos
                //en los atributos del artista
                Artista artista = new Artista();
                artista.idArtista = reader.GetInt32(0);
                artista.nombre = reader.GetString(1);
                artista.apellido = reader.GetString(2);
                artista.nombreArtistico = reader.GetString(3);
                artista.nacionalidad = reader.GetString(4);
                artista.sumatoria = reader.GetInt32(5);
                listaArtista.Add(artista);
            }
        }
        return listaArtista;
    }
    //Estos metodos son los que se usaran para actualizar los datos 
    //tanto del artista, del disco y de las canciones
    public void ActualizarArt(int idA, string nombre, string apellido, string Nartis, string nacion)
    {
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "UPDATE TblArtista SET Nombre='" +nombre +"',Apellido='"+apellido+"',NombreArtistico='"+Nartis+"',Nacionalidad='"+nacion+"' where IdA =" + idA;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
    public void ActualizarDis(int idC, string nombreD, int anio) {
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "UPDATE TblDisco SET NombreDisco='" + nombreD + "',Aniopublicacion=" + anio  + " where IdD =" + idC;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
    public void ActualizarCan(int idC, string nombreD)
    {
        SqlConnection conexion = obtenerConexion();
        //Consulta que obtendra tods los elementos de la tabla
        string consulta = "UPDATE TblCanciones SET Nombre='" + nombreD + "' where IdC =" + idC;
        //Ejecucion de la consulta en la base de datos
        SqlCommand comando = new SqlCommand(consulta, conexion);
        comando = new SqlCommand(consulta, conexion);
        comando.ExecuteNonQuery();
    }
}

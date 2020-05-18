//*****************************************************************************************************************
//Codigo02
//Victoria Elizabeth Camacho Muncha
//Jennifer Samanta Minayo Yujato
//Fecha de entrega: 05/08/2018

//                  *****************************************************************************************
//                                      TEMA: Proyecto 02
//                  *****************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Servicioprincipal;
using Serviciousuario;
public partial class FrmDisco : System.Web.UI.Page
{
    //Al tener dos referencias de servicio definimos 2 clientes
    //uno para cada servicio
    Servicioprincipal.ServiceClient cliente = new Servicioprincipal.ServiceClient();
    Serviciousuario.ServiceClient cliente2 = new Serviciousuario.ServiceClient();
    //Variable para almacenar la calificacion que se dara al disco
    int cal = 0;
    //Para guardar el id del disco, se extrae como string del formulario anterio
    string id;
    //Para guardar Id de usuario, se extrae como string del formulario anterior
    string idU;
    //Si ya existe una calificacion se almacenara ese valor en esta variables
    int calEncon;
    //Se almacenara el parametro puntuacion del disco para despues 
    //usarlo al actualizar el disco
    int pun;
    //Guardara el id del disco
    int idDis;
    //Almacenaremos el disco
    Disco disco = new Disco();
    //Almacenaremos el artista 
    Artista artista = new Artista();
    //Aqui almacenaremos la lista de canciones existente por disco en la base de datos
    //para mostrarla en el lstDiscos
    List<Cancion> cancions = new List<Cancion>();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Obtenemos las variables id de artista y usuario del formulario anterior
        id = (string)Session["IdD"];
        idU = (string)Session["IdUd"];
        //Buscamos al usuario en base al id que recibio del formulario anterior
        if(idU == "0")
        {
            btn1.Visible = false;
            btn2.Visible = false;
            btn3.Visible = false;
            btn4.Visible = false;
            btn5.Visible = false;
            btnEditar.Visible = false;
        }
        Usuario usuario = cliente2.BuscarUsuario(Convert.ToInt32(idU));
        //mostramos datos del usuario
        lblNombreU.Text = usuario.nombre;
        lblApellidoU.Text = usuario.apellido;
        idDis = Convert.ToInt32(id);
        //buscamos el disco en base al id que recibio del formulario anterior
        disco = cliente.BuscarDisco(idDis);
        //Mostramos datos del disco
        lblDisco.Text = disco.nombreDisco;
        lblanio.Text = disco.anoPublicacion.ToString();
        //buscamos el artista en base al id que recibio del formulario anterior
        artista = cliente.BuscarArt(disco.idArtista);
        //Mostramos datos del artista
        lblNombre.Text = artista.nombre;
        lblApellido.Text = artista.apellido;
        lblNart.Text = artista.nombreArtistico;
        lblNacion.Text = artista.nacionalidad;
        //Buscamos la calificacion del disco
        calEncon = cliente2.BuscarCalDis(Convert.ToInt32(idU), idDis);
        //Si no existe limpiamos los botones que se usaran para la calificacion
        if (calEncon == 0)
        {
            limpiarbotones();
        }
        //Si si existe dependiendo de cuanto sea la calificacion mostramos los 
        //botones en verde
        else if (calEncon == 1)
        {
            limpiarbotones();
            btn1.BackColor = Color.Green;
        }
        else if (calEncon == 2)
        {
            limpiarbotones();
            btn1.BackColor = Color.Green;
            btn2.BackColor = Color.Green;
        }
        else if (calEncon == 3)
        {
            limpiarbotones();
            btn1.BackColor = Color.Green;
            btn2.BackColor = Color.Green;
            btn3.BackColor = Color.Green;
        }
        else if (calEncon == 4)
        {
            limpiarbotones();
            btn1.BackColor = Color.Green;
            btn2.BackColor = Color.Green;
            btn3.BackColor = Color.Green;
            btn4.BackColor = Color.Green;
        }
        else
        {
            limpiarbotones();
            btn1.BackColor = Color.Green;
            btn2.BackColor = Color.Green;
            btn3.BackColor = Color.Green;
            btn4.BackColor = Color.Green;
            btn5.BackColor = Color.Green;
        }
        //Para la calificacion del disco si aun nadie lo 
        //a calificado inmediatamente pponemos cero y si si 
        //lo han calificado realizamos la division y mostramos
        //el resultado
        if (disco.puntuacion == 0)
        {
            lblCalificacion.Text = "0";
        }
        else
        {
            lblCalificacion.Text = Convert.ToString(disco.sumatoria / disco.puntuacion);
        }
        lstCanciones.Items.Clear();
        //Buscamos las canciones existentes para ese disco y los mostramos
        cancions = cliente.ListarCan(idDis);
        foreach (Cancion item in cancions)
        {
            lstCanciones.Items.Add(item.nombreCancion);
        }
    }
    //La calificacion la hacemos en base a los botones donde
    //asignaremos la calificacion que se le dara en el atributo cal
    //y mandaremos a actualizar las correspondientes tablas en base a 
    //los metodos ActualizarCal y Actualizar
    protected void btn1_Click(object sender, EventArgs e)
    {
        limpiarbotones();
        cal = 1;
        ActualizarCal();
        btn1.BackColor = Color.Green;
        Actualizar();
    }
    protected void btn2_Click(object sender, EventArgs e)
    {
        limpiarbotones();
        cal = 2;
        ActualizarCal();
        btn1.BackColor = Color.Green;
        btn2.BackColor = Color.Green;
        Actualizar();
    }
    protected void btn3_Click(object sender, EventArgs e)
    {
        limpiarbotones();
        cal = 3;
        ActualizarCal();
        btn1.BackColor = Color.Green;
        btn2.BackColor = Color.Green;
        btn3.BackColor = Color.Green;
        Actualizar();
    }
    protected void btn4_Click(object sender, EventArgs e)
    {
        limpiarbotones();
        cal = 4;
        ActualizarCal();
        btn1.BackColor = Color.Green;
        btn2.BackColor = Color.Green;
        btn3.BackColor = Color.Green;
        btn4.BackColor = Color.Green;
        Actualizar();
    }
    protected void btn5_Click(object sender, EventArgs e)
    {
        limpiarbotones();
        cal = 5;
        ActualizarCal();
        btn1.BackColor = Color.Green;
        btn2.BackColor = Color.Green;
        btn3.BackColor = Color.Green;
        btn4.BackColor = Color.Green;
        btn5.BackColor = Color.Green;
        Actualizar();
    }
    public void limpiarbotones()
    {
        btn1.BackColor = Color.Yellow;
        btn2.BackColor = Color.Yellow;
        btn3.BackColor = Color.Yellow;
        btn4.BackColor = Color.Yellow;
        btn5.BackColor = Color.Yellow;
    }
    //Para actualizar la calificacion en la base de datos
    public void ActualizarCal()
    {
        //Si aun no se ha realizado la calificacion 
        if (calEncon == 0)
        {
           
            //Ingresamos la calificacion en la tabla mediante el contrato 
            //del servicio
            cliente2.CalificarDisco(Convert.ToInt32(idU), idDis, cal);
            //Como este usuario aun no ha echo su calificacion sumaremos 1 al 
            //parametro puntuacion del disco y sumaremos la nueva calificacion
            //al parametro sumatorio del disco
            pun = 1;
            int suma = disco.sumatoria + cal;
            int suma1 = disco.puntuacion + pun;
            //Actualizamos los datos del artista
            cliente2.AcDisco(Convert.ToInt32(id), suma, suma1);

        }
        else
        {
            //Buscamos la calificacion del disco que le dio este usuario
            //a este disco
            int est = cliente2.BuscarCalDis(Convert.ToInt32(idU), idDis);
            pun = 0;
            //Actualizamos la nueva calificacion
            cliente2.AcCalifDisco(Convert.ToInt32(idU), Convert.ToInt32(id), cal);
            //Como ya estaba calificado por el usuario anteriormente
            //restamos la calificaion anterior y sumamos la nueva
            //para actualizar el sumatorio de este artista, ademas
            //la puntuacion que corresponde al numero de usuarios
            //que lo calificaron se mantiene igual
            int suma = disco.sumatoria - est + cal;
            int suma1 = disco.puntuacion + pun;
            //Actualizamos la informacion del artista
            cliente2.AcDisco(Convert.ToInt32(id), suma, suma1);
        }
    }
    //Actualizar la calificacion en el form actual
    public void Actualizar()
    {
        // Para que no se agregen nuevamente las lista sobre la lista
        //del list box limpiamos y volvemos a agregaar
        lstCanciones.Items.Clear();
        foreach (Cancion item in cancions)
        {
            lstCanciones.Items.Add(item.nombreCancion);
        }
        //Con la nueva informacion del disco obtenemos la nueva calificacion
        disco = cliente.BuscarDisco(idDis);
        lblCalificacion.Text = Convert.ToString(disco.sumatoria / disco.puntuacion);
    }
    //Regresa al formulario principal, enviamos id de usuario
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Session["IdUa"] = idU;
        Response.Redirect("Principal.aspx");
    }
    //Regresa al usuario login para ingresar otro usuario
    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmUsuario.aspx");
    }
    //Abre el formulario que permitira editar informacion del disco
    protected void btnEditar_Click(object sender, EventArgs e)
    {
        Session["IdU1"] = idU;
        Session["IdA1"] = disco.idArtista.ToString();
        Session["IdD1"] = disco.idDisco.ToString();
        Response.Redirect("Actualizar.aspx");
    }
}
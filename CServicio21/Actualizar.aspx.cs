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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Servicioprincipal;
using Serviciousuario;

public partial class Actualizar : System.Web.UI.Page
{
    //Variable donde almacenara el valor de id de artista recibido del form
    //anterior
    string idA;
    //Variable donde almacenara el valor de id de Disco recibido del form
    //anterior
    string idD;
    //Variable donde almacenara el valor de id de ausuario recibido del form
    //anterior
    string idU;
    //Variable donde almacenara el valor de id de la cancion para usarla al editar
    string idC;
    //Para mostrar la calificacion del disco
    string califD;
    //usaremos para guardar el artista obtenido en base a la busqueda con 
    //el id de artista recibido del form anterior y mostrarlo sus datos a ser
    //editados
    Artista artista = new Artista();
    //usaremos para guardar el disco obtenido en base a la busqueda con 
    //el id de disco recibido del form anterior y mostrarlo sus datos a ser
    //editados
    Disco disco = new Disco();
    //Almacenaremos la cancion que queremos editar en este objeto.
    Cancion cancion = new Cancion();
    //Lista de canciones donde se almacenara la busqueda de los discos
    //En base al artista
    List<Disco> discos = new List<Disco>();
    //Lista de canciones donde se almacenara la busqueda de las canciones
    //En base al disco
    List<Cancion> canciones = new List<Cancion>();
    //Cramos dos clientes ya que se usaran dos servicios y cada cliente
    //correspondera a un servicio
    Servicioprincipal.ServiceClient cliente = new Servicioprincipal.ServiceClient();
    Serviciousuario.ServiceClient cliente2 = new Serviciousuario.ServiceClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblsaber.Visible = false;
        lblCancion.Visible = false;
        //Variables obtenidas del form anterior
        idU = (string)Session["IdU1"];
        idA = (string)Session["IdA1"];
        idD = (string)Session["IdD1"];
        //Cuando ingresa por primera vez extrae los datos en base 
        //a los id de artistas y discos ingresados, cuando se recarge 
        //mantendra los datos editados ppor el usuario
        if (lblsaber.Text != "nuevo") {
            artista = cliente.BuscarArt(Convert.ToInt32(idA));
            txtApellidoArt.Text = artista.apellido;
            txtNombreArt.Text = artista.nombre;
            txtApellidoArt.Enabled = false;
            txtNombreArt.Enabled = false;
            txtNombreArtist.Text = artista.nombreArtistico;
            txtNac.Text = artista.nacionalidad;
            disco = cliente.BuscarDisco(Convert.ToInt32(idD));
            txtDisco.Text = disco.nombreDisco;
            txtAnio.Text = Convert.ToString(disco.anoPublicacion);
            if (artista.puntuacion == 0)
            {
                lblCalificacion.Text = "0";
            }
            else
            {
                lblCalificacion.Text = Convert.ToString(artista.sumatoria / artista.puntuacion);
            }
            if (disco.puntuacion == 0)
            {
                lblCalificacion0.Text = "0";
            }
            else
            {
                lblCalificacion0.Text = Convert.ToString(disco.sumatoria / disco.puntuacion);
            }
        }
        //Buscamos el usuario en base a su id
        Usuario usuario = cliente2.BuscarUsuario(Convert.ToInt32(idU));
        //Mostramos datos del usuario
        lblApellidoU.Text = usuario.apellido;
        lblNombreU.Text = usuario.nombre;
        //Buscar los discos de ese artista
        discos = cliente.BuscarDiscoArt(Convert.ToInt32(idA));
        //Para mostrar las calificaciones consideramos el no dividir para 0
        //tanto para los discos como para las canciones
        if (lstDiscos1.Items.Count == 0)
        {
            foreach (Disco item in discos)
            {
                if (item.puntuacion == 0)
                {
                    califD = "0";
                }
                else
                {
                    califD = Convert.ToString(item.sumatoria / item.puntuacion);
                }
                lstDiscos1.Items.Add(item.idDisco + ":  " + item.nombreDisco );
            }

        }
        //Buscamos las canciones correspondientes a un disco
        canciones = cliente.ListarCan(Convert.ToInt32(idD));
        if (lstCanciones.Items.Count == 0)
        {
           
            foreach (Cancion item in canciones)
            {
                lstCanciones.Items.Add(item.idCancion+":"+item.nombreCancion);
            }
        }
        Panel2.Enabled = false;
        //si solo se desea editar disco o artista
        if (idD == "0")
        {
            Panel2.Enabled = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
        }
        lblsaber.Text = "nuevo";
    }
    //Seleccionamos la cancion desde el disco y cargamos el dato de 
    //nombre de la cancion que se editara
    protected void btnActualizarC_Click(object sender, EventArgs e)
    {
        try {
            idC = lstCanciones.SelectedItem.ToString();
            string[] idC1 = idC.Split(':');
            cancion = cliente.BuscarCancion(Convert.ToInt32(idC1[0]));
            txtNombreCan.Text = cancion.nombreCancion;
            lblCancion.Text = idC1[0];
        }catch(Exception er){ }
        
    }
   //Vuelve al form usuario para logearse
    protected void btnSalir_Click(object sender, EventArgs e)
    {

        Response.Redirect("FrmUsuario.aspx");
    }
    //Regresa al formulario principal
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Session["IdU"] = idU;
        Response.Redirect("Principal.aspx");
    }
    //Actualiza los nuevos datos del artista
    protected void btnGuardarA_Click(object sender, EventArgs e)
    {
        lblmensaje.Text = "Edicion Realizada";
        lblsaber.Text = "nuevo";
        cliente.ActualizarArt(Convert.ToInt32(idA),txtNombreArt.Text,txtApellidoArt.Text,txtNombreArtist.Text,txtNac.Text);
        btnGuardarA.Enabled = false;
    }
    //Actualiza los nuevos datos del artista
    protected void btnGuardarD_Click(object sender, EventArgs e)
    {
        try
        {
            cliente.ActualizarDis(Convert.ToInt32(idD), txtDisco.Text, Convert.ToInt32(txtAnio.Text));
            lblsaber.Text = "nuevo";
            lblmensaje1.Text = "Edicion Realizada";
            btnGuardarD.Enabled = false;
        }
        catch (Exception er) { }
    }
    //Actualiza los nuevos datos de la cancion
    protected void btnGuardarC_Click(object sender, EventArgs e)
    {
        lblsaber.Text = "nuevo";
        cliente.ActualizarCan(Convert.ToInt32(lblCancion.Text), txtNombreCan.Text);
        lblmensaje2.Text = "Edicion Realizada";
        btnActualizarC.Enabled = false;
        btnGuardarC.Enabled = false;
    }
}
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
using Servicioprincipal;
using Serviciousuario;
using System.Drawing;

public partial class FrmArtista : System.Web.UI.Page
{

    //Al tener dos referencias de servicio definimos 2 clientes
    //uno para cada servicio
    Servicioprincipal.ServiceClient cliente = new Servicioprincipal.ServiceClient();
    Serviciousuario.ServiceClient cliente2 = new Serviciousuario.ServiceClient();
    //Para guardar Id de usuario, se extrae como string del formulario anterior
    string idU;
    //Para guardar el id del artista, se extrae como string del formulario anterior
    string id;
    //Variable para almacenar la calificacion que se dara al artista
    int cal = 0;
    //Si ya existe una calificacion se almacenara ese valor en esta variables
    int calEncon;
    //Guardara el id del artista
    int idart;
    //Aqui almacenaremos la lista de disco existente por usuario en la base de datos
    //para mostrarla en el lstDiscos
    List<Disco> discos = new List<Disco>();
    //Almacenaremos el artista 
    Artista artista = new Artista();
    //Se almacenara el parametro puntuacion del artista para despues 
    //usarlo al actualizar el artista
    int pun = 0;
    //Para mostrar la calificacion del disco al listar
    string califD;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Obtenemos las variables id de artista y usuario del formulario anterior
        id = (string)Session["IdA"];
        idU = (string)Session["IdUa"];
        //Buscamos al usuario en base al id que recibio del formulario anterior
        if(idU=="0")
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
        lblApellidoU.Text = usuario.apellido;
        lblNombreU.Text = usuario.nombre;
        idart = Convert.ToInt32(id);
        //buscamos el artista en base al id que recibio del formulario anterior
        artista = cliente.BuscarArt(idart);
        //Mostramos datos del artista
        lblNombre.Text = artista.nombre;
        lblApellido.Text = artista.apellido;
        lblNart.Text = artista.nombreArtistico;
        lblNacion.Text = artista.nacionalidad;
        //Buscamos la calificacion del artista
        calEncon = cliente2.BuscarCalArt(Convert.ToInt32(idU),idart);
        //Si no existe limpiamos los botones que se usaran para la calificacion
        if (calEncon == 0)
        {
            limpiarbotones();
        }
        //Si si existe dependiendo de cuanto sea la calificacion mostramos los botones en verde
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
        //Para la calificacion del artista si aun nadie lo 
        //a calificado inmediatamente pponemos cero y si si 
        //lo han calificado realizamos la division y mostramos
        //el resultado
        if (artista.puntuacion == 0)
        {
            lblCalificacion.Text = "0";
        }
        else
        {
            lblCalificacion.Text = Convert.ToString(artista.sumatoria / artista.puntuacion);
        }
        //Buscamos los disco existentes para ese artista y los mostramos
        //De igual manera tenemos la consideracion de si aun no ha sido calificado
        discos = cliente.BuscarDiscoArt(idart);
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
                lstDiscos1.Items.Add(item.idDisco + ":  " + item.nombreDisco + "     " + califD);
            }

        }

    }
    //Abrir el formulario del disco que se ha seleccionado del artista
    protected void btnDisco_Click(object sender, EventArgs e)
    {
        try {
            //Se selecciona un disco y se envia esa informacion
            //al siguiente formulario donde se mostrara la informacion 
            //de este disco, ademas se enviara el id del usuari para que 
            //interactue con el disco
            string disco = lstDiscos1.SelectedItem.ToString();
            string[] idD = disco.Split(':');
            Session["IdUd"] = idU;
            Session["IdD"] = idD[0];
            Response.Redirect("FrmDisco.aspx");
        } catch(Exception er) {

        }
        
    }
    //La calificacion la hacemos en base a los botones donde
    //asignaremos la calificacion que se le dara en el atributo cal
    //y mandaremos a actualizar las correspondientes tablas en base a 
    //los metodos ActualizarCal y Actualizar
    protected void btn1_Click(object sender, EventArgs e)
    {
        limpiarbotones();
        cal =1;
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
    //Actualizar calificacion en la base de datos
    public void ActualizarCal()
    {
        //Si aun no se ha realizado la calificacion 
        if (calEncon == 0)
        {
            //Ingresamos la calificacion en la tabla mediante el contrato 
            //del servicio
            cliente2.CalificarArtista(Convert.ToInt32(idU), Convert.ToInt32(id), cal);
            //Como este usuario aun no ha echo su calificacion sumaremos 1 al 
            //parametro puntuacion del artista y sumaremos la nueva calificacion
            //al parametro sumatorio del Artista
            pun = 1;
            int suma = artista.sumatoria + cal;
            int suma1 = artista.puntuacion + pun;
            //Actualizamos los datos del artista
            cliente2.AcArtista(Convert.ToInt32(id), suma, suma1);
        }
        else
        {
            //Buscamos la calificacion del artista que le dio este usuario
            //a este artista
            int est = cliente2.BuscarCalArt(Convert.ToInt32(idU), idart);
            pun = 0;
            //Actualizamos la nueva calificacion
            cliente2.AcCalifArtista(Convert.ToInt32(idU), Convert.ToInt32(id), cal);
            //Como ya estaba calificado por el usuario anteriormente
            //restamos la calificaion anterior y sumamos la nueva
            //para actualizar el sumatorio de este artista, ademas
            //la puntuacion que corresponde al numero de usuarios
            //que lo calificaron se mantiene igual
            int suma = artista.sumatoria - est + cal;
            int suma1 = artista.puntuacion + pun;
            //Actualizamos la informacion del artista
            cliente2.AcArtista(Convert.ToInt32(id), suma, suma1);
        }
    }
    //Actualizar la calificacion global del artista en este formulario
    public void Actualizar()
    {
        //Para que no se agregen nuevamente las lista sobre la lista
        //del list box limpiamos y volvemos a agregaar
        lstDiscos1.Items.Clear();
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
            lstDiscos1.Items.Add(item.idDisco + ":  " + item.nombreDisco + "     " + califD);
        }
        //Con la nueva informacion del artista obtenemos la nueva calificacion
        artista = cliente.BuscarArt(idart);
        lblCalificacion.Text = Convert.ToString(artista.sumatoria / artista.puntuacion);
    }
    protected void lstDiscos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //Regresa al formulario de usuario para logearse nuevamente
    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmUsuario.aspx");
    }
    //Regresa al formulario principal, enviamos id de usuario
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Session["IdU"] = idU;
        Response.Redirect("Principal.aspx");
    }
    //Ingesar a formulario para editar los datos del artista
    protected void btnEditar_Click(object sender, EventArgs e)
    {
        Session["IdU1"] = idU;
        Session["IdA1"] = artista.idArtista.ToString();
        Session["IdD1"] = "0";
        Response.Redirect("Actualizar.aspx");
    }
}
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
public partial class Principal : System.Web.UI.Page
{
    //Al tener dos referencias de servicio definimos 2 clientes
    //uno para cada servicio
    Servicioprincipal.ServiceClient cliente = new Servicioprincipal.ServiceClient();
    Serviciousuario.ServiceClient cliente2 = new Serviciousuario.ServiceClient();
    //Aqui almacenaremos la lista de artistas existente en la base de datos
    //para mostrarla en el lstArtistas
    List<Artista> listart = new List<Artista>();
    //Aqui almacenaremos la lista de disco existente en la base de datos
    //para mostrarla en el lstDiscos
    List<Disco> listdiscos = new List<Disco>();
    //Correspondera al usuario que se logeo en la aplicacion
    Usuario usuario = new Usuario();
    //Almacenara el id de usuario que enviaremos al siguiente formulario
    string idU;
    //Para las calificaciones tanto del Artista como
    //del disco que se mostraran con cada artista o disco
    //al momento de listarlos
    string califA;
    string califD;
    int car;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Recuperamos el valor enviado desde el formulario anterior
        idU = (string)Session["IdU"];
        //Buscamos el usuario logeado
        usuario = cliente2.BuscarUsuario(Convert.ToInt32(idU));
        //Mostramos los datos del usuario logeado
        lblApellidoU.Text = usuario.apellido;
        lblNombreU.Text = usuario.nombre;
        //Obtenemos los artistas de la base de datos
        listart = cliente.ListarArt();
        //Obtenemos los discos de la base de datos
        listdiscos = cliente.ListarDisco();
        //Para buscar bajo el criterio de disco o artista
        //usamos un dropdownlist
        car = ddlBuscar.Items.Count;
        if (car == 2)
        {
        }
        else
        {
            ddlBuscar.Items.Add("Artista");
            ddlBuscar.Items.Add("Disco");
        }
        if(lstArtistas.Items.Count== 0)
        {
            //mostramos los artistas en el lstArtistas
            foreach (Artista item in listart)
            {
                //Si aun nadie a calificado ese artista quiere decir
                //que su atributo puntuacion esta en cero directamente
                //mostramos cero de calificacion al artista ya que sino 
                //generaria un error de division para cero
                if (item.puntuacion == 0)
                {
                    califA = "0";
                }
                else
                {
                    //realizamos la division de la sumatoria para el numero
                    //de veces que ha sido calificado y obtenemos
                    //la calificacion global
                    califA = Convert.ToString(item.sumatoria / item.puntuacion);
                }
                //Mostramos
                lstArtistas.Items.Add(item.idArtista + ":   " + item.nombreArtistico + "     " + califA);

            }
        }
        if (lstDiscos.Items.Count == 0) {
            //mostramos los discos en el lstDiscos
            foreach (Disco item in listdiscos)
            {
                //Si aun nadie a calificado ese disco quiere decir
                //que su atributo puntuacion esta en cero directamente
                //mostramos cero de calificacion al disco ya que sino 
                //generaria un error de division para cero
                if (item.puntuacion == 0)
                {
                    califD = "0";
                }
                else
                {

                    //realizamos la division de la sumatoria para el numero
                    //de veces que ha sido calificado y obtenemos
                    //la calificacion global
                    califD = Convert.ToString(item.sumatoria / item.puntuacion);
                }
                //mostramos
                lstDiscos.Items.Add(item.idDisco + ":  " + item.nombreDisco + "     " + califD);
            }
        }
        lblSaber.Visible = false;
    }

    protected void lstArtistas_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        
    }
    //Abrir el formulario del artista seleccionado del lstArtistas
    protected void btnArtistas_Click(object sender, EventArgs e)
    {
        try
        {
            //Se selecciona un artista y se lo envia al siguiente formulario
            //para mostrar la informacion de ese artista, ademas enviamos el 
            //id de usuario para que pueda interactuar con el artista
            string artista = lstArtistas.SelectedItem.ToString();
            string[] idA = artista.Split(':');
            Session["IdUa"] = idU;
            Session["IdA"] = idA[0];
            Response.Redirect("FrmArtista.aspx");
        }
        catch (Exception er)
        {
            lblMensaje.Text = "No se a seleccionado ningun artista";
        }
    }
    //Abrir el formulario del disco seleccionado del lstdiscos
    protected void btnDiscos_Click(object sender, EventArgs e)
    {
        try
        {
            //Se selecciona un disco y se lo envia al siguiente formulario
            //para mostrar la informacion de ese artista, ademas enviamos el 
            //id de usuario para que pueda interactuar con el disco
            string disco = lstDiscos.SelectedItem.ToString();
            string[] idD = disco.Split(':');
            Session["IdUd"] = idU;
            Session["IdD"] = idD[0];
            Response.Redirect("FrmDisco.aspx");
        }
        catch (Exception er) {
            lblMensaje.Text = "No se a seleccionado ningun disco";
        }
    }

    protected void txtBart_TextChanged(object sender, EventArgs e)
    {

    }
    //Aqui se realizara la busqueda de discos o artista en base
    //a un string ingresado en el txtBart donde no importa ingresar 
    //cadena completa, reconoce la coincidencia con la cadena ingresada
    //el resultado se  lo muestra en el lstResult
    protected void btnBart_Click(object sender, EventArgs e)
    {
        lblSaber.Text = ddlBuscar.Text;
        lstResult.Items.Clear();
        lstDiscos.Items.Clear();
        lstArtistas.Items.Clear();
        foreach (Artista item in listart)
        {
            if (item.puntuacion == 0)
            {
                califA = "0";
            }
            else
            {
                califA = Convert.ToString(item.sumatoria / item.puntuacion);
            }
            lstArtistas.Items.Add(item.idArtista + ":   " + item.nombreArtistico + "     " + califA);

        }
        foreach (Disco item in listdiscos)
        {
            if (item.puntuacion == 0)
            {
                califD = "0";
            }
            else
            {
                califD = Convert.ToString(item.sumatoria / item.puntuacion);
            }
            lstDiscos.Items.Add(item.idDisco + ":  " + item.nombreDisco + "     " + califD);
        }
        List<Artista> artistas1 = new List<Artista>();
        List<Disco> discos1 = new List<Disco>();
        int i = ddlBuscar.SelectedIndex;
       if (i == 0)
       {
            artistas1 = cliente.BusquedaArt(txtBart.Text);
            foreach (Artista item in artistas1)
            {
                lstResult.Items.Add(item.idArtista+":  "+item.nombreArtistico);
            }
       }
       else
       {
            discos1 = cliente.BusquedaDis(txtBart.Text);
            foreach (Disco item in discos1)
            {
                lstResult.Items.Add(item.idDisco+ ":  " + item.nombreDisco);
            }
        }
        ddlBuscar.Items.Clear();
        ddlBuscar.Items.Add("Artista");
        ddlBuscar.Items.Add("Disco");
        if (lstResult.Items.Count == 0)
        {
            lblMensaje.Text = "No hay resultado a su busqueda";
        }
        else{
            lblMensaje.Text = "Resultado de su busqueda";

        }
    }
    //Una vez obtenido el resultado de la busqueda se puede sellecionar
    //alguno de los resultados y mostrarlo mediante este boton
    protected void btnVer_Click(object sender, EventArgs e)
    {
        if (lblSaber.Text == "Artista")
        {
            try
            {
                //Se selecciona un artista y se lo envia al siguiente formulario
                //para mostrar la informacion de ese artista, ademas enviamos el 
                //id de usuario para que pueda interactuar con el artista
                string artista = lstResult.SelectedItem.ToString();
                string[] idA = artista.Split(':');
                Session["IdUa"] = idU;
                Session["IdA"] = idA[0];
                Response.Redirect("FrmArtista.aspx");
                //0994265837
            }
            catch (Exception er)
            {
                lblMensaje.Text = "No se a seleccionado ningun artista";
            }
        }
        else
        {

            try
            {
                //Se selecciona un disco y se lo envia al siguiente formulario
                //para mostrar la informacion de ese artista, ademas enviamos el 
                //id de usuario para que pueda interactuar con el disco
                string disco = lstResult.SelectedItem.ToString();
                string[] idD = disco.Split(':');
                Session["IdUd"] = idU;
                Session["IdD"] = idD[0];
                Response.Redirect("FrmDisco.aspx");
            }
            catch (Exception er)
            {
                lblMensaje.Text = "No se a seleccionado ningun disco";
            }
        }
     }
    //Volver al interfaz de logear usuario
    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("FrmUsuario.aspx");
    }

    protected void ddlBuscar_SelectedIndexChanged(object sender, EventArgs e)
    {

        lstResult.Items.Clear();
        lstDiscos.Items.Clear();
        lstArtistas.Items.Clear();
        foreach (Artista item in listart)
        {
            if (item.puntuacion == 0)
            {
                califA = "0";
            }
            else
            {
                califA = Convert.ToString(item.sumatoria / item.puntuacion);
            }
            lstArtistas.Items.Add(item.idArtista + ":   " + item.nombreArtistico + "     " + califA);

        }
        foreach (Disco item in listdiscos)
        {
            if (item.puntuacion == 0)
            {
                califD = "0";
            }
            else
            {
                califD = Convert.ToString(item.sumatoria / item.puntuacion);
            }
            lstDiscos.Items.Add(item.idDisco + ":  " + item.nombreDisco + "     " + califD);
        }

    }
}
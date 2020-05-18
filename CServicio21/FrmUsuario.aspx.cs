//*****************************************************************************************************************
//Codigo02
//Victoria Elizabeth Camacho Muncha
//Jennifer Samanta Minayo Yujato
//Fecha de entrega: 05/08/2018

//                  *****************************************************************************************
//                                      TEMA: Proyecto 02
//                  *****************************************************************************************
// Resultados:
// *El usuario desarollado en ASP NET puede acceder a los dos servicios WCF 
//  donde el primer servicio nos permite interactuar (mostrar) con los datos de las tablas
//  de la base de datos, ademas mediante este servicio podemos actualizar la 
//  informacion de discos, artistas y canciones, el segundo servicio es el que nos 
//  permite actualizar las calificaciones, estas pueden ser dadas solo 
//  por usuarios asi que este segundo servicio requiere de un login para poder calificar
//  al artista o disco ademas de que cada vez que se califica se actualiza la nota de 
//  calificacion en base a las calificaciones dadas por los usuarios (sumatoria) y
//  el numero de usuarios que lo han calificado haciendo un promedio.

// *El proyecto consta de 5 formularios web donde el primero es el form de login (FrmUsuario)
//  una vez que ingresa sigue el form principal (Principal) donde se muestran todos los disco,
//  artistas y se puede realizar la busqueda de algun artista o disco en especifico, desde este 
//  se puede ir al form de discos (FrmDisco) o artistas (FrmArtista) dependiendo de lo que  
//  haya seleccionado de los listbox de artista y listbox de disco o del resultado de la busqueda 
//  almacenada en el list resultado. 
//  El formulario Artista muestra la informacion del artista seleccionado en el form Principal
//  asi como la calificacion global dada por todos los usuarios y permite calificar al artista 
//  si el usuario aun no lo ha clificado y si ya lo ha calificado puede actualizar la calificacion
//  ademas tiene la opcion de actualizar artista abriendo el form Actualizar.
//  El formulario Disco muestrala informacion del disco seleccionado en el form Principal
//  ademas muestra la informacion del Artista al que pertenece el disco y muesra las canciones 
//  existentes dentro del disco, tambien permite dar calificacion o actualizar calificacion 
//  o actualizar los datos del disco abriendo el form Actualizar.
//  El ultimo formulario es el formulario Actualizar donde si se actualizara el artista no se 
//  muestran el panel de disco y cancion y si se desea actualizar disco se muestran todos los 
//  paneles pero se bloquea el del artista ya que se modificara el disco y las canciones dentro 
//  del disco que el usuario selecciono para actualizar
// *
//** Conclusiones: 
//  
//  *La aplicacion permite dar calificacion a los artistas y a los discos independientemente una 
//  de la otra ademas permite mostrar la informacion tanto de un artista o un disco cada uno en 
//  un formulario independiente dentro del artista mostrara los discos del artista y dentro de 
//  cada disco las canciones del disco.
//  *Se puede modificar la informacion del artista,disco y cancion dentro del formulario Actualizar
//  dependiendo de lo que requiera el usuario, no se puede actualizar la calificacion global del 
//  artista ya que esta no depende de un solo usuario, sino de todos los que han dado una calificacion.
//  *Las calificaciones dadas al Artista o al disco se almacenaran en las tablas correspondientes de
//  calificaciones y cada vez que se desee actualizar estas calificaciones se accedera a estas tablas
//  en base al id del usuario y del disco o artista por calificar y se tomara la calificacion para
//  actualizar los campos sumatorio y puntuacion de las correspondientes tablas artistas y discos.
//
//** Recomendaciones:
// * Cada vez que se dispara un evento al interactuar en el web form este se actualiza y algunas de las   
//  listas de datos mostradas en el litsbox se duplican por lo que es necesario considerar si estos list
//  boxs ya estan llenos no los vuelva a llenar y si estas vacios que se llenen 
//  *El paso de informacion entre los formularios es muy util al momento de cargar la informacion en cada
//  formulario dependiendo de lo que se deba mostrar en dicho fomulario, ademas entre todos los formularios
//  se pasa la informacion del id de usuario para que constemente se muestre quien es el usuario que esta
//  consumiendo el servicio web.
//  
// *****************************************************************************************************************
// *******************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Serviciousuario;

public partial class FrmUsuario : System.Web.UI.Page
{
    //Definimos un cliente que accedera al servicio
    ServiceClient cliente = new ServiceClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    //Con el boton entrar accedemos al contrato Validar usuario
    protected void btnEntrar_Click(object sender, EventArgs e)
    {
        Usuario usuario = new Usuario();
        string user = txtNombre.Text;
        string contra = txtCon.Text;
        //Los parametros de entrada seran el usuario y la contraseña
        //que se escriba en los textbox correspondientes
        usuario = cliente.ValidarUsuario(user, contra);
        //Si nos devuelve un usuario null el mensaje sera 
        //usuario no registrado pero si si existe se dirigira 
        //al formulario principal.
        if(usuario.username == txtNombre.Text && usuario.password==txtCon.Text)
        {
            string idU = usuario.idUsuario.ToString();
            //Enviamos el id del usuario al siguiente formulario
            Session["IdU"] = idU;
            //Abrimos el siguiente formulario
            Response.Redirect("Principal.aspx");
        }
        else
        {

            lblMensaje.Text = "Usuario no registrado";
        }
    }
    //Permitira ingresar a cualquier persona a ver la informacion
    //existente en ella pero no puede interactuar con ella por 
    //ejemplo no podra calificar
    protected void btnInvitado_Click(object sender, EventArgs e)
    {
        string idU = "0";
        //Enviamos el id del usuario al siguiente formulario
        Session["IdU"] = idU;
        //Abrimos el siguiente formulario
        Response.Redirect("Principal.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pokeDominio;
using datosNegocio;

namespace Pokedex
{
    public partial class frmAltaPokemon : Form
    {
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon nuevoPoke = new Pokemon();
                PokemonDatosNegocio datosNegocio = new PokemonDatosNegocio();

                nuevoPoke.Numero = int.Parse(txtNumero.Text);
                nuevoPoke.Nombre = txtNombre.Text;
                nuevoPoke.Descripcion = txtDescripcion.Text;
                nuevoPoke.Tipo = (Elemento)cboxTipo.SelectedItem;
                nuevoPoke.Debilidad = (Elemento)cboxDebilidad.SelectedItem;

                datosNegocio.agregar(nuevoPoke);
                MessageBox.Show("Nuevo pokemon agregado");
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoDatosNegocio elementoDatos = new ElementoDatosNegocio();
            try
            {
                cboxTipo.DataSource = elementoDatos.listar();
                cboxDebilidad.DataSource = elementoDatos.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

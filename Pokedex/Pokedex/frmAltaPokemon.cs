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

        private Pokemon pokemon = null;
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        public frmAltaPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
            btnAgregar.Text = "Modificar";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            PokemonDatosNegocio datosNegocio = new PokemonDatosNegocio();
            
            try
            {
                if (pokemon == null)
                    pokemon = new Pokemon();

                pokemon.Numero = int.Parse(txtNumero.Text);
                pokemon.Nombre = txtNombre.Text;
                pokemon.Descripcion = txtDescripcion.Text;
                pokemon.UrlImagen = txtUrlImagen.Text;
                pokemon.Tipo = (Elemento)cboxTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cboxDebilidad.SelectedItem;

                if(pokemon.Id != 0)
                {
                    datosNegocio.modificar(pokemon);
                    MessageBox.Show("Pokemon modificado");
                }
                else
                {
                    datosNegocio.agregar(pokemon);
                    MessageBox.Show("Pokemon agregado");
                }

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
                cboxTipo.ValueMember = "Id";
                cboxTipo.DisplayMember = "Descripcion";

                cboxDebilidad.DataSource = elementoDatos.listar();
                cboxDebilidad.ValueMember = "Id";
                cboxDebilidad.DisplayMember = "Descripcion";

                if (pokemon != null)
                {
                    txtNumero.Text = pokemon.Numero.ToString();
                    txtNombre.Text = pokemon.Nombre;
                    txtDescripcion.Text = pokemon.Descripcion;
                    txtUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cboxTipo.SelectedValue = pokemon.Tipo.Id;
                    cboxDebilidad.SelectedValue = pokemon.Debilidad.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pboxAlta.Load(imagen);
            }
            catch (Exception ex)
            {
                pboxAlta.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

    }
}

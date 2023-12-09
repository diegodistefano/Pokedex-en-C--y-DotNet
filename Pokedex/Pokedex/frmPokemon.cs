using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using datosNegocio;
using pokeDominio;

namespace Pokedex
{
    public partial class frmPokemon : Form
    {
        private List<Pokemon> listaPokemon;
        public frmPokemon()
        {
            InitializeComponent();
        }

        private List<Elemento> listaElementos;


        private void frmPokemon_Load(object sender, EventArgs e)
        {
            PokemonDatosNegocio negocio = new PokemonDatosNegocio();
            listaPokemon = negocio.listar();
            dgvPokemons.DataSource = listaPokemon;
            dgvPokemons.Columns["UrlImagen"].Visible = false;
            cargarImagen(listaPokemon[0].UrlImagen);

            ElementoDatosNegocio elementoDatos = new ElementoDatosNegocio();
            listaElementos = elementoDatos.listar();
            dgvElementos.DataSource = listaElementos;
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }

        private void dgvElementos_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvElementos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception)
            {
                pbxPokemon.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnAgregarPokemon_Click(object sender, EventArgs e)
        {
            frmAltaPokemon altaPokemon = new frmAltaPokemon();
            altaPokemon.ShowDialog();
        }
    }
}
 
using datosNegocio;
using pokeDominio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pokedex
{
    public partial class frmPokemon : Form
    {
        private List<Pokemon> listaPokemon;
        //private List<Elemento> listaElementos;
                
        public frmPokemon()
        {
            InitializeComponent();
        }

        private void frmPokemon_Load(object sender, EventArgs e)
        {
            cargar();
            cBoxCampo.Items.Add("Número");
            cBoxCampo.Items.Add("Nombre");
            cBoxCampo.Items.Add("Descripcion");

            //ElementoDatosNegocio elementoDatos = new ElementoDatosNegocio();
            //listaElementos = elementoDatos.listar();
            //dgvElementos.DataSource = listaElementos;
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPokemons.CurrentRow != null)
            {
                Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
        }

        //private void dgvElementos_SelectionChanged(object sender, EventArgs e)
        //{
        //    Pokemon seleccionado = (Pokemon)dgvElementos.CurrentRow.DataBoundItem;
        //    cargarImagen(seleccionado.UrlImagen);
        //}

        private void cargar()
        {
            PokemonDatosNegocio negocio = new PokemonDatosNegocio();
            try
            {
                listaPokemon = negocio.listar();
                dgvPokemons.DataSource = listaPokemon;
                ocultarColumnas();
                cargarImagen(listaPokemon[0].UrlImagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            try
            {
                dgvPokemons.Columns["UrlImagen"].Visible = false;
                dgvPokemons.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxPokemon.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }
        
        private void eliminar(bool logico = false)
        {
            PokemonDatosNegocio datos = new PokemonDatosNegocio();
            Pokemon seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("Estas Seguro de eliminar este Pokemon?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                    
                    if(logico)
                        datos.eliminarLogico(seleccionado.Id);
                    else
                        datos.eliminar(seleccionado.Id);

                    MessageBox.Show("Pokemon eliminado");
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool validarFiltro()
        {
            if (cBoxCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un campo");
                return true;
            }
            if (cBoxCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un criterio");
                return true;
            }

            if (cBoxCampo.SelectedItem.ToString() == "Número")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Ingrese un valor numerico");
                    return true;
                }
                if (!(soloNumeros(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Ingrese un valor numerico");
                    return true;
                }
            }

            return false;
        }

        private bool soloNumeros(string cadena) 
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }


        private void btnAgregarPokemon_Click(object sender, EventArgs e)
        {
            frmAltaPokemon altaPokemon = new frmAltaPokemon();
            altaPokemon.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

            frmAltaPokemon modificarPokemon = new frmAltaPokemon(seleccionado);
            modificarPokemon.ShowDialog();
            cargar();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnEliminarLogica_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            PokemonDatosNegocio negocio = new PokemonDatosNegocio();
            try
            {
                validarFiltro();
                string campo = cBoxCampo.SelectedItem.ToString();
                string criterio = cBoxCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                dgvPokemons.DataSource = negocio.filtrar(campo, criterio, filtro);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
             List<Pokemon> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro != "")
            {
                listaFiltrada = listaPokemon.FindAll( x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Tipo.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaPokemon;
            }

            dgvPokemons.DataSource = null;
            dgvPokemons.DataSource = listaFiltrada;

            ocultarColumnas();
        }

        private void cBoxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cBoxCampo.SelectedItem.ToString();

            if (opcion == "Número")
            {
                cBoxCriterio.Items.Clear();
                cBoxCriterio.Items.Add("Mayor a:");
                cBoxCriterio.Items.Add("Menor a:");
                cBoxCriterio.Items.Add("Igual a:");
            }
            else
            {
                cBoxCriterio.Items.Clear();
                cBoxCriterio.Items.Add("Comienza con:");
                cBoxCriterio.Items.Add("Termina con:");
                cBoxCriterio.Items.Add("Contiene:");
            }
        }
    }
}
 
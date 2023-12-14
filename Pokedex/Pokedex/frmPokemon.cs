﻿using datosNegocio;
using pokeDominio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            cargar();

            ElementoDatosNegocio elementoDatos = new ElementoDatosNegocio();
            listaElementos = elementoDatos.listar();
            //dgvElementos.DataSource = listaElementos;
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
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
                dgvPokemons.Columns["UrlImagen"].Visible = false;
                dgvPokemons.Columns["Id"].Visible = false;
                cargarImagen(listaPokemon[0].UrlImagen);
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
    }
}
 
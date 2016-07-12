﻿using Clases;
using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;

using System.Windows.Forms;

namespace GDD.ComprarOfertar
{
    public partial class frmHome : Form
    {
        private int publicacionesXpagina = 21;
        private int paginaActual = 0;
        private int ultimaPagina = 0;
        private List<PublicacionShow> publicaciones;
        private Usuario usuario;

        public frmHome(Usuario us)
        {
            InitializeComponent();
            usuario = us;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            List<Rubro> rubros;
            int i = 1;
            if (clbRubros.CheckedItems.Count == 0)
            {
                rubros = clbRubros.Items.Cast<Rubro>().ToList();
            }
            else
            {
                rubros = clbRubros.CheckedItems.Cast<Rubro>().ToList();
            }

            var parametros = new Dictionary<string, object>()
            {
                { "@descripcion", txtDescripcion.Text}
            };
            
            rubros.ForEach(r =>
            {
                parametros.Add("@r" + i.ToString(), r.Id);
                i++;
            });
            for ( ; i <= 23; i++)
            {
                parametros.Add("@r" + i.ToString(), -1);
            }

            bool reset;
            try
            {
                reset = (bool)sender;
            }
            catch
            {
                reset = true;
            }

            LoadPublicaciones(ToPublicacionesShow(DBHelper.ExecuteReader(
                "Publicacion_GetPublicacionesByDescripcionYRubro_Show", parametros)), reset);
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            LoadPublicaciones(ToPublicacionesShow(DBHelper.ExecuteReader("Publicacion_GetAll_Show")), true);
            LoadRubros(DBHelper.ExecuteReader("Rubro_GetAll").ToRubros());
        }

        private void LoadRubros(List<Rubro> rubros)
        {
            clbRubros.DataSource = null;
            clbRubros.DataSource = rubros;
            clbRubros.DisplayMember = "DescripcionCorta";
        }

        private void LoadPublicaciones(List<PublicacionShow> publicaciones, bool reset)
        {
            this.publicaciones = publicaciones;
            if (reset)
            {
                paginaActual = 0;
                ultimaPagina = (int)Math.Floor(Convert.ToDouble(publicaciones.Count / publicacionesXpagina));
            }
            dgvPublicaciones.DataSource = null;
            dgvPublicaciones.DataSource = actualizarPagina();
        }

        private List<PublicacionShow> actualizarPagina()
        {
            List<PublicacionShow> retorno;
            lblPagina.Text = (paginaActual + 1).ToString();
            btnInicio.Enabled = true;
            btnAnterior.Enabled = true;
            btnSiguiente.Enabled = true;
            btnFin.Enabled = true;
            if (paginaActual == ultimaPagina)
            {
                int mod = publicaciones.Count % publicacionesXpagina;
                if (mod != 0)
                {
                    retorno = publicaciones.GetRange(paginaActual * publicacionesXpagina, mod);
                }
                else
                {
                    ultimaPagina -= 1;
                    paginaActual = ultimaPagina;
                    retorno = publicaciones.GetRange(paginaActual * publicacionesXpagina, publicacionesXpagina);
                }
                btnSiguiente.Enabled = false;
                btnFin.Enabled = false;
            }
            else
            {
                retorno = publicaciones.GetRange(paginaActual * publicacionesXpagina, publicacionesXpagina);
                if(paginaActual == 0)
                {
                    btnInicio.Enabled = false;
                    btnAnterior.Enabled = false;
                }
            }
            
            return retorno;
        }

        internal class PublicacionShow
        {
            public int Id { get; set; }
            public string Tipo { get; set; }
            public string Descripcion { get; set; }
            public string Usuario { get; set; }
            public string Rubro { get; set; }
            public int Stock { get; set; }
            public decimal Precio { get; set; }
            public string Visibilidad { get; set; }
        }

        internal List<PublicacionShow> ToPublicacionesShow(SqlDataReader rdr)
        {
            List<PublicacionShow> list = new List<PublicacionShow>();
            while (rdr.Read())
            {
                list.Add(new PublicacionShow()
                {
                    Id = (int)rdr["publ_id"],
                    Tipo = (string)rdr["publ_tipo"],
                    Descripcion = (string)rdr["publ_descripcion"],
                    Usuario = (string)rdr["publ_usuario"],
                    Rubro = (string)rdr["rubr_descripcion_corta"],
                    Stock = (int)rdr["publ_stock"],
                    Precio = (decimal)rdr["publ_precio"],
                    Visibilidad = (string)rdr["visi_detalle"]
                });
            }
            DBHelper.DB.Close();
            return list;
        }

        private void dgvPublicaciones_MouseClick(object sender, MouseEventArgs e)
        {
            PublicacionShow publicacion = (PublicacionShow)dgvPublicaciones.SelectedRows[0].DataBoundItem;
            if (publicacion.Tipo == "Subasta")
            {
                lblTextoAccion.Text = "Ofertar por el monto:";
                btnAccionar.Text = "OFERTAR";
            }
            else
            {
                lblTextoAccion.Text = "Cantidad a comprar:";
                btnAccionar.Text = "COMPRAR";
            }
            lblTextoAccion.Enabled = true;
            txtAccion.Enabled = true;
            btnAccionar.Enabled = true;
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            dgvPublicaciones.DataSource = null;
            dgvPublicaciones.DataSource = actualizarPagina();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            paginaActual -= 1;
            dgvPublicaciones.DataSource = null;
            dgvPublicaciones.DataSource = actualizarPagina();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual += 1;
            dgvPublicaciones.DataSource = null;
            dgvPublicaciones.DataSource = actualizarPagina();
        }

        private void btnFin_Click(object sender, EventArgs e)
        {
            paginaActual = ultimaPagina;
            dgvPublicaciones.DataSource = null;
            dgvPublicaciones.DataSource = actualizarPagina();
        }

        private void btnAccionar_Click(object sender, EventArgs e)
        {
            if (txtAccion.Text != "")
            {
                PublicacionShow publ = (PublicacionShow)dgvPublicaciones.SelectedRows[0].DataBoundItem;
                if (btnAccionar.Text == "COMPRAR")
                {
                    int cantidad = Convert.ToInt32(txtAccion.Text);

                    if (cantidad > publ.Stock)
                    {
                        MessageBox.Show("No hay stock disponible");
                    }
                    else
                    {
                        var parametros = new Dictionary<string, object>()
                    {
                        { "@cliente", GetClienteIdByUsername()},
                        { "@publicacion", publ.Id},
                        { "@cantidad", cantidad},
                        { "@fecha", DateTime.Parse(ConfigurationManager.AppSettings["fecha"]) }
                    };
                        DBHelper.ExecuteNonQuery("Venta_Add", parametros);
                        btnFiltrar_Click(false, new EventArgs());
                    }
                }
                else if (btnAccionar.Text == "OFERTAR")
                {
                    Oferta oferta = new Oferta();
                    oferta.Monto = Convert.ToInt32(txtAccion.Text);
                    oferta.PublicacionId = publ.Id;
                    oferta.ClienteId = GetClienteIdByUsername();

                    if (oferta.Monto > publ.Precio)
                    {
                        var parametros = new Dictionary<string, object>()
                    {
                        { "@cliente", oferta.ClienteId},
                        { "@publicacion", oferta.PublicacionId},
                        { "@monto", oferta.Monto},
                        { "@fecha", DateTime.Parse(ConfigurationManager.AppSettings["fecha"]) }
                    };
                        DBHelper.ExecuteNonQuery("Oferta_Add", parametros);
                        btnFiltrar_Click(false, new EventArgs());
                    }
                    else
                    {
                        MessageBox.Show("El monto debe superar la ultima puja");
                    }
                }

            }
            else
            {
                MessageBox.Show("Ingrese un número");
            }
        }

        private int GetClienteIdByUsername()
        {
            var parametros = new Dictionary<string, object>()
            {
                { "@username", usuario.Username}
            };
            return DBHelper.ExecuteReader("Cliente_GetByUsername", parametros).ToCliente().Id;
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            SeleccionarRubros(true);
        }

        private void btnNinguno_Click(object sender, EventArgs e)
        {
            SeleccionarRubros(false);
        }

        private void SeleccionarRubros(bool selected)
        {
            for (int i = 0; i < clbRubros.Items.Count; i++)
            {
                clbRubros.SetItemChecked(i, selected);
            }
        }

    }
}

﻿using Clases;
using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace GDD.ABM_Usuario
{
    public partial class frmEmpresa : Form
    {
        private Usuario usuario;
        
        public frmEmpresa(Usuario us)
        {
            InitializeComponent();
            usuario = us;
        }
        private void frmEmpresa_Load(object sender, EventArgs e)
        {
            cmbRubro.DataSource = DBHelper.ExecuteReader("Rubro_GetAll").ToRubros(); 
            cmbRubro.DisplayMember = "DescripcionCorta";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (DatosCompletados())
            {
                var emp = new Dictionary<string, object>() {
                    { "@Username", usuario.Username },
                    { "@RazonSocial", txtRazonSocial.Text },
                    { "@Mail" , txtMail.Text },
                    { "@Telefono" ,  txtTelefono.Text},
                    { "@Direccion" ,  txtDireccion.Text},
                    { "@CodigoPostal" ,  Convert.ToInt32(txtCodPostal.Text)},
                    { "@Cuit" ,  txtCuit.Text},
                    { "@Ciudad" ,  txtCiudad.Text},
                    { "@NombreContacto" , txtNombre.Text},
                    { "@RubroId" ,  ((Rubro)cmbRubro.SelectedItem).Id}
                };
                DBHelper.ExecuteNonQuery("Usuario_Add", new Dictionary<string, object> { { "@Username", usuario.Username }, { "@Password", usuario.Password } });
                DBHelper.ExecuteNonQuery("Empresa_Add", emp);
                MessageBox.Show("Ingresado con exitos");
                Hide();
            }
            else
            {
                MessageBox.Show("Complete los campos correctamente");
            }
        }

        private bool DatosCompletados() {
            int result;
            return txtRazonSocial != null && int.TryParse(txtRazonSocial.Text, out result) &&
                txtMail != null &&
                txtTelefono != null && 
                txtDireccion != null &&
                txtCodPostal != null && int.TryParse(txtCodPostal.Text, out result) &&
                txtCiudad != null &&
                txtCuit != null &&
                txtNombre != null &&
                cmbRubro.SelectedItem != null;
        }        
    }
}

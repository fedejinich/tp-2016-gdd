﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Clases;

namespace Helpers
{
    public static class ConversionHelper
    {
        #region USUARIO
        public static Usuario ToUsuario(this SqlDataReader rdr) {
            return rdr.ToUsuarios().FirstOrDefault();
        }
        public static List<Usuario> ToUsuarios(this SqlDataReader rdr) {
            List<Usuario> list = new List<Usuario>();
            while (rdr.Read()) {
                list.Add(new Usuario() {
                    Username = (string)rdr["usua_username"],
                    Activo = (bool)rdr["usua_activo"],
                    Password = (string)rdr["usua_password"],
                    Habilitado = (bool)rdr["usua_habilitado"],
                    Intentos= (int)rdr["usua_intentos"]
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region CLIENTE
        public static Cliente ToCliente(this SqlDataReader rdr)
        {
            return rdr.ToClientes().FirstOrDefault();
        }
        public static List<Cliente> ToClientes(this SqlDataReader rdr)
        {
            List<Cliente> list = new List<Cliente>();
            while (rdr.Read())
            {
                list.Add(new Cliente()
                {
                    Username = (string)rdr["clie_usuario"],
                    Nombre = (string)rdr["clie_nombre"],
                    Apellido = (string)rdr["clie_apellido"],
                    Dni = (int)rdr["clie_dni"],
                    TipoDocumento = (string)rdr["clie_tipo_documento"],
                    Mail = (string)rdr["clie_mail"],
                    Telefono = (string)rdr["clie_telefono"],
                    Direccion = (string)rdr["clie_direccion"],
                    CodigoPostal = (int)rdr["clie_codigo_postal"],
                    FechaNacimiento = (DateTime)rdr["clie_fecha_nacimiento"],
                    Id = (int)rdr["clie_id"],
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region EMPRESA
        public static Empresa ToEmpresa(this SqlDataReader rdr)
        {
            return rdr.ToEmpresas().FirstOrDefault();
        }
        public static List<Empresa> ToEmpresas(this SqlDataReader rdr)
        {
            List<Empresa> list = new List<Empresa>();
            while (rdr.Read())
            {
                list.Add(new Empresa()
                {
                    Username = (string)rdr["empr_usuario"],
                    RazonSocial = (string)rdr["empr_razon_social"],
                    Mail = (string)rdr["empr_mail"],
                    Telefono = (string)rdr["empr_telefono"],
                    Direccion = (string)rdr["empr_direccion"],
                    CodigoPostal = (int)rdr["empr_codigoPostal"],
                    Ciudad = (string)rdr["empr_ciudad"],
                    Cuit = (string)rdr["empr_cuit"],
                    NombreContacto = (string)rdr["empr_nombre_contacto"],
                    ReputacionTotal = (int)rdr["empr_reputacion_total"],
                    ReputacionCantVotos = (int)rdr["empr_reputacion_cantidad_votos"],
                    RubroId = (int)rdr["empr_rubro"],
                    Id= (int)rdr["empr_id"],
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region CALIFICACION
        public static Calificacion ToCalificacion(this SqlDataReader rdr)
        {
            return rdr.ToCalificaciones().FirstOrDefault();
        }
        public static List<Calificacion> ToCalificaciones(this SqlDataReader rdr)
        {
            List<Calificacion> list = new List<Calificacion>();
            while (rdr.Read())
            {
                list.Add(new Calificacion()
                {
                    Id = (int)rdr["cali_id"],
                    Estrellas = (int)rdr["cali_estrellas"],
                    Detalle = (string)rdr["cali_detalle"],
                    Fecha = (DateTime)rdr["cali_fecha"],
                    VentaId = (int)rdr["cali_venta"],
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region ESTADO
        public static Estado ToEstado(this SqlDataReader rdr)
        {
            return rdr.ToEstados().FirstOrDefault();
        }
        public static List<Estado> ToEstados(this SqlDataReader rdr)
        {
            List<Estado> list = new List<Estado>();
            while (rdr.Read())
            {
                list.Add(new Estado()
                {
                    Id = (int)rdr["esta_id"],
                    Descripcion = (string)rdr["esta_descripcion"],
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region FACTURA
        public static Factura ToFactura(this SqlDataReader rdr)
        {
            return rdr.ToFacturas().FirstOrDefault();
        }
        public static List<Factura> ToFacturas(this SqlDataReader rdr)
        {
            List<Factura> list = new List<Factura>();
            while (rdr.Read())
            {
                list.Add(new Factura()
                {
                    Numero = (int)rdr["fact_id"],
                    Fecha = (DateTime)rdr["fact_fecha"],
                    Total = (decimal)rdr["fact_total"],
                    PublicacionId = (int)rdr["fact_publicacion"],
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region ITEMFACTURA
        public static ItemFactura ToItemFactura(this SqlDataReader rdr)
        {
            return rdr.ToItemFacturas().FirstOrDefault();
        }
        public static List<ItemFactura> ToItemFacturas(this SqlDataReader rdr)
        {
            List<ItemFactura> list = new List<ItemFactura>();
            while (rdr.Read())
            {
                list.Add(new ItemFactura()
                {
                    FacturaId = (int)rdr["item_factura"],
                    Id = (int)rdr["item_id"],
                    Cantidad = (decimal)rdr["item_cantidad"],
                    PrecioUnitario = (decimal)rdr["item_precio_unitario"],
                    Detalle = (string)rdr["item_detalle"]
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region OFERTA
        public static Oferta ToOferta(this SqlDataReader rdr)
        {
            return rdr.ToOfertas().FirstOrDefault();
        }
        public static List<Oferta> ToOfertas(this SqlDataReader rdr)
        {
            List<Oferta> list = new List<Oferta>();
            while (rdr.Read())
            {
                list.Add(new Oferta()
                {
                    Id = (int)rdr["ofer_id"],
                    Monto = (decimal)rdr["ofer_monto"],
                    Fecha = (DateTime)rdr["ofer_fecha"],
                    PublicacionId = (int) rdr["ofer_publicacion"],
                    ClienteId = (int)rdr["ofer_cliente"],
                    ConEnvio = (bool)rdr["ofer_envio"],
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region PUBLICACION
        public static Publicacion ToPublicacion(this SqlDataReader rdr)
        {
            return rdr.ToPublicaciones().FirstOrDefault();
        }
        public static List<Publicacion> ToPublicaciones(this SqlDataReader rdr)
        {
            List<Publicacion> list = new List<Publicacion>();
            while (rdr.Read())
            {
                list.Add(new Publicacion()
                {
                    Id = (int)rdr["publ_id"],
                    Tipo = (string)rdr["publ_tipo"],
                    FechaInicio = (DateTime)rdr["publ_fecha_inicio"],
                    FechaVencimiento = (DateTime)rdr["publ_fecha_vencimiento"],
                    Descripcion = (string)rdr["publ_descripcion"],
                    Usuario = (string)rdr["publ_usuario"],
                    Rubro = (int)rdr["publ_rubro"],
                    Estado = (int)rdr["publ_estado"],
                    Stock = (int)rdr["publ_stock"],
                    Precio = (decimal)rdr["publ_precio"],
                    VisibilidadId = (int)rdr["publ_visibilidad"]
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region ROL
        public static Rol ToRol(this SqlDataReader rdr)
        {
            return rdr.ToRoles().FirstOrDefault();
        }
        public static List<Rol> ToRoles(this SqlDataReader rdr)
        {
            List<Rol> list = new List<Rol>();
            while (rdr.Read())
            {
                list.Add(new Rol()
                {
                    Id = (int)rdr["rol_id"],
                    Nombre =(string)rdr["rol_nombre"],
                    Activo = Convert.ToBoolean((int)rdr["rol_activo"])
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region FUNCION
        public static Funcion ToFuncion(this SqlDataReader rdr)
        {
            return rdr.ToFunciones().FirstOrDefault();
        }
        public static List<Funcion> ToFunciones(this SqlDataReader rdr)
        {
            List<Funcion> list = new List<Funcion>();
            while (rdr.Read())
            {
                list.Add(new Funcion()
                {
                    Id = (int)rdr["func_id"],
                    Descripcion = (string)rdr["func_descripcion"]
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region RUBRO
        public static Rubro ToRubro(this SqlDataReader rdr)
        {
            return rdr.ToRubros().FirstOrDefault();
        }
        public static List<Rubro> ToRubros(this SqlDataReader rdr)
        {
            List<Rubro> list = new List<Rubro>();
            while (rdr.Read())
            {
                list.Add(new Rubro()
                {
                    Id = (int)rdr["rubr_id"],
                    DescripcionCorta = (string)rdr["rubr_descripcion_corta"],
                    //TODO: CORREGIR EN LA BASE DE DATOS PARA QUE INSERTE UN STRING VACIO EN VEZ DE NULL EN LA MIGRACION
                    //DescripcionLarga = (string)rdr["rubr_descripcion_larga"]
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region VENTA
        public static Venta ToVenta(this SqlDataReader rdr)
        {
            return rdr.ToVentas().FirstOrDefault();
        }
        public static List<Venta> ToVentas(this SqlDataReader rdr)
        {
            List<Venta> list = new List<Venta>();
            while (rdr.Read())
            {
                list.Add(new Venta()
                {
                    Id = (int)rdr["vent_id"],
                    Cantidad = (int)rdr["vent_cantidad"],
                    PublicacionId = (int)rdr["vent_publicacion"],
                    Fecha = (DateTime)rdr["vent_fecha"],
                    ClienteId = (int)rdr["vent_cliente"],
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region VISIBILIDAD
        public static Visibilidad ToVisibilidad(this SqlDataReader rdr)
        {
            return rdr.ToVisibilidades().FirstOrDefault();
        }
        public static List<Visibilidad> ToVisibilidades(this SqlDataReader rdr)
        {
            List<Visibilidad> list = new List<Visibilidad>();
            while (rdr.Read())
            {
                list.Add(new Visibilidad()
                {
                    Id = (int)rdr["visi_id"],
                    CostoEnvio = (int)rdr["visi_costo_envio"],
                    CostoPublicacion = (int)rdr["visi_costo_publicacion"],
                    Detalle = (string)rdr["visi_detalle"],
                    PorcentajeProducto = (int)rdr["visi_porcentaje_prod"]
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion

        #region ESTADISTICA
        public static List<Estadistica> ToEstadisticas(this SqlDataReader rdr)
        {
            List<Estadistica> list = new List<Estadistica>();
            while (rdr.Read())
            {
                list.Add(new Estadistica()
                {
                    Username = (string)rdr["username"],
                    Extra =  Convert.ToDouble(rdr["extra"])
                });
            }
            DBHelper.DB.Close();
            return list;
        }
        #endregion
    }
}

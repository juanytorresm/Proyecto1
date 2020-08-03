using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test2.Models;
using System.Data.Sql;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
       public string MyFirstMethod()
        {
            return "Welcome to Quespond!";
        }
        public ActionResult MyFirstViewMethod()
        {
            CustomerBAL b = new CustomerBAL();
            Customer c = b.GetCustomer();

            //string cadena = "Data Source=Desarrollo; Initial Catalog =pruebas; User ID=sa;Password=sa";
            //SqlConnection conectarBD = new SqlConnection();
            //conectarBD.ConnectionString = cadena;
            //conectarBD.Open();

            //conexionBD conBD = new conexionBD();
            //conBD.abrir();

            //SqlCommand com = new SqlCommand("select * from cliente", conBD.conectarBD);



            //listaUsuarios = com.ExecuteScalar();

            // var usuario = listaUsuarios.FirstOrDefault(f => f.Nombre == "Prodensa");
            DataBasepruebas conbd = new DataBasepruebas();


            var listaClientes = (from d in conbd.Cliente
                                 select new ListaCliente
                                 {
                                     IdCliente = d.IdCliente,
                                     Numero = d.Numero,
                                     Nombre = d.Nombre

                                 }
                ).ToList();
            //var listaClientes = conbd.Cliente.ToList();

            //var Cliente = conbd.Cliente.FirstOrDefault(f => f.Nombre == "Prodensa");
            //return new JsonResult
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    //Data = listaClientes
            //    Data = Cliente
            //};

            //var count = (int)com.ExecuteScalar();
            //ViewData["TotalData"] = count;
            //string CustomerName = "Mr ABC.";
            ////ViewData["MyData"] = CustomerName;
            //ViewBag.MyData = "CustomerName";
            //ViewBag.CustomerData = c;
            //conBD.cerrar();
            //conectarBD.Close();
            return View("MyFirstView",listaClientes);
        }

        public ActionResult Nuevo()
        {
            return View("Nuevo");
        }
        [HttpPost]
        public ActionResult Nuevo(ClienteViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DataBasepruebas db = new DataBasepruebas())
                    {
                        var oCliente = new Cliente();
                        //oCliente.IdCliente = model.IdCliente;
                        oCliente.Numero = model.Numero;
                        oCliente.Nombre = model.Nombre;
                        oCliente.FechaAlta = model.FechaAlta;
                        oCliente.EmailAddress = model.EmailAddress;

                        db.Cliente.Add(oCliente);
                        db.SaveChanges();
                    }
                    return Redirect("~/Test/MyFirstViewMethod");
                }
                return View(model);
            }
            catch (Exception ex)
                {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int Id)
        {
            ClienteViewModels model = new ClienteViewModels();
            using(DataBasepruebas db = new DataBasepruebas())
            {
                var oCliente = db.Cliente.Find(Id);
                model.Numero = oCliente.Numero;
                model.Nombre = oCliente.Nombre;
                model.FechaAlta = oCliente.FechaAlta;
                model.EmailAddress = oCliente.EmailAddress;
                model.IdCliente = oCliente.IdCliente;
                //prueba cambio

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(ClienteViewModels model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (DataBasepruebas db = new DataBasepruebas())
                    {
                        var oCliente = db.Cliente.Find(model.IdCliente);
                        //oCliente.IdCliente = model.IdCliente;
                        oCliente.Numero = model.Numero;
                        oCliente.Nombre = model.Nombre;
                        oCliente.FechaAlta = model.FechaAlta;
                        oCliente.EmailAddress = model.EmailAddress;

                        db.Entry(oCliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Test/MyFirstViewMethod");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            ClienteViewModels model = new ClienteViewModels();
            using (DataBasepruebas db = new DataBasepruebas())
            {
                var oCliente = db.Cliente.Find(Id);
                db.Cliente.Remove(oCliente);
                db.SaveChanges();

            }
            return Redirect("~/Test/MyFirstViewMethod");
        }

    }
}
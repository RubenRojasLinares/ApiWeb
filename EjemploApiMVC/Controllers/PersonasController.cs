using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EjemploApiMVC.DAOPersonas;
using EjemploApiMVC.Model.Personas;
using EjemploApiMVC.Model.Respuesta;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EjemploApiMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        [HttpGet]
        public List<Persona> ObtienePersonas(string nombre=null,int edad=0, string mail=null) {

            DataTable dtObtienePersonas =  PersonaBD.Instance.ObtienePersonas(nombre, edad, mail);
            List<Persona> listaPersonas = new List<Persona>();

            foreach (DataRow row in dtObtienePersonas.Rows)
            {
                listaPersonas.Add(new Persona { 
                    Edad=int.Parse( row["Edad"].ToString()),
                    Mail= row["Mail"].ToString(),
                    Nombre= row["Nombre"].ToString()
                });
            }

            return listaPersonas;
        }

        [HttpPost]
        public Respuesta InsertaPersona(string nombre , int edad , string mail )
        {
            Respuesta respuesta = new Respuesta
            {
                filasAfectadas = PersonaBD.Instance.InsertarPersonas(nombre, edad, mail)
            };

            return respuesta;
        }
             
        
    }
}
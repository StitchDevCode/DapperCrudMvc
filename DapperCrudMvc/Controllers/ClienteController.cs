using Dapper;
using DapperCrudMvc.Conexcion;
using DapperCrudMvc.Models;
using Microsoft.AspNetCore.Mvc;
using static DapperCrudMvc.Models.Enum;

namespace DapperCrudMvc.Controllers
{
    public class ClienteController : AlertController
    {
        public readonly IDbConnectionFactory _dbConnectionFactory;

        public ClienteController(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public IActionResult Index()
        {
            string sqlClientes = "SELECT * FROM dbo.\"Cliente\"";

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                var authors = connection.Query<Cliente>(sqlClientes).ToList();
                return View(authors);
            }
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear([Bind()] Cliente cliente)
        {
            string sqlInsert = "INSERT INTO dbo.\"Cliente\" (\"PrimerNombre\", \"PrimerApellido\", \"Edad\") VALUES (@PrimerNombre, @PrimerApellido, @Edad)";

            try
            {
                if (!ModelState.IsValid)
                {
                    Alert("El Modelo no es valido", NotificationType.warning);
                    return View();
                }

                using (var connection = _dbConnectionFactory.GetConnection())
                    {
                        var affectedRows = connection.Execute(sqlInsert,
                            new
                            {
                                PrimerNombre = cliente.PrimerNombre,
                                PrimerApellido = cliente.PrimerApellido,
                                Edad = cliente.Edad,

                            });

                        if (affectedRows > 0)
                        {
                            Alert("El registro se ha guardado con éxito", NotificationType.success);
                            return RedirectToAction("Index");
                        }
                    }
                
            }
            catch(Exception ex)
            {
                Alert(ex.Message, NotificationType.error);
            }

            return View(cliente);

        }

        public IActionResult Editar(int? IdCliente)
        {
            string sqlFind = $"SELECT * FROM dbo.\"Cliente\" WHERE \"IdCliente\"={IdCliente}";

            if (IdCliente == null)
            {
                return BadRequest();
            }

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                var cliente = connection.Query<Cliente>(sqlFind).FirstOrDefault();
                return View(cliente);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar([Bind()] Cliente cliente)
        {

            string sqlUpdate = $@"
                        UPDATE dbo.""Cliente"" 
                        SET ""PrimerNombre"" = @PrimerNombre, ""PrimerApellido"" = @PrimerApellido, ""Edad"" = @Edad 
                        WHERE ""IdCliente"" = {cliente.IdCliente}";

            try
            {
                if (!ModelState.IsValid)
                {
                    Alert("El Modelo no es valido", NotificationType.warning);

                }
                using (var connection = _dbConnectionFactory.GetConnection())
                {
                    var affectRow = connection.Execute(sqlUpdate,
                        new
                        {
                            PrimerNombre = cliente.PrimerNombre,
                            PrimerApellido = cliente.PrimerApellido,
                            Edad = cliente.Edad,
                        });

                    if (affectRow > 0)
                    {
                        Alert("El registro se ha actualizado con éxito", NotificationType.success);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch(Exception ex)
            {
                Alert(ex.Message, NotificationType.error);
            }
            return View(cliente);
        }

        public IActionResult Detalle(int? IdCliente)
        {
            // Verificamos si IdCliente es nulo
            if (IdCliente == null)
            {
                // Devolvemos un BadRequest si el IdCliente es nulo
                return BadRequest();

            }
            string sqlFind = $"SELECT * FROM dbo.\"Cliente\" WHERE \"IdCliente\" = {IdCliente}";

            using (var connection = _dbConnectionFactory.GetConnection())
            {
                var cliente = connection.Query<Cliente>(sqlFind).FirstOrDefault();

                // Verificamos si cliente es nulo
                if (cliente == null)
                {
                    // Devolvemos un NotFound si no se encontró el cliente
                    return NotFound();
                }

                // Devolvemos la vista con el cliente encontrado
                return View(cliente);
            }
        }

        [HttpPost]
        [Route("/Cliente/Eliminar/{idCliente}")]
        public IActionResult Eliminar(int? idCliente)
        {
            try
            {
                if (idCliente ==  null)
                {
                    return BadRequest();
                }

                string sqlDelete = $"DELETE FROM dbo.\"Cliente\" WHERE \"IdCliente\"={idCliente}";

                using(var connection = _dbConnectionFactory.GetConnection())
                {
                    var affectRow = connection.Execute(sqlDelete);

                    //if (affectRow > 0)
                    //{
                    //    //Alert("Se ha eliminado correctamente", NotificationType.success);
                    //}
                }

            }
            catch (Exception ex)
            {
                Alert(ex.Message, NotificationType.error);

            }

            return Json(new {success = true});
        }
    }
}

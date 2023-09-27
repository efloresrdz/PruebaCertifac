using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{

    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        [Route("api/Addenda/GetAll")]
        [EnableCors("AnotherPolicy")]
        public IActionResult GetAll()
        {

            ML.Result result = BL.Addenda.GetAll();

            if (result.Correct)
            {
                ML.Addenda addenda = new ML.Addenda();
                addenda.Addendas = result.Objects;


                return Ok(addenda.Addendas);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpDelete]
        [Route("api/Addenda/Delete")]
        [EnableCors("AnotherPolicy")]
        public ActionResult Delete(string guid) 
        {
        ML.Result result = BL.Addenda.Delete(guid);

            if (result.Correct)
            {

                return Ok(result);

            }
            else
            {
                return NotFound(result);
            }


        }


        [HttpGet]
        [Route("api/Addenda/GetById")]
        public ActionResult GetById(string guid) 
        {
        ML.Result result = BL.Addenda.GetById (guid);

            if (result.Correct)
            {
                ML.Addenda addenda = new ML.Addenda();

                addenda = (ML.Addenda)result.Object;
                return Ok(addenda);

            }
            else 
            {
            return NotFound(result);
            
            }  

        }

        [HttpPut]
        [Route("api/Addenda/Update")]
        public ActionResult Update([FromBody] ML.Addenda addenda)
        {
            ML.Result result = BL.Addenda.Update(addenda);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }



        [HttpDelete]
        [Route("api/Addenda/DeleteUpdate")]
        public ActionResult DeleteUpdate(string guid)
        {
            ML.Result result = BL.Addenda.DeleteUpdate(guid);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }










        [HttpPost]
        [Route("api/Addenda/Add")]
        public ActionResult Add([FromBody] ML.Addenda addenda)
        {
            ML.Result result = BL.Addenda.Add(addenda);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
























    }
}

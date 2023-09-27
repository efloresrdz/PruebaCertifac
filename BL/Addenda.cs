using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public class Addenda
    {

        public static ML.Result GetAll() 
        {
        ML.Result result = new ML.Result();

            try 
            {
                using (DL.CerAddendasContext context = new DL.CerAddendasContext()) 
                {

                    var query = (from ad in context.Addendas
                                 select new
                                 {
                                 IdAddenda = ad.IdAddenda,
                                 NombreAddenda=ad.NombreAddenda,
                                 Xml=ad.Xml,
                                 FechaModificacion =ad.FechaModificacion,
                                 Usuario=ad.Usuario,
                                 Estado =ad.Estado, 
                                 });
                result.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {

                        foreach (var item in query)
                        {
                            ML.Addenda addenda = new ML.Addenda();
                            addenda.IdAddenda = item.IdAddenda == null ? Guid.Empty : item.IdAddenda;
                            addenda.NombreAddenda = item.NombreAddenda==null ? "":item.NombreAddenda;
                            addenda.Xml = item.Xml == null ? "":item.Xml;
                            addenda.FechaModificacion =item.FechaModificacion==null ? "":item.FechaModificacion.ToString();
                            addenda.Usuario = item.Usuario == null ? "" : item.Usuario;
                            addenda.Estado = (bool)(item.Estado==null? false : item.Estado);
                            result.Objects.Add(addenda);

                        }
                        result.Correct = true;
                    }
                    else 
                    {
                    result.Correct = false;
                    result.ErrorMensaje = "Error no se encontro registo";
                    }
                }
            }catch (Exception ex) 
            {
            result.ErrorMensaje= ex.Message;
            result.Correct= false;
            }
        return result;
        }

        public static ML.Result Delete(string guid) 
        {
        ML.Result result = new ML.Result();

            try 
            {

                using (DL.CerAddendasContext context = new DL.CerAddendasContext()) 
                {
var query = (from ad in context.Addendas where ad.IdAddenda == Guid.Parse(guid) select  ad).First();
                    context.Addendas.Remove(query);
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {

                        result.Correct = true;


                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMensaje = "No se pudo eliminar";

                    }




                }

            
            }catch(Exception ex) 
            {
                result.Correct = false;
                result.ErrorMensaje = ex.Message;
            }

        
        return result;
        }

        public static ML.Result GetById(string guid) 
        {
        ML.Result result= new ML.Result();


            try
            {
                using (DL.CerAddendasContext context = new DL.CerAddendasContext()) 
                {
                    var query = (from ad in context.Addendas where ad.IdAddenda == Guid.Parse(guid)
                                 select new 
                                 {
                                     IdAddenda = ad.IdAddenda,
                                     NombreAddenda = ad.NombreAddenda,
                                     Xml = ad.Xml,
                                     FechaModificacion = ad.FechaModificacion,
                                     Usuario = ad.Usuario,
                                     Estado = ad.Estado,

                                 }
                                 ).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Addenda addenda = new ML.Addenda();
                        addenda.IdAddenda = query.IdAddenda == null ? Guid.Empty : query.IdAddenda;
                        addenda.NombreAddenda = query.NombreAddenda == null ? "" : query.NombreAddenda;
                        addenda.Xml = query.Xml == null ? "" : query.Xml;
                        addenda.FechaModificacion = query.FechaModificacion == null ? "" : query.FechaModificacion.ToString();
                        addenda.Usuario = query.Usuario == null ? "" : query.Usuario;
                        addenda.Estado = (bool)(query.Estado == null ? false : query.Estado);

                        result.Object = addenda;
                        result.Correct = true;
                    }
                    else 
                    {
                        result.Correct = false;
                        result.ErrorMensaje = "No se encontraron registros";

                    }
                }

            }
            catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMensaje = ex.Message;


            }

            return result;
        
        }

        public static ML.Result Update(ML.Addenda addenda) 
        {
        ML.Result result = new ML.Result();

            try
            {
                using (DL.CerAddendasContext context = new DL.CerAddendasContext()) 
                {
                var query = (from ad in context.Addendas where ad.IdAddenda== addenda.IdAddenda
                             select ad).SingleOrDefault();

                    if (query != null) 
                    {
                        query.NombreAddenda = addenda.NombreAddenda==null? "":addenda.NombreAddenda;


                        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(addenda.Xml);
                        string myBase64 = Convert.ToBase64String(myByte);


                        query.Xml = myBase64 == null ? "" : myBase64;



                        query.FechaModificacion= DateTime.Parse(addenda.FechaModificacion == null ? "" : addenda.FechaModificacion);
                        query.Usuario = addenda.Usuario == null ? "" : addenda.Usuario;
                        query.Estado = addenda.Estado==null?false:addenda.Estado;


                        int RowsAffected = context.SaveChanges();
                        if (RowsAffected > 0)
                        {

                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMensaje = "No se pudo actualizar";


                        }

                    }

                }
            }
            catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMensaje = ex.Message;

            }

        return result;
        }

        public static ML.Result Add(ML.Addenda addenda) 
        {
            ML.Result result = new ML.Result();

            try 
            {

                using(DL.CerAddendasContext context = new DL.CerAddendasContext()) 
                {
                    DL.Addenda Objaddenda = new DL.Addenda();
                    Objaddenda.IdAddenda = Guid.NewGuid();
                    Objaddenda.NombreAddenda = addenda.NombreAddenda==null?"":addenda.NombreAddenda;



                    byte[] myByte = System.Text.Encoding.UTF8.GetBytes(addenda.Xml);
                    string myBase64 = Convert.ToBase64String(myByte);




                    Objaddenda.Xml = myBase64 == null ? "": myBase64;
                   
                    Objaddenda.FechaModificacion = DateTime.Parse(addenda.FechaModificacion == null ? "" : addenda.FechaModificacion);
                    Objaddenda.Usuario = addenda.Usuario == null ? "" : addenda.Usuario;
                    Objaddenda.Estado = addenda.Estado==null?false:addenda.Estado;

                    context.Addendas.Add(Objaddenda);
                    int RowsAffected = context.SaveChanges();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMensaje = "No se pudo registar";

                    }


                }            
            
            }catch (Exception ex) 
            {
                result.Correct = false;
                result.ErrorMensaje = ex.Message;

            }

            return result;
        }

        public static ML.Result DeleteUpdate(string guid)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CerAddendasContext context = new DL.CerAddendasContext())
                {
                    var query = (from ad in context.Addendas
                                 where ad.IdAddenda == Guid.Parse(guid)
                                 select ad).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Addenda addenda = new ML.Addenda();


                        if (query.Estado == false)
                        {

                            addenda.Estado = true;
                        }
                        if (query.Estado == true) 
                        {

                            addenda.Estado = false;
                        }
                   




                        query.Estado = addenda.Estado;

                        /*----------------------------------------------------------------------------------------------*/
                        int RowsAffected = context.SaveChanges();
                        if (RowsAffected > 0)
                        {

                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMensaje = "No se pudo actualizar";


                        }

                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMensaje = ex.Message;

            }

            return result;
        }


    }
}
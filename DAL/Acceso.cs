using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using System.Text.RegularExpressions;

namespace DAL
{
    public class Acceso
    {
        private SqlConnection oCnn;
        public Acceso()
        {
            this.oCnn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
            this.oCnn.Open();
            string query = "USE [master]; SELECT count(*) FROM sys.databases WHERE name = 'PASTALINDA'";

            SqlCommand command = new SqlCommand(query, oCnn);
            int i = Convert.ToInt32(command.ExecuteScalar());

            if (i == 1)
            {
                oCnn.Close();
                oCnn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=PASTALINDA;Integrated Security=True");
            }
            else
            {
                string directorioEjecutable = AppDomain.CurrentDomain.BaseDirectory;
                string nombreArchivo = "PASTALINDA.sql";
                string rutaCompleta = Path.Combine(directorioEjecutable, nombreArchivo);
                string script = File.ReadAllText(rutaCompleta);
                string[] scriptBlocks = Regex.Split(script, @"\bGO\b", RegexOptions.IgnoreCase);
                foreach (string block in scriptBlocks)
                {
                    if (!string.IsNullOrWhiteSpace(block))
                    {
                        using (SqlCommand command2 = new SqlCommand(block, oCnn))
                        {
                            command2.ExecuteNonQuery();
                        }
                    }
                }
                oCnn.Close();
                oCnn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=PASTALINDA;Integrated Security=True");

            }


        }



        //Creo el objeto command
        SqlCommand cmd;

        // creo una funcion para saber el estado de la conexion
        public string TestConnection()
        {
            oCnn.Open();
            //si no uso el metodo Abrir puedo hacer el open 
            //conexion.Open();
            //Cerrar();
            if (oCnn.State == ConnectionState.Open)
            {
                return "Conexion a la BD OK";
            }
            else
            {
                return "No se pudo conectar a la BD, que pacho???";
            }
        }



        // Stored procedre Escritura
        public int EscribirSP(string SPName, Hashtable Params)
        {
            oCnn.Open();
            int a = 0;
            SqlCommand comm = new SqlCommand(SPName, oCnn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                //creo el data adapter le paso la consulta y la conexion
                SqlDataAdapter Da = new SqlDataAdapter(comm);
                if (Params != null)
                {
                    foreach (string key in Params.Keys)
                    {
                        comm.Parameters.AddWithValue(key, Params[key]);
                    }
                }

                a = comm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { //cierro la Conexion
                oCnn.Close();
            }
            return a;
        }



        // Stored procedre Lectura
        public DataTable LeerSP(string SPName, Hashtable Params)
        {
            oCnn.Open();
            DataTable tabla = new DataTable();
            SqlCommand comm = new SqlCommand(SPName, oCnn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                //creo el data adapter le paso la consulta y la conexion
                SqlDataAdapter Da = new SqlDataAdapter(comm);
                if (Params != null)
                {
                    foreach (string key in Params.Keys)
                    {
                        comm.Parameters.AddWithValue(key, Params[key]);
                    }
                }
                //lleno la tabla con el metodo fill

                int a = comm.ExecuteNonQuery();
                Da.Fill(tabla);

            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { //cierro la Conexion
                oCnn.Close();
            }
            return tabla;
        }


        public bool EscribirConsulta(string Consulta_SQL)
        {

            oCnn.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = oCnn;
            cmd.CommandText = Consulta_SQL;
            try
            {
                int respuesta = cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            finally
            { oCnn.Close(); }

        }


        // Stored procedre con return value
        public int LeerSPRT(string SPName, Hashtable Params)
        {
            oCnn.Open();

            SqlCommand comm = new SqlCommand(SPName, oCnn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                //creo el data adapter le paso la consulta y la conexion
                SqlDataAdapter Da = new SqlDataAdapter(comm);
                SqlParameter retValue = comm.Parameters.Add("@RetValue", SqlDbType.Int);
                retValue.Direction = ParameterDirection.ReturnValue;
                if (Params != null)
                {
                    foreach (string key in Params.Keys)
                    {
                        comm.Parameters.AddWithValue(key, Params[key]);
                    }
                }
                //lleno la tabla con el metodo fill

                int a = comm.ExecuteNonQuery();
                return (int)(retValue.Value);

            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { //cierro la Conexion
                oCnn.Close();
            }

        }


        // Stored procedure que permite pedir output variables
        //public void LeerSPRTO(string SPName, Hashtable Params, ref Hashtable ReturnValues )
        public SqlParameterCollection LeerSPRTO(string SPName, List<SqlParameter> parameters)
        {
            oCnn.Open();
            // ReturnValues = new Hashtable();
            SqlCommand comm = new SqlCommand(SPName, oCnn);
            comm.CommandType = CommandType.StoredProcedure;

            try
            {
                //creo el data adapter le paso la consulta y la conexion
                SqlDataAdapter Da = new SqlDataAdapter(comm);
                //SqlParameter retValue = comm.Parameters.Add("@RetValue", SqlDbType.Int);
                //retValue.Direction = ParameterDirection.ReturnValue;
                if (parameters != null)
                {
                    foreach (SqlParameter key in parameters)
                    {
                        comm.Parameters.Add(key);
                    }
                }
                //lleno la tabla con el metodo fill

                int a = comm.ExecuteNonQuery();
                /*
                foreach (string key in ReturnValues.Keys)
                {
                    SqlParameter retValue = comm.Parameters.Add("@RetValue", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.ReturnValue;
                    ReturnValues[key] = comm.Parameters[key].Value.ToString();
                }
                ReturnValues.Add("@RetValues", retValue.Value.ToString());
                */
                
                return comm.Parameters;

            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { //cierro la Conexion
                oCnn.Close();
            }

        }

    }
}

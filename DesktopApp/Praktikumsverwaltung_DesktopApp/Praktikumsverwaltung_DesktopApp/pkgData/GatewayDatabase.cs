using MongoDB.Bson;         // !!!
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace Praktikumsverwaltung_DesktopApp.pkgData
{
    class GatewayDatabase
    {
        private static GatewayDatabase instance = null;

        // user
        public String idUserBsonId { get; set; }         // Id of Pupil or Teacher
        public String idUserClassBsonId { get; set; }        // Class of Pupil
        public String idUserDepartmentBsonId { get; set; }       // Department of Pupil or Teacher

        // Required for generating "special" main window for teacher who are admin (accepting entries)
        public bool IsAdmin { get; set; }
        public bool IsPupil { get; set; }
        public bool IsTeacher { get; set; }

        /***********************/
        private string urlWebService = "http://192.168.195.126:8080/PraktikumsverwaltungWebService/resources";     //schule: 192.168.195.246  daheim: 10.0.0.19
        private static readonly HttpClient client = new HttpClient();

        // Singleton
        public static GatewayDatabase newInstance()
        {
            if (instance == null)
            {
                instance = new GatewayDatabase();                
            }
            return instance;
        }

        // GETs results of the webservice
        private string GETWebService(string myPath)
        {
            string responseText;
            Encoding encoding = new UTF8Encoding();

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(myPath));       //Create a HttpWebRequest object  
                httpWebRequest.Method = "GET";      //Set the Method  
                //httpWebRequest.KeepAlive = true;        //Set Keep Alive

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();        //Get the Response 

                // reads every byte of the response message
                using (var reader = new System.IO.StreamReader(httpWebResponse.GetResponseStream(), encoding))
                {
                    responseText = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_GETWebService: " + ex.Message);
            }

            return responseText;
        }

        // POSTs results on the webservice
        private string POSTWebService(string myPath, string jsonString)
        {
            string responseText;
            Encoding encoding = new UTF8Encoding();

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(myPath));       //Create a HttpWebRequest object  
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";      //Set the Method

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();        //Get the Response 

                // reads every byte of the response message
                using (var reader = new System.IO.StreamReader(httpWebResponse.GetResponseStream(), encoding))
                {
                    responseText = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_POSTWebService: " + ex.Message);
            }

            return responseText;
        }

        // PUTs results on the webservice
        private string PUTWebService(string myPath, string jsonString)
        {
            string responseText;
            Encoding encoding = new UTF8Encoding();

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(myPath));       //Create a HttpWebRequest object  
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PUT";      //Set the Method

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();        //Get the Response 

                // reads every byte of the response message
                using (var reader = new System.IO.StreamReader(httpWebResponse.GetResponseStream(), encoding))
                {
                    responseText = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_PUTWebService: " + ex.Message);
            }

            return responseText;
        }

        // DELETE's something on the webservice (similiar to GETWebService except the httpwebrequest.method)
        private string DELETEWebService(string myPath)
        {
            string responseText;
            Encoding encoding = new UTF8Encoding();

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(myPath));       //Create a HttpWebRequest object  
                httpWebRequest.Method = "DELETE";      //Set the Method

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();        //Get the Response 

                // reads every byte of the response message
                using (var reader = new System.IO.StreamReader(httpWebResponse.GetResponseStream(), encoding))
                {
                    responseText = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_DELETEWebService: " + ex.Message);
            }

            return responseText;
        }

        // WebService checks both Login possibilities, Pupil and Teacher
        public bool IsLoginOk(string username, string password)
        {
            bool successful = false;
            string myPath, jsonString;

            try
            {
                myPath = this.urlWebService + "/Login?username=" + username + "&password=" + password;       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                Pupil pupilLogin = JsonConvert.DeserializeObject<Pupil>(jsonString);

                // Wenn es null ist, bleibt successful auf false
                if (pupilLogin != null)
                {
                    // Wenn es ein Pupil ist, hat er das Feld IdClass im jsonString, wenn nicht, ist es ein Teacher
                    if (pupilLogin.IdClass != null)
                    {
                        this.idUserBsonId = pupilLogin.Id;
                        this.idUserClassBsonId = pupilLogin.IdClass;
                        this.idUserDepartmentBsonId = pupilLogin.IdDepartment;
                        this.IsPupil = true;
                        this.IsTeacher = false;
                        this.IsAdmin = false;

                        successful = true;
                    }
                    else
                    {
                        Teacher teacherLogin = JsonConvert.DeserializeObject<Teacher>(jsonString);

                        this.idUserBsonId = teacherLogin.Id;
                        this.IsPupil = false;
                        this.IsTeacher = true;

                        if (teacherLogin.IsAdmin)
                        {
                            this.IsAdmin = true;
                        }

                        successful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_IsLoginOk: " + ex.Message);
            }           

            return successful;
        }

        public List<Entry> GetAllEntries()
        {
            List<Entry> listEntries;
            string myPath, jsonString;

            try
            {
                myPath = this.urlWebService + "/Entry";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listEntries = JsonConvert.DeserializeObject<List<Entry>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_GetAllEntries: " + ex.Message);
            }

            return listEntries;
        }

        public List<Entry> GetAllOwnEntries()
        {
            List<Entry> listEditableEntries;
            string myPath, jsonString;

            try
            {
                myPath = this.urlWebService + "/Entry/" + this.idUserBsonId;       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listEditableEntries = JsonConvert.DeserializeObject<List<Entry>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_GetAllEditableEntries: " + ex.Message);
            }

            return listEditableEntries;
        }

        public List<Pupil> GetAllActivePupils()
        {
            List<Pupil> listPupils;
            string myPath, jsonString;

            try
            {
                myPath = this.urlWebService + "/Pupil";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listPupils = JsonConvert.DeserializeObject<List<Pupil>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_GetAllActivePupils: " + ex.Message);
            }

            return listPupils;
        }

        public List<Teacher> GetAllActiveTeachers()
        {
            List<Teacher> listTeacher;
            string myPath, jsonString;

            try
            {
                myPath = this.urlWebService + "/Teacher";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listTeacher = JsonConvert.DeserializeObject<List<Teacher>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_GetAllActiveTeachers: " + ex.Message);
            }

            return listTeacher;
        }

        public List<Company> GetAllCompanies()
        {
            List<Company> listCompanies;
            string myPath, jsonString;

            try
            {
                myPath = this.urlWebService + "/Company";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listCompanies = JsonConvert.DeserializeObject<List<Company>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_GetAllCompanies: " + ex.Message);
            }

            return listCompanies;
        }

        public bool AddEntry(Entry entry)
        {
            bool successful = false;
            string myPath, mySpecialJavaAndMongoDbDouble, jsonStringResponse;

            try
            {
                // setting the id's here, because in AddEntryWindow the id's aren't stored
                entry.Id = (ObjectId.GenerateNewId()).ToString();
                entry.IdPupil = this.idUserBsonId;
                entry.IdClass = this.idUserClassBsonId;

                myPath = this.urlWebService + "/Entry";       // path to the webservice with the params

                // !!!!!!!!!!!!! C# double is with a ',' BUT IN JAVA a double is with a '.'
                mySpecialJavaAndMongoDbDouble = entry.Salary.ToString();
                mySpecialJavaAndMongoDbDouble = mySpecialJavaAndMongoDbDouble.Replace(',', '.');

                // !!!!!!! MongoDB salary (=double) needs always a type like a float e.g. 1300.0 and NOT only 1300 => otherwise exception
                if (mySpecialJavaAndMongoDbDouble.Contains(".") == false)
                {
                    mySpecialJavaAndMongoDbDouble += ".0";
                }

                // !!! Creating own json String because of the date and double
                StringBuilder jsonStringBuilder = new StringBuilder();
                jsonStringBuilder.Append("{ \"_id\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.Id);
                jsonStringBuilder.Append("\" }, \"startDate\" : { \"$date\" : ");
                jsonStringBuilder.Append((entry.StartDate - new DateTime(1970, 1, 1)).TotalMilliseconds);     // to get the milliseconds of the date. Needed because of java. Furthermore subtract 1970 because in Java, Date starts at the year 0 and in c# year starts at 1970. (or vice versa)
                jsonStringBuilder.Append(" }, \"endDate\" : { \"$date\" : ");
                jsonStringBuilder.Append((entry.EndDate - new DateTime(1970, 1, 1)).TotalMilliseconds);       // to get the milliseconds of the date. Needed because of java. Furthermore subtract 1970 because in Java, Date starts at the year 0 and in c# year starts at 1970. (or vice versa)
                jsonStringBuilder.Append(" }, \"salary\" : ");
                jsonStringBuilder.Append(mySpecialJavaAndMongoDbDouble);
                jsonStringBuilder.Append(", \"title\" : \"");
                jsonStringBuilder.Append(entry.Title);
                jsonStringBuilder.Append("\", \"description\" : \"");
                jsonStringBuilder.Append(entry.Description);
                jsonStringBuilder.Append("\", \"allowedTeacher\" : ");
                jsonStringBuilder.Append(entry.AllowedTeacher.ToString().ToLower());            // ToLower() because mongodb needs true/false and not True/False
                jsonStringBuilder.Append(", \"allowedAV\" : ");
                jsonStringBuilder.Append(entry.AllowedTeacher.ToString().ToLower());
                jsonStringBuilder.Append(", \"seenByAdmin\" : ");
                jsonStringBuilder.Append(entry.SeenByAdmin.ToString().ToLower());
                jsonStringBuilder.Append(", \"adminNote\" : \"");
                jsonStringBuilder.Append(entry.AdminNote);
                jsonStringBuilder.Append("\", \"idPupil\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdPupil);
                jsonStringBuilder.Append("\" }, \"idCompany\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdCompany);
                jsonStringBuilder.Append("\" }, \"idClass\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdClass);
                jsonStringBuilder.Append("\" } }");
                                
                jsonStringResponse = this.POSTWebService(myPath, jsonStringBuilder.ToString());

                if (jsonStringResponse.Equals("ok"))
                {
                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_AddEntry: " + ex.Message);
            }

            return successful;
        }

        public List<Entry> GetAllUnacceptedEntries()
        {
            List<Entry> listUnacceptedEntries;
            string myPath, jsonString;

            try
            {
                myPath = this.urlWebService + "/EntryAdmin";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listUnacceptedEntries = JsonConvert.DeserializeObject<List<Entry>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_GetAllUnacceptedEntries: " + ex.Message);
            }

            return listUnacceptedEntries;
        }

        public bool UpdateEntry(Entry editedEntry)
        {
            bool successful = false;
            string myPath, mySpecialJavaAndMongoDbDouble, jsonStringResponse;

            try
            {
                myPath = this.urlWebService + "/Entry";       // path to the webservice with the params

                // !!!!!!!!!!!!! C# double is with a ',' BUT IN JAVA a double is with a '.'
                mySpecialJavaAndMongoDbDouble = editedEntry.Salary.ToString();
                mySpecialJavaAndMongoDbDouble = mySpecialJavaAndMongoDbDouble.Replace(',', '.');

                // !!!!!!! MongoDB salary (=double) needs always a type like a float e.g. 1300.0 and NOT only 1300 => otherwise exception
                if (mySpecialJavaAndMongoDbDouble.Contains(".") == false)
                {
                    mySpecialJavaAndMongoDbDouble += ".0";
                }

                // !!! Creating own json String because of the date and double
                StringBuilder jsonStringBuilder = new StringBuilder();
                jsonStringBuilder.Append("{ \"_id\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(editedEntry.Id);
                jsonStringBuilder.Append("\" }, \"startDate\" : { \"$date\" : ");
                jsonStringBuilder.Append((editedEntry.StartDate - new DateTime(1970, 1, 1)).TotalMilliseconds);     // to get the milliseconds of the date. Needed because of java. Furthermore subtract 1970 because in Java, Date starts at the year 0 and in c# year starts at 1970. (or vice versa)
                jsonStringBuilder.Append(" }, \"endDate\" : { \"$date\" : ");
                jsonStringBuilder.Append((editedEntry.EndDate - new DateTime(1970, 1, 1)).TotalMilliseconds);       // to get the milliseconds of the date. Needed because of java. Furthermore subtract 1970 because in Java, Date starts at the year 0 and in c# year starts at 1970. (or vice versa)
                jsonStringBuilder.Append(" }, \"salary\" : ");
                jsonStringBuilder.Append(mySpecialJavaAndMongoDbDouble);
                jsonStringBuilder.Append(", \"title\" : \"");
                jsonStringBuilder.Append(editedEntry.Title);
                jsonStringBuilder.Append("\", \"description\" : \"");
                jsonStringBuilder.Append(editedEntry.Description);
                jsonStringBuilder.Append("\", \"allowedTeacher\" : ");
                jsonStringBuilder.Append(editedEntry.AllowedTeacher.ToString().ToLower());            // ToLower() because mongodb needs true/false and not True/False
                jsonStringBuilder.Append(", \"allowedAV\" : ");
                jsonStringBuilder.Append(editedEntry.AllowedTeacher.ToString().ToLower());
                jsonStringBuilder.Append(", \"seenByAdmin\" : ");
                jsonStringBuilder.Append(editedEntry.SeenByAdmin.ToString().ToLower());
                jsonStringBuilder.Append(", \"adminNote\" : \"");
                jsonStringBuilder.Append(editedEntry.AdminNote);
                jsonStringBuilder.Append("\", \"idPupil\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(editedEntry.IdPupil);
                jsonStringBuilder.Append("\" }, \"idCompany\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(editedEntry.IdCompany);
                jsonStringBuilder.Append("\" }, \"idClass\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(editedEntry.IdClass);
                jsonStringBuilder.Append("\" } }");

                jsonStringResponse = this.PUTWebService(myPath, jsonStringBuilder.ToString());

                if (jsonStringResponse.Equals("ok"))
                {
                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_UpdateEntry: " + ex.Message);
            }

            return successful;
        }

        public Entry GetEntry(string idOfEntry)
        {
            Entry entry;
            string myPath, jsonString;

            try
            {
                myPath = this.urlWebService + "/EntryDetail/" + idOfEntry;       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                entry = JsonConvert.DeserializeObject<Entry>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_GetAllEditableEntries: " + ex.Message);
            }

            return entry;
        }

        private string MakeSpecialEntryJsonString(Entry entry)
        {
            string jsonString = null, mySpecialJavaAndMongoDbDouble;

            try
            {
                // !!!!!!!!!!!!! C# double is with a ',' BUT IN JAVA a double is with a '.'
                mySpecialJavaAndMongoDbDouble = entry.Salary.ToString();
                mySpecialJavaAndMongoDbDouble = mySpecialJavaAndMongoDbDouble.Replace(',', '.');

                // !!!!!!! MongoDB salary (=double) needs always a type like a float e.g. 1300.0 and NOT only 1300 => otherwise exception
                if (mySpecialJavaAndMongoDbDouble.Contains(".") == false)
                {
                    mySpecialJavaAndMongoDbDouble += ".0";
                }

                // !!! Creating own json String because of the date and double
                StringBuilder jsonStringBuilder = new StringBuilder();
                jsonStringBuilder.Append("{ \"_id\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.Id);
                jsonStringBuilder.Append("\" }, \"startDate\" : { \"$date\" : ");
                jsonStringBuilder.Append((entry.StartDate - new DateTime(1970, 1, 1)).TotalMilliseconds);     // to get the milliseconds of the date. Needed because of java. Furthermore subtract 1970 because in Java, Date starts at the year 0 and in c# year starts at 1970. (or vice versa)
                jsonStringBuilder.Append(" }, \"endDate\" : { \"$date\" : ");
                jsonStringBuilder.Append((entry.EndDate - new DateTime(1970, 1, 1)).TotalMilliseconds);       // to get the milliseconds of the date. Needed because of java. Furthermore subtract 1970 because in Java, Date starts at the year 0 and in c# year starts at 1970. (or vice versa)
                jsonStringBuilder.Append(" }, \"salary\" : ");
                jsonStringBuilder.Append(mySpecialJavaAndMongoDbDouble);
                jsonStringBuilder.Append(", \"title\" : \"");
                jsonStringBuilder.Append(entry.Title);
                jsonStringBuilder.Append("\", \"description\" : \"");
                jsonStringBuilder.Append(entry.Description);
                jsonStringBuilder.Append("\", \"allowedTeacher\" : ");
                jsonStringBuilder.Append(entry.AllowedTeacher.ToString().ToLower());            // ToLower() because mongodb needs true/false and not True/False
                jsonStringBuilder.Append(", \"allowedAV\" : ");
                jsonStringBuilder.Append(entry.AllowedTeacher.ToString().ToLower());
                jsonStringBuilder.Append(", \"seenByAdmin\" : ");
                jsonStringBuilder.Append(entry.SeenByAdmin.ToString().ToLower());
                jsonStringBuilder.Append(", \"adminNote\" : \"");
                jsonStringBuilder.Append(entry.AdminNote);
                jsonStringBuilder.Append("\", \"idPupil\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdPupil);
                jsonStringBuilder.Append("\" }, \"idCompany\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdCompany);
                jsonStringBuilder.Append("\" }, \"idClass\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdClass);
                jsonStringBuilder.Append("\" } }");

                jsonString = jsonStringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_MakeSpecialEntryJsonString: " + ex.Message);
            }

            return jsonString;
        }

        public bool DeleteEntry(string idOfEntry)
        {
            bool successful = false;
            string myPath, jsonResponse;

            try
            {
                myPath = this.urlWebService + "/Entry?entryId=" + idOfEntry;       // path to the webservice with the params
                jsonResponse = this.DELETEWebService(myPath);
                
                if (jsonResponse.Equals("ok"))
                {
                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_IsLoginOk: " + ex.Message);
            }

            return successful;
        }

        public bool AddCompany(Company company)
        {
            bool successful = false;
            string myPath, jsonString, jsonStringResponse;

            try
            {
                // setting the id's here, because in AddCompanyWindow the id isn't stored
                company.Id = (ObjectId.GenerateNewId()).ToString();

                myPath = this.urlWebService + "/Company";       // path to the webservice with the params                
                //jsonString = JsonConvert.SerializeObject(company);      


                // !!! Creating own json String because of the date and double
                StringBuilder jsonStringBuilder = new StringBuilder();
                jsonStringBuilder.Append("{ \"_id\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(company.Id);
                jsonStringBuilder.Append("\" }, \"name\" : \"");
                jsonStringBuilder.Append(company.Name);
                jsonStringBuilder.Append("\", \"location\" : \"");
                jsonStringBuilder.Append(company.Location);
                jsonStringBuilder.Append("\", \"numberOfEmployees\" : ");
                jsonStringBuilder.Append(company.NumberOfEmployees);
                jsonStringBuilder.Append(", \"contactPerson\" : \"");
                jsonStringBuilder.Append(company.ContactPerson);
                jsonStringBuilder.Append("\" }");

                jsonString = jsonStringBuilder.ToString();

                jsonStringResponse = this.POSTWebService(myPath, jsonString);

                if (jsonStringResponse.Equals("ok"))
                {
                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GatewayDB_AddCompany: " + ex.Message);
            }

            return successful;
        }
    }
}

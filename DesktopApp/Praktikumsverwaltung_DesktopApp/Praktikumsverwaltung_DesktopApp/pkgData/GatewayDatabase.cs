﻿using MongoDB.Bson;         // !!!
using MongoDB.Bson.Serialization;
using MongoDB.Driver;       // !!!
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        private string urlWebService = "http://10.0.0.19:8080/PraktikumsverwaltungWebService/resources";     //schule: 192.168.195.61  daheim: 10.0.0.19
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
            var encoding = ASCIIEncoding.ASCII;

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
                throw new Exception("Error in GETWebService: " + ex.Message);
            }

            return responseText;
        }

        // POSTs results on the webservice
        private string POSTWebService(string myPath, string jsonString)
        {
            string responseText;
            var encoding = ASCIIEncoding.ASCII;

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
                throw new Exception("Error in POSTWebService: " + ex.Message);
            }

            return responseText;
        }

        // POSTs results on the webservice
        private string PUTWebService(string myPath, string jsonString)
        {
            string responseText;
            var encoding = ASCIIEncoding.ASCII;

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
                throw new Exception("Error in PUTWebService: " + ex.Message);
            }

            return responseText;
        }

        // WebService checks both Login possibilities, Pupil and Teacher
        public bool IsLoginOk(string username, string password)
        {
            bool successful = false, isPupil = false;
            string myPath, jsonString;
            var encoding = ASCIIEncoding.ASCII;

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
                throw new Exception("Error in IsLoginOk: " + ex.Message);
            }           

            return successful;
        }

        public List<Entry> GetAllEntries()
        {
            List<Entry> listEntries;

            string myPath, jsonString;
            var encoding = ASCIIEncoding.ASCII;

            try
            {
                myPath = this.urlWebService + "/Entry";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listEntries = JsonConvert.DeserializeObject<List<Entry>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllEntries: " + ex.Message);
            }

            return listEntries;
        }

        public List<Entry> GetAllOwnEntries()
        {
            List<Entry> listEditableEntries;

            string myPath, jsonString;
            var encoding = ASCIIEncoding.ASCII;

            try
            {
                myPath = this.urlWebService + "/Entry/" + this.idUserBsonId;       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listEditableEntries = JsonConvert.DeserializeObject<List<Entry>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllEditableEntries: " + ex.Message);
            }

            return listEditableEntries;
        }

        public List<Pupil> GetAllActivePupils()
        {
            List<Pupil> listPupils;

            string myPath, jsonString;
            var encoding = ASCIIEncoding.ASCII;

            try
            {
                myPath = this.urlWebService + "/Pupil";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listPupils = JsonConvert.DeserializeObject<List<Pupil>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllActivePupils: " + ex.Message);
            }

            return listPupils;
        }

        public List<Teacher> GetAllActiveTeachers()
        {
            List<Teacher> listTeacher;

            string myPath, jsonString;
            var encoding = ASCIIEncoding.ASCII;

            try
            {
                myPath = this.urlWebService + "/Teacher";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listTeacher = JsonConvert.DeserializeObject<List<Teacher>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllActiveTeachers: " + ex.Message);
            }

            return listTeacher;
        }

        public List<Company> GetAllCompanies()
        {
            List<Company> listCompanies;

            string myPath, jsonString;
            var encoding = ASCIIEncoding.ASCII;

            try
            {
                myPath = this.urlWebService + "/Company";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listCompanies = JsonConvert.DeserializeObject<List<Company>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllCompanies: " + ex.Message);
            }

            return listCompanies;
        }

        public bool AddEntry(Entry entry)
        {
            bool successful = false;
            string myPath, jsonStringResponse;
            var encoding = ASCIIEncoding.ASCII;

            try
            {
                // setting the id's here, because in AddEntryWindow the id's aren't stored
                entry.Id = (ObjectId.GenerateNewId()).ToString();
                entry.IdPupil = this.idUserBsonId;
                entry.IdClass = this.idUserClassBsonId;
                entry.IdCompany = this.idUserDepartmentBsonId;

                myPath = this.urlWebService + "/Entry";       // path to the webservice with the params

                StringBuilder jsonStringBuilder = new StringBuilder();
                jsonStringBuilder.Append("{ \"_id\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.Id);
                jsonStringBuilder.Append("\" }, \"startDate\" : { \"$date\" : ");
                jsonStringBuilder.Append((entry.StartDate - new DateTime(1970, 1, 1)).TotalMilliseconds);     // to get the milliseconds of the date
                jsonStringBuilder.Append(" }, \"endDate\" : { \"$date\" : ");
                jsonStringBuilder.Append((entry.EndDate - new DateTime(1970, 1, 1)).TotalMilliseconds);       // to get the milliseconds of the date (long)(entry.EndDate - new DateTime(1970, 1, 1)).TotalMilliseconds
                jsonStringBuilder.Append(" }, \"salary\" : ");
                jsonStringBuilder.Append(entry.Salary);
                jsonStringBuilder.Append(", \"title\" : \"");
                jsonStringBuilder.Append(entry.Title);
                jsonStringBuilder.Append("\", \"description\" : \"");
                jsonStringBuilder.Append(entry.Description);
                jsonStringBuilder.Append("\", \"allowedTeacher\" : ");
                jsonStringBuilder.Append(entry.AllowedTeacher.ToString().ToLower());
                jsonStringBuilder.Append(", \"allowedAV\" : ");
                jsonStringBuilder.Append(entry.AllowedAV.ToString().ToLower());
                jsonStringBuilder.Append(", \"idPupil\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdPupil);
                jsonStringBuilder.Append("\" }, \"idCompany\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdCompany);
                jsonStringBuilder.Append("\" }, \"idClass\" : { \"$oid\" : \"");
                jsonStringBuilder.Append(entry.IdClass);
                jsonStringBuilder.Append("\" } }");


                //{ "_id" : { "$oid" : "5a621f5554fc3d1de88cb089" }, "startDate" : { "$date" : 1499644800000 }, "endDate" : { "$date" : 1504224000000 }, "salary" : 550.0, "title" : "sadgaffadfgfgsdf", "description" : "sdfsds", "allowedTeacher" : false, "allowedAV" : false, "idPupil" : { "$oid" : "5a1d3eba2d19782a01d75dc0" }, "idCompany" : { "$oid" : "5a1d39152d19782a01d75db5" }, "idClass" : { "$oid" : "5a1d3e092d19782a01d75dbe" } }
                //jsonString = JsonConvert.SerializeObject(entry);                
                //jsonString = entry.ToJson();
                jsonStringResponse = this.POSTWebService(myPath, jsonStringBuilder.ToString());

                string result = JsonConvert.DeserializeObject<String>(jsonStringResponse);

                if (result.Equals("ok"))
                {
                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddEntry: " + ex.Message);
            }

            return successful;
        }

        public List<Entry> GetAllUnacceptedEntries()
        {
            List<Entry> listUnacceptedEntries;

            string myPath, jsonString;
            var encoding = ASCIIEncoding.ASCII;

            try
            {
                myPath = this.urlWebService + "/EntryAdmin";       // path to the webservice with the params
                jsonString = this.GETWebService(myPath);

                listUnacceptedEntries = JsonConvert.DeserializeObject<List<Entry>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllUnacceptedEntries: " + ex.Message);
            }

            return listUnacceptedEntries;
        }

        public bool UpdateEntry(Entry editedEntry)
        {
            bool successful = false;
            string myPath, jsonStringResponse;
            var encoding = ASCIIEncoding.ASCII;

            try
            {
                myPath = this.urlWebService + "/EntryDetail";       // path to the webservice with the params
                jsonStringResponse = this.PUTWebService(myPath, editedEntry.ToJson());

                string result = JsonConvert.DeserializeObject<String>(jsonStringResponse);

                if (result.Equals("ok"))
                {
                    successful = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UpdateEntry: " + ex.Message);
            }

            return successful;
        }
    }
}

package com.obercoder.praktikumsverwaltung_androidapp.pkgController;


import android.os.AsyncTask;
import android.util.Log;

import com.google.gson.Gson;
import com.obercoder.praktikumsverwaltung_androidapp.pkgData.Database;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;

/**
 * Created by Sasa on 01.10.2017.
 */

public class ControllerPupil extends AsyncTask<String, Void, String> {
    private static final String URL = Database.getUrl();
    private static final String contentType = Database.getContentType();
    private Gson gson = null;

    @Override
    protected String doInBackground(String... command) {
        BufferedReader reader = null;
        String response = null;
        java.net.URL url = null;
        BufferedWriter writer = null;
        String retVal = null;
        gson = new Gson();

        try{
            if(command[0].compareTo("GET") == 0) {
                if (command[1].compareTo("Pupil") == 0) {
                    url = new URL(URL + command[1]);
                    URLConnection conn = url.openConnection();
                    Log.d("TESTURL", conn.getURL().toString());

                    reader = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                    StringBuilder sb = new StringBuilder();
                    String line = null;
                    while ((line = reader.readLine()) != null) {
                        sb.append(line);
                        Log.d("READERWHILE", line);
                    }

                    response = sb.toString();
                    reader.close();
                }
                else if (command[1].compareTo("Login") == 0) {
                    url = new URL(URL + command[1] + command[2]);
                    URLConnection conn = url.openConnection();

                    reader = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                    StringBuilder sb = new StringBuilder();
                    String line = null;
                    while ((line = reader.readLine()) != null) {
                        sb.append(line);
                        Log.d("READERWHILE2", line);
                    }

                    response = sb.toString();
                    reader.close();
                }
            }
        else if(command[0].equals("PUT")){
                    String updatedMatch = gson.toJson(command[2]);
                    url = new URL(URL + command[1]);

                    HttpURLConnection urlConnection = (HttpURLConnection)url.openConnection();
                    urlConnection.setDoOutput(true);
                    urlConnection.setRequestMethod(command[0].toString());
                    urlConnection.setRequestProperty("Content-Type", contentType);

                    byte[] outputBytes = updatedMatch.getBytes("UTF-8");
                    urlConnection.setRequestProperty("Content-Length", Integer.toString(outputBytes.length));
                    OutputStream os = urlConnection.getOutputStream();
                    os.write(outputBytes);

                    response = Integer.toString(urlConnection.getResponseCode());
            }
        } catch(Exception ex){
            ex.printStackTrace();
        }
        return response;
    }
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import java.util.ArrayList;
import javax.faces.bean.ApplicationScoped;
import javax.faces.bean.ManagedBean;
import pkgMisc.Controller;

/**
 *
 * @author Jakob
 */
@ManagedBean(eager = true)
@ApplicationScoped
public class Database {
    private Controller controller;
    private Gson gson;
    private Person currentPerson;
    private ArrayList<Entry> allEntries;
    
    public Database() {
        try {
            gson = new Gson();
            controller = new Controller("http://172.16.40.139:8080/PraktikumsverwaltungWebService/resources/");
        } catch (Exception ex) {
            System.out.println("error constructor database: " + ex.getMessage());
        }
    }
    
    public boolean login(String username, String password) throws Exception {
        boolean isValid = false;
        currentPerson = null;
        String strFromServer = controller.login(username, password);
        currentPerson = gson.fromJson(strFromServer, Pupil.class);
        if (currentPerson != null && ((Pupil)currentPerson).getIdClass() != null) {
            isValid = true;
        } else if (currentPerson != null) {
            currentPerson = gson.fromJson(strFromServer, Teacher.class);;
            isValid = true;
        }
        
        return isValid;
    }

    public Person getCurrentPerson() {
        return currentPerson;
    }

    public void setCurrentPerson(Person currentPerson) {
        this.currentPerson = currentPerson;
    }
    
    public void loadEntries() throws Exception {
        allEntries = gson.fromJson(controller.getEntries(), new TypeToken<ArrayList<Entry>>(){}.getType());
    }
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgBeans;

import javax.faces.application.FacesMessage;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;
import javax.faces.context.FacesContext;
import javax.servlet.http.HttpSession;
import pkgData.Database;
import pkgMisc.SessionUtils;

/**
 *
 * @author Jakob
 */
@ManagedBean
public class LoginBean {
    
    @ManagedProperty(value="#{database}")
    private Database database;
    private String username;
    private String password;
        
    public String checkLogin() {
        String retValue = "";
        try {            
            if(database.login(username, password)) {
                HttpSession session = SessionUtils.getSession();
                session.setAttribute("username", database.getCurrentPerson().getUsername());
                retValue = "mainPage";
            } else {
                FacesContext.getCurrentInstance().addMessage(null,
                    new FacesMessage(FacesMessage.SEVERITY_WARN, "Incorrect username or password", "Please enter correct username and password"));
                retValue = "login";
            }
        } catch (Exception ex) {
            System.out.println("error in checkLogin: " + ex.getMessage());
        }
        return retValue;
    }
    
    public String logout() {
        HttpSession session = SessionUtils.getSession();
        session.invalidate();
        database.setCurrentPerson(null);
        return "login";
    }

    public void setDatabase(Database database){
        this.database = database;
    }
    
    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
    
    
}

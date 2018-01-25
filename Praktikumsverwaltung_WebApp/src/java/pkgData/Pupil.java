/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author schueler
 */
@XmlRootElement
public class Pupil extends Person{
    private String idDepartment;
    private String idClass;

    public Pupil() {
        
    }
    
    public Pupil (String username, String password) {
        super(username, password);
    }

    public Pupil(String id, String username, String password, String firstName, String lastname, String currentForm, String email, String idDepartment, String idClass, boolean isActive) {
        super(id, username, password, firstName, lastname, email, isActive);
        this.idDepartment = idDepartment;
        this.idClass = idClass;
    }

    public String getIdDepartment() {
        return idDepartment;
    }

    public void setIdDepartment(String idDepartment) {
        this.idDepartment = idDepartment;
    }

    public String getIdClass() {
        return idClass;
    }

    public void setIdClass(String idClass) {
        this.idClass = idClass;
    }
    
    
}
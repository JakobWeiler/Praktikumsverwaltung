/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import javax.xml.bind.annotation.XmlRootElement;
import org.bson.types.ObjectId;

/**
 *
 * @author schueler
 */
@XmlRootElement
public class Pupil extends Person{
    private ObjectId idDepartment;
    private ObjectId idClass;

    public Pupil() {
        
    }
    
    public Pupil (String username, String password) {
        super(username, password);
    }

    public Pupil(ObjectId id, String username, String password, String firstName, String lastname, String currentForm, String email, ObjectId idDepartment, ObjectId idClass, boolean isActive) {
        super(id, username, password, firstName, lastname, email, isActive);
        this.idDepartment = idDepartment;
        this.idClass = idClass;
    }

    public ObjectId getIdDepartment() {
        return idDepartment;
    }

    public void setIdDepartment(ObjectId idDepartment) {
        this.idDepartment = idDepartment;
    }

    public ObjectId getIdClass() {
        return idClass;
    }

    public void setIdClass(ObjectId idClass) {
        this.idClass = idClass;
    }
    
    
}
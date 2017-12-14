/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkgData;

import org.bson.types.ObjectId;

/**
 *
 * @author schueler
 */
public class Teacher extends Person {
    private boolean isAdmin;
    private boolean isActive;

    public Teacher() {
    }

    public Teacher(ObjectId id, String username, String password, String firstName, String lastname, String email, boolean isAdmin, boolean isActive) {
        super(id, username, password, firstName, lastname, email);
        this.isAdmin = isAdmin;
        this.isActive = isActive;
    }

    public boolean isIsAdmin() {
        return isAdmin;
    }

    public void setIsAdmin(boolean isAdmin) {
        this.isAdmin = isAdmin;
    }

    public boolean isIsActive() {
        return isActive;
    }

    public void setIsActive(boolean isActive) {
        this.isActive = isActive;
    }
    
}
